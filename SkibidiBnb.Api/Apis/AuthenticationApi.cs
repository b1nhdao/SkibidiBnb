using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SkibidiBnb.Application.Features.Authentication.DTOs;
using SkibidiBnb.Application.Features.Authentication.Services;
using SkibidiBnb.Application.Features.User.DTOs;
using SkibidiBnb.Application.Features.User.Services;

namespace SkibidiBnb.Api.Apis
{
    public static class AuthenticationApi
    {
        public static IEndpointRouteBuilder MapAuthenticationApi(this IEndpointRouteBuilder builder)
        {
            var endpoints = builder.MapGroup("api/v1/auth");

            endpoints.MapPost("/signup", SignUpAsync)
                .WithDescription("Create a new account")
                .DisableAntiforgery();
            endpoints.MapPost("/login", LogInAsync)
                .WithDescription("Login to get a JWT token");
            return builder;
        }

        private static async Task<Results<Ok<UserResponseDTO>, BadRequest<string>>> SignUpAsync(
            [FromForm]CreateUserRequestDTO request,
            [FromServices]IUserService _userService)
        {
            var result = await _userService.CreateUser(request);
            if (result == null)
            {
                return TypedResults.BadRequest("User creation failed.");
            }
            return TypedResults.Ok(result.Value);
        }

        private static async Task<Results<Ok<LoginResponseDTO>, BadRequest<string>>> LogInAsync(LoginRequestDTO model, IAuthenticationService authenticationService)
        {
            var result = await authenticationService.Login(model.Email, model.Password);
            if (result == null)
            {
                return TypedResults.BadRequest("Invalid email or password.");
            }
            return TypedResults.Ok(result);
        }
    }
}
