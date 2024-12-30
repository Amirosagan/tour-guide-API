using System.Security.Claims;
using Application.Authentication.Commands.ConfirmEmail;
using Application.Authentication.Commands.ForgotPassword;
using Application.Authentication.Commands.GenerateNewConfirmToken;
using Application.Authentication.Commands.UserRegister;
using Application.Authentication.Queries.ResetPassword;
using Application.Authentication.Queries.UserLogin;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Presentation.Contracts.Auth.Requests;
using Presentation.Contracts.Auth.Responses;

namespace Presentation.Controllers;

[ApiController]
[Route("auth")]
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
    public async Task<IActionResult> Login([FromQuery] AuthenticationLoginRequest request)
    {
        var query = _mapper.Map<UserLoginQuery>(request);
        var result = await _mediator.Send(query);
        return result.Match<IActionResult>(
            success => Ok(_mapper.Map<AuthenticationLoginResponse>(success)),
            BadRequest
        );
    }

    [HttpPost("NewEmailConfirmLink")]
    public async Task<IActionResult> NewEmailConfirmLink([FromBody] NewEmailConfirmLinkRequest request)
    {
        var command = _mapper.Map<GenerateNewConfirmTokenCommand>(request);
        var result = await _mediator.Send(command);
        return result.Match<IActionResult>(
            success => Ok(_mapper.Map<NewEmailConfirmLinkResponse>(success)),
            BadRequest
        );
    }
    
    [HttpGet("ConfirmEmail")]
    public async Task<IActionResult> ConfirmEmail([FromQuery] ConfirmEmailRequest request)
    {
        var command = _mapper.Map<ConfirmEmailCommand>(request);
        var result = await _mediator.Send(command);
        return result.Match<IActionResult>(
            success => Ok(_mapper.Map<ConfirmEmailResponse>(success)),
            BadRequest
        );
    } 
    
    [HttpPost("ForgotPassword")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
    {
        var command = _mapper.Map<ForgotPasswordCommand>(request);
        var result = await _mediator.Send(command);
        return result.Match<IActionResult>(
            success => Ok(_mapper.Map<ForgotPasswordResponse>(success)),
            BadRequest
        );
    }
    
    [HttpGet("ResetPassword")]
    public async Task<IActionResult> ResetPassword([FromQuery] ResetPasswordRequest request)
    {
        var query = _mapper.Map<ResetPasswordQuery>(request);
        var result = await _mediator.Send(query);
        return result.Match<IActionResult>(
            success => Ok(_mapper.Map<ResetPasswordResponse>(success)),
            BadRequest
        );
    }
    
    [HttpGet("google-login")]
    public IActionResult GoogleLogin()
    {
        var redirectUrl = Url.Action("GoogleSignin", "Auth");
        var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
        return Challenge(properties, GoogleDefaults.AuthenticationScheme);
    }
    
    [HttpGet("google-signin")]
    public async Task<IActionResult> GoogleSignin()
    {
        var authenticateResult = await HttpContext.AuthenticateAsync(IdentityConstants.ExternalScheme);
        if (!authenticateResult.Succeeded || authenticateResult.Principal == null)
            return Unauthorized();
        var email = authenticateResult.Principal.FindFirstValue(ClaimTypes.Email);
        var name = authenticateResult.Principal.FindFirstValue(ClaimTypes.Name);
        var picture = authenticateResult.Principal.FindFirstValue("picture");
        authenticateResult.Principal.Claims.ToList().ForEach(c => Console.WriteLine(c.Type + " " + c.Value));
        authenticateResult.Principal.Identities.ToList().ForEach(i => Console.WriteLine(i.Name));
        authenticateResult.Properties.Items.ToList().ForEach(i => Console.WriteLine(i.Key + " " + i.Value));
        Console.WriteLine(authenticateResult.Properties.GetTokenValue("refresh_token"));
        Console.WriteLine(authenticateResult.Properties.GetTokenValue("refresh_token"));
        

        return Ok(email);
    }
}