using FluentValidation;

namespace StudentEnrollment.API.DTO.Student;

public class StudentResponse : StudentCreateRequest
{
    public int Id { get; set; }
}

public class StudentDtoValidator : AbstractValidator<StudentResponse>
{
    public StudentDtoValidator()
    {
        Include(new StudentCreateRequestValidator());
    }
}
