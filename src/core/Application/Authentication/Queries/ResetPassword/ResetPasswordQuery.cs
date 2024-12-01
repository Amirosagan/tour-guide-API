using ErrorOr;
using MediatR;

namespace Application.Authentication.Queries.ResetPassword;

public record ResetPasswordQuery(
    string Email,
    string Token,
    string NewPassword
    ) : IRequest<ErrorOr<ResetPasswordQueryResponse>>;