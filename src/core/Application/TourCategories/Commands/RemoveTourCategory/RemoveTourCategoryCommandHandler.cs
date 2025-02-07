using Application.Interfaces.UnitOfWork;
using Domain.Errors;
using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.TourCategories.Commands.RemoveTourCategory;

public class RemoveTourCategoryCommandHandler
    : IRequestHandler<RemoveTourCategoryCommand, ErrorOr<RemoveTourCategoryCommandResponse>>
{
    private readonly ITourCategoryRepository _tourCategoryRepository;
    private readonly IUnitOfWorkRepo _unitOfWorkRepo;
    private readonly ILogger<RemoveTourCategoryCommandHandler> _logger;

    public RemoveTourCategoryCommandHandler(
        ILogger<RemoveTourCategoryCommandHandler> logger,
        IUnitOfWorkRepo unitOfWorkRepo,
        ITourCategoryRepository tourCategoryRepository
    )
    {
        _logger = logger;
        _unitOfWorkRepo = unitOfWorkRepo;
        _tourCategoryRepository = tourCategoryRepository;
    }

    public async Task<ErrorOr<RemoveTourCategoryCommandResponse>> Handle(
        RemoveTourCategoryCommand request,
        CancellationToken cancellationToken
    )
    {
        var tourCategory = await _tourCategoryRepository.GetAsync(request.Id);

        if (tourCategory is null)
        {
            _logger.LogWarning("Tour category with id {Id} not found", request.Id);
            return DomainErrors.TourCategory.TourCategoryNotFound();
        }

        _logger.LogInformation("tour category with id {Id} removed successfully", request.Id);

        _tourCategoryRepository.Delete(tourCategory);

        await _unitOfWorkRepo.SaveChangesAsync(cancellationToken);

        return new RemoveTourCategoryCommandResponse();
    }
}
