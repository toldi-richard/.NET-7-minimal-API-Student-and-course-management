using Microsoft.AspNetCore.Http.HttpResults;
using StudentEnrollment.DATA;
using StudentEnrollment.API.DTO.Course;
using StudentEnrollment.DATA.Repositories.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using StudentEnrollment.API.DTO.Authentication;
using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace StudentEnrollment.API.Endpoints;

public static class CourseEndpoints
{
    public static void MapCourseEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Course").WithTags(nameof(Course));

        group.MapGet("/", async (ICourseRepository repo, IMapper mapper) =>
        {
            return mapper.Map<List<CourseResponse>>(await repo.GetAllAsync());
        })
        .WithName("GetAllCourses")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<CourseResponse>, NotFound>> (int id, ICourseRepository repo, IMapper mapper) =>
        {
            return await repo.GetAsync(id)
                is Course model
                    ? TypedResults.Ok(mapper.Map<CourseResponse>(model))
                    : TypedResults.NotFound();
        })
        .WithName("GetCourseById")
        .WithOpenApi();

        group.MapGet("/GetStudents/{id}", async Task<Results<Ok<CourseDetailsDto>, NotFound>> (int id, ICourseRepository repo, IMapper mapper) =>
        {
            return await repo.GetStudentList(id)
                is Course model
                    ? TypedResults.Ok(mapper.Map<CourseDetailsDto>(model))
                    : TypedResults.NotFound();
        })
        .WithName("GetStudentsOfCourse")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<BadRequest<IDictionary<string, string[]>>, NotFound, NoContent>> (int id, CourseCreateRequest courseDTO, ICourseRepository repo, IMapper mapper, IValidator<CourseCreateRequest> validator) =>
        {
            var validationResult = await validator.ValidateAsync(courseDTO);
            if (!validationResult.IsValid)
            {
                return TypedResults.BadRequest(validationResult.ToDictionary());
            }

            var foundModel = await repo.GetAsync(id);
            if (foundModel is null)
            {
                return TypedResults.NotFound();
            }

            mapper.Map(courseDTO, foundModel);
            await repo.UpdateAsync(foundModel);

            return TypedResults.NoContent();
        })
        .WithName("UpdateCourse")
        .WithOpenApi();

        group.MapPost("/", async Task<Results<BadRequest<IDictionary<string, string[]>>, Created<Course>>> (CourseCreateRequest courseDTO, ICourseRepository repo, IMapper mapper, IValidator<CourseCreateRequest> validator) =>
        {
            var validationResult = await validator.ValidateAsync(courseDTO);
            if (!validationResult.IsValid)
            {
                return TypedResults.BadRequest(validationResult.ToDictionary());
            }

            var course = mapper.Map<Course>(courseDTO);
            await repo.AddAsync(course);
            return TypedResults.Created($"/api/Course/{course.Id}",course);
        })
        .WithName("CreateCourse")
        .WithOpenApi();

        group.MapDelete("/{id}", [Authorize(Roles = "Administrator")] async Task<Results<NoContent, NotFound>> (int id, ICourseRepository repo) =>
        {
            return await repo.DeleteAsync(id) ? TypedResults.NoContent() : TypedResults.NotFound();
        })
        .WithName("DeleteCourse")
        .WithOpenApi();
    }
}
