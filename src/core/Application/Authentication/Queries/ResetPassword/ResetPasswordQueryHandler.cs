using Domain.Errors;
using Domain.Identity;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Authentication.Queries.ResetPassword;

public class ResetPasswordQueryHandler : IRequestHandler<ResetPasswordQuery, ErrorOr<ResetPasswordQueryResponse>>
{
    private readonly UserManager<NormalUser> _normalUserManager;
    private readonly UserManager<TourGuide> _tourGuideManager;

    public ResetPasswordQueryHandler(UserManager<NormalUser> normalUserManager, UserManager<TourGuide> tourGuideManager)
    {
        _normalUserManager = normalUserManager;
        _tourGuideManager = tourGuideManager;
    }

    public async Task<ErrorOr<ResetPasswordQueryResponse>> Handle(ResetPasswordQuery request, CancellationToken cancellationToken)
    {
        var user = await _normalUserManager.FindByEmailAsync(request.Email);
        if (user != null)
        {
            var result = await _normalUserManager.ResetPasswordAsync(user, request.Token, request.NewPassword);
            
            if (result.Errors.Any())
            {
                return Error.Failure(description: result.Errors.Select(x => x.Description).Aggregate((x, y) => $"{x}, {y}"));
            }
            
            return new ResetPasswordQueryResponse();
        }
        
        var tourGuide = await _tourGuideManager.FindByEmailAsync(request.Email);
        
        if (tourGuide != null)
        {
            var result = await _tourGuideManager.ResetPasswordAsync(tourGuide, request.Token, request.NewPassword);
            
            if (result.Errors.Any())
            {
                return Error.Failure(description: result.Errors.Select(x => x.Description).Aggregate((x, y) => $"{x}, {y}"));
            }
            
            return new ResetPasswordQueryResponse();
        }

        return DomainErrors.TokenConfirmation.EmailNotFound();
    }
}