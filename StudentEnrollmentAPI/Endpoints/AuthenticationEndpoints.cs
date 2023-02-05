using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using StudentEnrollment.API.DTO;
using StudentEnrollment.API.DTO.Authentication;
using StudentEnrollment.API.Filters;
using StudentEnrollment.API.Services.IServices;

namespace StudentEnrollment.API.Endpoints;

public static class AuthenticationEndpoints
{
    public static void MapAuthenticationEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api").WithTags("Authentication").WithOpenApi();

        group.MapPost("/Login", async Task<Results<BadRequest<IDictionary<string, string[]>>,Ok<AuthResponseDto>, UnauthorizedHttpResult>> (LoginDto loginDTO, IAuthManager authManager, IValidator<LoginDto> validator) =>
        {
            var response = await authManager.Login(loginDTO);
            if (response is null)
            {
                return TypedResults.Unauthorized();
            }

            return TypedResults.Ok(response);
        })
        .AddEndpointFilter<ValidationFilter<LoginDto>>()
        .AllowAnonymous()
        .WithName("Login")
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status401Unauthorized);

        group.MapPost("/Register", [AllowAnonymous] async Task<Results<BadRequest<IDictionary<string, string[]>>, Ok, BadRequest<List<ErrorResponseDto>>>> (RegisterDto registerDTO, IAuthManager authManager, IValidator<LoginDto> validator) =>
        {
            var response = await authManager.Register(registerDTO);
            if (!response.Any())
            {
                return TypedResults.Ok();
            }

            var errors = new List<ErrorResponseDto>();
            foreach (var error in response)
            {
                errors.Add(new ErrorResponseDto
                {
                    Code = error.Code,
                    Description = error.Description
                });
            }

            return TypedResults.BadRequest(errors);
        })
        .AddEndpointFilter<ValidationFilter<RegisterDto>>()
        .WithName("Register")
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest);
    }
}
