using ErrorOr;
using MediatR;

namespace Application.Authentication.Queries.ResetPassword;

public record ResetPasswordQuery(string Email, string Otp, string NewPassword)
    : IRequest<ErrorOr<ResetPasswordQueryResponse>>;
