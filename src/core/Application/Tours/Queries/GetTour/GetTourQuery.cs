using Domain.Entities;
using ErrorOr;

using MediatR;

namespace Application.Tours.Queries.GetTour;

public record GetTourQuery(
    Guid Id
) : IRequest<ErrorOr<Tour>>;