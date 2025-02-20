using Domain.Entities;

using ErrorOr;

using MediatR;

namespace Application.TourCategories.Queries.GetTourCategories;

public record GetTourCategoriesQuery() : IRequest<ErrorOr<GetTourCategoriesQueryResponse>>;