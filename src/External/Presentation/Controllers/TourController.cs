using Application.Tours.Commands.CreateTour;
using Application.Tours.Queries.GetAllTours;
using Application.Tours.Queries.GetTour;

using AutoMapper;

using Domain.Enums;

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Contracts.Tours.Requests;
using Presentation.Contracts.Tours.Responses;

namespace Presentation.Controllers;

[Route("api/Tours")]
public class TourController : Controller
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public TourController(ISender mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    /// <summary>
    /// Creates a new tour.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>The newly created tour.</returns>
    /// <response code="200">The newly created tour.</response>
    /// <response code="400">The request was invalid.</response>
    [HttpPost]
    [Authorize(Roles = nameof(Roles.Admin))]
    public async Task<IActionResult> Create([FromBody] CreateTourDto request)
    {
        var command = _mapper.Map<CreateTourCommand>(request);
        var result = await _mediator.Send(command);
        return result.Match<IActionResult>(
            success => Ok(_mapper.Map<CreateTourResponseDto>(success)),
            BadRequest
        );
    }

/// <summary>
/// Retrieves all tours based on the specified parameters.
/// </summary>
/// <param name="request">The request containing parameters for filtering the tours.</param>
/// <returns>A list of tours that match the request parameters.</returns>
/// <response code="200">Returns the list of tours.</response>
/// <response code="400">The request was invalid.</response>
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] GetAllToursRequestDto request)
    {
        var query = _mapper.Map<GetAllToursQuery>(request);

        var result = await _mediator.Send(query);
        return result.Match<IActionResult>(
            success => Ok(_mapper.Map<GetAllToursResponseDto>(success)),
            BadRequest
        );
    }


    /// <summary>
    /// Retrieves the specified tour.
    /// </summary>
    /// <param name="id">The id of the tour to retrieve.</param>
    /// <returns>The specified tour, if found.</returns>
    /// <response code="200">Returns the specified tour.</response>
    /// <response code="404">The tour with the specified <paramref name="id"/> was not found.</response>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var query = new GetTourQuery(id);
        var result = await _mediator.Send(query);
        return result.Match<IActionResult>(
            success => Ok(_mapper.Map<GetTourResponseDto>(success)),
            NotFound
        );
    }
}