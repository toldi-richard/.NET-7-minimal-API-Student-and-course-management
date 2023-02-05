using Microsoft.AspNetCore.Http.HttpResults;
using StudentEnrollment.DATA;
using StudentEnrollment.API.DTO.Student;
using StudentEnrollment.DATA.Repositories.IRepositories;
using FluentValidation;
using StudentEnrollment.API.Services.IServices;
using StudentEnrollment.API.Filters;

namespace StudentEnrollment.API.Endpoints;

public static class StudentEndpoints
{
    public static void MapStudentEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Student").WithTags(nameof(Student));

        group.MapGet("/", async (IStudentRepository repo, IMapper mapper) =>
        {
            var students = await repo.GetAllAsync();
            return mapper.Map<List<StudentResponse>>(students);
        })
        .WithName("GetAllStudents")
        .Produces<List<StudentResponse>>(StatusCodes.Status200OK);

        group.MapGet("/{id}", async Task<Results<Ok<StudentResponse>, NotFound>> (int id, IStudentRepository repo, IMapper mapper) =>
        {
            return await repo.GetAsync(id)
                is Student model
                    ? TypedResults.Ok(mapper.Map<StudentResponse>(model))
                    : TypedResults.NotFound();
        })
        .WithName("GetStudentById")
        .Produces<StudentResponse>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        group.MapGet("/GetDetails/{id}", async Task<Results<Ok<StudentDetailsDto>, NotFound>> (int id, IStudentRepository repo, IMapper mapper) =>
        {
            return await repo.GetStudentDetails(id)
                is Student model
                    ? TypedResults.Ok(mapper.Map<StudentDetailsDto>(model))
                    : TypedResults.NotFound();
        })
        .WithName("GetCoursesOfStudent")
        .Produces<StudentDetailsDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        group.MapPut("/{id}", async Task<Results<BadRequest<IDictionary<string, string[]>>, NotFound, NoContent>> (int id, StudentResponse studentResponse, IStudentRepository repo, IMapper mapper, IValidator<StudentResponse> validator, IFileUpload fileUpload) =>
        {
            var validationResult = await validator.ValidateAsync(studentResponse);
            if (!validationResult.IsValid)
            {
                return TypedResults.BadRequest(validationResult.ToDictionary());
            }

            var foundModel = await repo.GetAsync(id);
            if (foundModel is null)
            {
                return TypedResults.NotFound();
            }

            mapper.Map(studentResponse, foundModel);

            if (studentResponse.ProfilePicture != null)
            {
                foundModel.Picture = fileUpload.UploadStudentFile(studentResponse.ProfilePicture, studentResponse.OriginalFileName);
            }

            await repo.UpdateAsync(foundModel);
            return TypedResults.NoContent();
        })
        .WithName("UpdateStudent")
        .Produces(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status204NoContent);

        group.MapPost("/", async Task<Results<BadRequest<IDictionary<string, string[]>>, Created<Student>>> (StudentCreateRequest createdStudent, IStudentRepository repo, IMapper mapper, IValidator<StudentCreateRequest> validator, IFileUpload fileUpload) =>
        {
            var validationResult = await validator.ValidateAsync(createdStudent);
            if (!validationResult.IsValid)
            {
                return TypedResults.BadRequest(validationResult.ToDictionary());
            }

            var student = mapper.Map<Student>(createdStudent);

            student.Picture = fileUpload.UploadStudentFile(createdStudent.ProfilePicture, createdStudent.OriginalFileName);

            await repo.AddAsync(student);
            return TypedResults.Created($"/api/Student/{student.Id}", student);
        })
        .AddEndpointFilter<ValidationFilter<StudentCreateRequest>>()
        .AddEndpointFilter<LoggingFilter>()
        .WithName("CreateStudent")
        .Produces<Student>(StatusCodes.Status201Created);

        group.MapDelete("/{id}", async Task<Results<NoContent, NotFound>> (int id, IStudentRepository repo) =>
        {
            return await repo.DeleteAsync(id) ? TypedResults.NoContent() : TypedResults.NotFound();
        })
        .WithName("DeleteStudent")
        .Produces<Student>(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound);
    }
}
