
using FluentValidation;

namespace StudentEnrollment.API.DTO.Course;

public class CourseCreateRequest
{
    public string? Title { get; set; }
    public int Credits { get; set; }
}

public class CourseCreateRequestValidator : AbstractValidator<CourseCreateRequest>
{
    public CourseCreateRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty();
        RuleFor(x => x.Credits)
            .NotEmpty();
    }
}