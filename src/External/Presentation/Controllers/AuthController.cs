using Application.Authentication.Commands.UserRegister;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Contracts.Auth.Responses;

namespace Presentation.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : Controller
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;
    
    public AuthController(ISender mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }
    
    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] UserRegisterCommand command)
    {
        var result = await _mediator.Send(command);
        return result.Match<IActionResult>(
            success => Ok(_mapper.Map<AuthenticationSignupResponse>(success)),
            error => BadRequest(error)
        );
    }
    
}