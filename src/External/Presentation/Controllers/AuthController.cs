using Application.Authentication.Commands.UserRegister;
using Application.Authentication.Queries.UserLogin;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Contracts.Auth.Requests;
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
    public async Task<IActionResult> Register([FromBody] AuthenticationSignupRequest request)
    {
        var command = _mapper.Map<UserRegisterCommand>(request);
        var result = await _mediator.Send(command);
        return result.Match<IActionResult>(
            success => Ok(_mapper.Map<AuthenticationSignupResponse>(success)),
            BadRequest
        );
    }
    
    [HttpGet("Login")]
    public async Task<IActionResult> Login([FromBody] AuthenticationLoginRequest request)
    {
        var query = _mapper.Map<UserLoginQuery>(request);
        var result = await _mediator.Send(query);
        return result.Match<IActionResult>(
            success => Ok(_mapper.Map<AuthenticationLoginResponse>(success)),
            BadRequest
        );
    }
}