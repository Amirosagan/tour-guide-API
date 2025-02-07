using Application.Interfaces.UnitOfWork;
using Domain.Entities;
using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.TourCategories.Commands.CreateTourCategory;

public class CreateTourCategoryCommandHandler
    : IRequestHandler<CreateTourCategoryCommand, ErrorOr<CreateTourCategoryCommandResponse>>
{
    private readonly ITourCategoryRepository _tourCategoryRepository;
    private readonly IUnitOfWorkRepo _unitOfWorkRepo;
    private readonly ILogger<CreateTourCategoryCommandHandler> _logger;

    public CreateTourCategoryCommandHandler(
        ITourCategoryRepository tourCategoryRepository,
        ILogger<CreateTourCategoryCommandHandler> logger,
        IUnitOfWorkRepo unitOfWorkRepo
    )
    {
        _tourCategoryRepository = tourCategoryRepository;
        _logger = logger;
        _unitOfWorkRepo = unitOfWorkRepo;
    }

    public async Task<ErrorOr<CreateTourCategoryCommandResponse>> Handle(
        CreateTourCategoryCommand request,
        CancellationToken cancellationToken
    )
    {
        var newTourCategory = new TourCategory { Name = request.Name };

        _tourCategoryRepository.Add(newTourCategory);

        await _unitOfWorkRepo.SaveChangesAsync(cancellationToken);

        return new CreateTourCategoryCommandResponse(newTourCategory.Id, newTourCategory.Name);
    }
}
