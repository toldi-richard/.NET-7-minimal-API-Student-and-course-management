using FluentValidation;

namespace StudentEnrollment.API.DTO.Student;

public class StudentCreateRequest
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateofBirth { get; set; }
    public string IdNumber { get; set; }
    public byte[] ProfilePicture { get; set; }
    public string OriginalFileName { get; set; }       
}

public class StudentCreateRequestValidator : AbstractValidator<StudentCreateRequest>
{
    public StudentCreateRequestValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty();
        RuleFor(x => x.LastName)
            .NotEmpty();
        RuleFor(x => x.DateofBirth)
            .NotEmpty();
        RuleFor(x => x.IdNumber)
            .NotEmpty();

        RuleFor(x => x.OriginalFileName)
            .NotNull()
            .When(x => x.ProfilePicture != null);
    }
}