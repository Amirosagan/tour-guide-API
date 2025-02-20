using Application.TourCategories.Commands.CreateTourCategory;
using Application.TourCategories.Commands.RemoveTourCategory;
using Application.TourCategories.Queries.GetTourCategories;
using Application.Tours.Queries.GetAllTours;

using AutoMapper;
using Domain.Enums;
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

    /// <summary>
    /// Creates a new tour category.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <returns>The newly created tour category.</returns>
    /// <response code="200">The newly created tour category.</response>
    /// <response code="400">The request was invalid.</response>
    [HttpPost]
    [Authorize(Roles = nameof(Roles.Admin))]
    public async Task<IActionResult> Create([FromBody] CreateTourCategoryDto request)
    {
        var command = _mapper.Map<CreateTourCategoryCommand>(request);

        var result = await _mediator.Send(command);
        return result.Match<IActionResult>(
            success => Ok(_mapper.Map<CreateTourCategoryResponseDto>(success)),
            BadRequest
        );
    }

    /// <summary>
    /// Removes a tour category.
    /// </summary>
    /// <param name="request">The request containing the ID of the tour category to be removed.</param>
    /// <returns>Returns Ok if the tour category is successfully removed, otherwise returns BadRequest.</returns>
    /// <response code="200">The tour category was successfully removed.</response>
    /// <response code="400">The request was invalid or the tour category was not found.</response>
    [HttpDelete]
    [Authorize(Roles = nameof(Roles.Admin))]
    public async Task<IActionResult> Remove([FromQuery] RemoveTourCategoryDto request)
    {
        var command = _mapper.Map<RemoveTourCategoryCommand>(request);

        var result = await _mediator.Send(command);
        return result.Match<IActionResult>(success => Ok(), BadRequest);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var query = new GetTourCategoriesQuery();

        var result = await _mediator.Send(query);
        
        return result.Match<IActionResult>(
            success => Ok(_mapper.Map<GetAllTourCategoriesResponseDto>(success)),
            BadRequest
        );
    }
}
