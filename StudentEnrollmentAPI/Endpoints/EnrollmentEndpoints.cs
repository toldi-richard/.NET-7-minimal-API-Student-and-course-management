using Microsoft.AspNetCore.Http.HttpResults;
using StudentEnrollment.DATA;
using StudentEnrollment.API.DTO.Enrollment;
using StudentEnrollment.DATA.Repositories.IRepositories;
using FluentValidation;
using StudentEnrollment.API.DTO.Authentication;

namespace StudentEnrollment.API.Endpoints;

public static class EnrollmentEndpoints
{
    public static void MapEnrollmentEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Enrollment").WithTags(nameof(Enrollment)).WithOpenApi();

        group.MapGet("/", async (IEnrollmentRepository repo, IMapper mapper) =>
        {
            var enrollments = await repo.GetAllAsync();
            var data = mapper.Map<List<EnrollmentResponse>>(enrollments);
            return data;
        })
        .WithName("GetAllEnrollments")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<EnrollmentResponse>, NotFound>> (int id, IEnrollmentRepository repo, IMapper mapper) =>
        {
            return await repo.GetAsync(id)
                is Enrollment model
                    ? TypedResults.Ok(mapper.Map<EnrollmentResponse>(model))
                    : TypedResults.NotFound();
        })
        .WithName("GetEnrollmentById");

        group.MapPut("/{id}", async Task<Results<BadRequest<IDictionary<string, string[]>>, NotFound, NoContent>> (int id, EnrollmentResponse enrollmentResponse, IEnrollmentRepository repo, IMapper mapper, IValidator<EnrollmentResponse> validator) =>
        {
            var validationResult = await validator.ValidateAsync(enrollmentResponse);
            if (!validationResult.IsValid)
            {
                return TypedResults.BadRequest(validationResult.ToDictionary());
            }
            var foundModel = await repo.GetAsync(id);

            if (foundModel is null)
            {
                return TypedResults.NotFound();
            }

            mapper.Map(enrollmentResponse, foundModel);
            await repo.UpdateAsync(foundModel);

            return TypedResults.NoContent();
        })
        .WithName("UpdateEnrollment");

        group.MapPost("/", async Task<Results<BadRequest<IDictionary<string, string[]>>, Created<Enrollment>>> (EnrollmentCreateRequest enrollmentResponse, IEnrollmentRepository repo, IMapper mapper, IValidator<EnrollmentCreateRequest> validator) =>
        {
            var validationResult = await validator.ValidateAsync(enrollmentResponse);
            if (!validationResult.IsValid)
            {
                return TypedResults.BadRequest(validationResult.ToDictionary());
            }

            var enrollment = mapper.Map<Enrollment>(enrollmentResponse);
            await repo.AddAsync(enrollment);
            return TypedResults.Created($"/api/Enrollment/{enrollment.Id}", enrollment);
        })
        .WithName("CreateEnrollment");

        group.MapDelete("/{id}", async Task<Results<NoContent, NotFound>> (int id, ICourseRepository repo) =>
        {
            return await repo.DeleteAsync(id) ? TypedResults.NoContent() : TypedResults.NotFound();
        })
        .WithName("DeleteEnrollment");
    }
}
