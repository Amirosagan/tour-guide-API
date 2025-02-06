using Application.Tours.Commands.CreateTour;
using AutoMapper;
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

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTourDto request)
    {
        var command = _mapper.Map<CreateTourCommand>(request);
        var result = await _mediator.Send(command);
        return result.Match<IActionResult>(
            success => Ok(_mapper.Map<CreateTourResponseDto>(success)),
            BadRequest
        );
    }
}
