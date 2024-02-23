using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TechnicalEvaluation.Application.Careers.Dtos;
using TechnicalEvaluation.Application.UseCases;
using TechnicalEvaluation.Presentation.Api.Careers.Responses;

namespace TechnicalEvaluation.Presentation.Api.Careers
{
    /// <summary>
    /// Static class containing handlers for career-related endpoints
    /// and a method to register those endpoints in the route builder.
    /// </summary>
    public static class CareersEndpoints
    {
        /// <summary>
        /// Handles adding content to a career.
        /// </summary>
        /// <param name="careerName">The name of the career.</param>
        /// <param name="contentDescription">The description of the content.</param>
        /// <param name="careerUseCase">The business logic implementation for career operations.</param>
        /// <returns>An instance of AddContentToCareerResponse.</returns>
        public static async Task<Results<NotFound, Ok<AddContentToCareerResponse>>> AddContentToCareerHandler(
            [FromRoute] string careerName,
            [FromBody] ContentDto content,
            [FromServices] ICareerUseCase careerUseCase)
        {
            try
            {
                var career = await
                    careerUseCase.AddContentToCareerAsync(careerName, content.ContentDescription, content.ContentType);
                return TypedResults.Ok(new AddContentToCareerResponse(CareerDto.FromCareer(career)));
            } 
                catch
            {
                return TypedResults.NotFound();
            }
        }

        /// <summary>
        /// Handles the creation of a new career.
        /// </summary>
        /// <param name="careerDto">The career to be created.</param>
        /// <param name="careerUseCase">The business logic implementation for career operations.</param>
        /// <returns>An instance of CreateCareerResponse.</returns>
        public static async Task<Results<NotFound, Ok<CreateCareerResponse>>> AddCareerHandler(
            [FromBody] CareerDto careerDto,
            [FromServices] ICareerUseCase careerUseCase)
        {
            try
            {
                var career = await careerUseCase.CreateCareerAsync(careerDto);
                return TypedResults.Ok(new CreateCareerResponse(CareerDto.FromCareer(career)));
            }
            catch
            {
                return TypedResults.NotFound();
            }
        }

        /// <summary>
        /// Handles searching careers by name.
        /// </summary>
        /// <param name="careerName">The name to search careers by.</param>
        /// <param name="careerUseCase">The business logic implementation for career operations.</param>
        /// <returns>An instance of SearchCareersByNameResponse.</returns>
        public static async Task<SearchCareersByNameResponse> SearchCareersByNameHandler(
            [FromRoute] string careerName,
            [FromServices] ICareerUseCase careerUseCase)
        {
            var careers = await careerUseCase.SearchCareersByNameAsync(careerName);
            return new SearchCareersByNameResponse(
                careers.Select(c => CareerDto.FromCareer(c))
                       .ToList());
        }

        /// <summary>
        /// Handles getting a career by ID.
        /// </summary>
        /// <param name="careerName">The ID of the career to retrieve.</param>
        /// <param name="careerUseCase">The business logic implementation for career operations.</param>
        /// <returns>An instance of Results, NotFound result if the career is not found or Ok result with GetCareerByIdResponse if found.</returns>
        public static async Task<Results<NotFound, Ok<GetCareerByIdResponse>>> GetCareerByIdHandler(
            [FromRoute] string careerName,
            [FromServices] ICareerUseCase careerUseCase)
        {
            var career = await careerUseCase.GetCareerByIdAsync(careerName);
            if (career is null)
            {
                return TypedResults.NotFound();
            }

            return TypedResults.Ok(
                new GetCareerByIdResponse(CareerDto.FromCareer(career)
                ));
        }

        public static async Task<List<ContentTypeDto>> GetAllContentTypesHandler(
            [FromServices] ICareerUseCase careerUseCase)
        {
            var contentTypes = await careerUseCase.GetAllContentTypesAsync();
            var contentTypeDtos = new List<ContentTypeDto>();
            foreach ( var contentType in contentTypes)
            {
                contentTypeDtos.Add(ContentTypeDto.FromContenType(contentType));
            }
            return contentTypeDtos;
        }

        public static async Task<Results<NotFound<string>, Ok>> UpdateCareerHandler(
            [FromBody] CareerDto career,
            [FromServices] ICareerUseCase careerUseCase)
        {
            try
            {
                await careerUseCase.UpdateCareerAsync(career);
                return TypedResults.Ok();
            }
            catch (Exception ex)
            {
                return TypedResults.NotFound(ex.Message);
            }
        }

        public static async Task<List<CareerDto>> GetAllCareerHandler(
            [FromServices] ICareerUseCase careerUseCase)
        {
            var careers = await careerUseCase.GetAllCareersAsync();
            var careerDtos = new List<CareerDto>();
            foreach (var career in careers)
            {
                careerDtos.Add(CareerDto.FromCareer(career));
            }
            return careerDtos;
        }

        /// <summary>
        /// Extension method to register career-related endpoints in the route builder.
        /// </summary>
        /// <param name="builder">The IEndpointRouteBuilder instance being extended.</param>
        /// <returns>The IEndpointRouteBuilder instance with the endpoints registered.</returns>
        public static IEndpointRouteBuilder RegisterCareersEndpoints(this IEndpointRouteBuilder builder)
        {
            builder
                .MapPost("/careers/{careerName}/add-content", CareersEndpoints.AddContentToCareerHandler)
                .WithName("AddContentToCareer")
                .WithOpenApi(); // Registers the endpoint to handle adding content to a career.

            builder
                .MapPost("/careers/add-career", CareersEndpoints.AddCareerHandler)
                .WithName("AddCareer")
                .WithOpenApi(); // Registers the endpoint to handle adding a new career.

            builder
                .MapGet("/careers/list", CareersEndpoints.GetAllCareerHandler)
                .WithName("CareersList")
                .WithOpenApi(); // Registers the endpoint to handle searching careers by name.

            builder
                .MapGet("/careers/list/{careerName}", CareersEndpoints.SearchCareersByNameHandler)
                .WithName("SearchCareersList")
                .WithOpenApi(); // Registers the endpoint to handle searching careers by name.

            builder
                .MapGet("/careers/info/{careerName}", CareersEndpoints.GetCareerByIdHandler)
                .WithName("GetCareer")
                .WithOpenApi(); // Registers the endpoint to handle getting a career by ID.

            builder
                .MapGet("/contentTypes/list", CareersEndpoints.GetAllContentTypesHandler)
                .WithName("GetAllContentTypes")
                .WithOpenApi(); // Registers the endpoint to handle getting content types.

            builder
                .MapPut("/careers/update", CareersEndpoints.UpdateCareerHandler)
                .WithName("UpdateCareer")
                .WithOpenApi(); // Registers the endpoint to handle update career.


            return builder;
        }
    }
}
