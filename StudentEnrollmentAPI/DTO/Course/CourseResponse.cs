using FluentValidation;

namespace StudentEnrollment.API.DTO.Course;

public class CourseResponse : CourseCreateRequest
{
    public int Id { get; set; }
}

public class CourseDtoValidator : AbstractValidator<CourseResponse>
{
    public CourseDtoValidator()
    {
        Include(new CourseCreateRequestValidator());
    }
}
