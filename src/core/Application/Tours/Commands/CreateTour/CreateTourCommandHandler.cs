using Application.Interfaces.UnitOfWork;
using Domain.Entities;
using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Tours.Commands.CreateTour;

public class CreateTourCommandHandler
    : IRequestHandler<CreateTourCommand, ErrorOr<CreateTourCommandResponse>>
{
    private readonly ITourRepository _tourRepository;
    private readonly IUnitOfWorkRepo _unitOfWorkRepo;
    private readonly ISessionRepository _sessionRepository;
    private readonly ITourCategoryRepository _tourCategoryRepository;
    private readonly ILogger<CreateTourCommandHandler> _logger;

    public CreateTourCommandHandler(
        ITourRepository tourRepository,
        ILogger<CreateTourCommandHandler> logger,
        ISessionRepository sessionRepository,
        IUnitOfWorkRepo unitOfWorkRepo,
        ITourCategoryRepository tourCategoryRepository
    )
    {
        _tourRepository = tourRepository;
        _logger = logger;
        _sessionRepository = sessionRepository;
        _unitOfWorkRepo = unitOfWorkRepo;
        _tourCategoryRepository = tourCategoryRepository;
    }

    public async Task<ErrorOr<CreateTourCommandResponse>> Handle(
        CreateTourCommand request,
        CancellationToken cancellationToken
    )
    {
        _logger.LogInformation("Creating tour with name {Name}", request.Name);

        var tour = new Tour
        {
            Name = request.Name,
            Description = request.Description,
            Location = request.Location,
            Price = request.Price,
            Images = request.Images,
            MaxCapacity = request.MaxCapacity,
        };

        _logger.LogInformation("Creating Session for Tour with Id {Id}", tour.Id);

        foreach (var session in request.Sessions)
        {
            var newSession = new Session
            {
                TourId = tour.Id,
                StartDate = session.StartDate,
                EndDate = session.EndDate,
                MaxCapacity = request.MaxCapacity,
            };

            _sessionRepository.Add(newSession);
        }

        _logger.LogInformation("Adding Tour Category with Id {Id}", tour.Id);

        var tourCategories = new List<TourCategory>();

        foreach (var tourCategory in request.TourCategoryIds)
        {
            var category = await _tourCategoryRepository.GetAsync(tourCategory);

            if (category is null)
            {
                _logger.LogWarning("Category with Id {Id} not found", tourCategory);
                continue;
            }

            tourCategories.Add(category);
        }

        tour.Categories = tourCategories;

        _logger.LogInformation("Saving changes to database");

        await _tourRepository.AddAsync(tour);

        await _unitOfWorkRepo.SaveChangesAsync(cancellationToken);

        return new CreateTourCommandResponse(tour.Id);
    }
}
