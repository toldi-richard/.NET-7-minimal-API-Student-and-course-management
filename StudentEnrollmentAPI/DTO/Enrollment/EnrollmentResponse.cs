using FluentValidation;
using StudentEnrollment.API.DTO.Course;
using StudentEnrollment.API.DTO.Student;
using StudentEnrollment.DATA.Repositories.IRepositories;

namespace StudentEnrollment.API.DTO.Enrollment;

public class EnrollmentResponse : EnrollmentCreateRequest
{
    public virtual CourseResponse? Course { get; set; }
    public virtual StudentResponse? Student { get; set; }

}

public class EnrollmentResponseValidator : AbstractValidator<EnrollmentResponse>
{
    public EnrollmentResponseValidator(ICourseRepository courseRepository, IStudentRepository stuedentRepository)
    {
        Include(new CreateEnrollmentDtoValidator(courseRepository, stuedentRepository));
    }
}