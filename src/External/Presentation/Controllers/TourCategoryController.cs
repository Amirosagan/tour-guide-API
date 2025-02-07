using Application.TourCategories.Commands.CreateTourCategory;
using Application.TourCategories.Commands.RemoveTourCategory;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Contracts.TourCategories.Requests;
using Presentation.Contracts.TourCategories.Responses;

namespace Presentation.Controllers;

[Route("api/TourCategory")]
public class TourCategoryController : Controller
{
    private readonly IMapper _mapper;
    private readonly ISender _mediator;

    public TourCategoryController(IMapper mapper, ISender mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTourCategoryDto request)
    {
        var command = _mapper.Map<CreateTourCategoryCommand>(request);

        var result = await _mediator.Send(command);
        return result.Match<IActionResult>(
            success => Ok(_mapper.Map<CreateTourCategoryResponseDto>(success)),
            BadRequest
        );
    }

    [HttpDelete]
    public async Task<IActionResult> Remove([FromQuery] RemoveTourCategoryDto request)
    {
        var command = _mapper.Map<RemoveTourCategoryCommand>(request);

        var result = await _mediator.Send(command);
        return result.Match<IActionResult>(success => Ok(), BadRequest);
    }
}
