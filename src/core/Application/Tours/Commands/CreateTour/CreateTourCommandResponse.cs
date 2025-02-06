using Application.Tours.Helpers;
using Domain.Entities;

namespace Application.Tours.Commands.CreateTour;

public record CreateTourCommandResponse(Guid TourId);
