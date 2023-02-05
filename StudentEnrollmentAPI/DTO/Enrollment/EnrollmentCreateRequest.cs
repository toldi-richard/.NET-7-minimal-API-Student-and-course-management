using FluentValidation;
using StudentEnrollment.DATA.Repositories.IRepositories;

namespace StudentEnrollment.API.DTO.Enrollment;

public class EnrollmentCreateRequest
{
    public int CourseId { get; set; }
    public int StudentId { get; set; }
}

public class CreateEnrollmentDtoValidator : AbstractValidator<EnrollmentCreateRequest>
{
    private readonly ICourseRepository _courseRepository;
    private readonly IStudentRepository _stuedentRepository;

    public CreateEnrollmentDtoValidator(ICourseRepository courseRepository, IStudentRepository stuedentRepository)
    {
        _courseRepository = courseRepository;
        _stuedentRepository = stuedentRepository;

        RuleFor(x => x.CourseId)
            .MustAsync(async (id, token) =>
            {
                var courseExists = await _courseRepository.Exists(id);
                return courseExists;
            });

        RuleFor(x => x.StudentId)
            .MustAsync(async (id, token) =>
            {
                var strudentExists = await _stuedentRepository.Exists(id);
                return strudentExists;
            });
    }
}
