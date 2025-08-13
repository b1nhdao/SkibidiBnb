
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SkibidiBnb.Application.Common;
using SkibidiBnb.Application.DTO.Property;
using SkibidiBnb.Application.Interfaces;
using SkibidiBnb.Application.Services.UploadCloudServices;

namespace SkibidiBnb.Api.Apis
{
    public static class PropertyApi
    {
        public static IEndpointRouteBuilder MapProperty (this IEndpointRouteBuilder builder)
        {
            var endpoints = builder.MapGroup("api/v1/property");
            endpoints.MapGet("/", GetPropertiesAsync)
                .WithDescription("Get all properties");
            endpoints.MapPost("/", CreatePropertyAsync)
                .WithDescription("Create a new property")
                .RequireAuthorization()
                .DisableAntiforgery();
            return endpoints;
        }

        private static async Task<Results<Ok<CreatePropertyDTO>, BadRequest<string>>> CreatePropertyAsync(
            [FromForm]CreatePropertyDTO propertyDTO,
            [FromServices] IPropertyService _propertyService,
            [FromServices] IUploadCloudService _uploadCloudService
            )
        {
            var result = await _propertyService.CreatePropertyAsync(propertyDTO);
            if (result == null)
            {
                return TypedResults.BadRequest("Failed to create property.");
            }
            return TypedResults.Ok(propertyDTO);
        }

        private static async Task<Results<Ok<ApiResponse<PropertyResponseDTO>>, BadRequest>> GetPropertiesAsync(
            [AsParameters] RequestApi request,
            [FromServices] IPropertyService _propertyService)
        {
            var properties = await _propertyService.GetPropertiesPagedAsync(request);
            return TypedResults.Ok(properties);
        }
    }
}