using StudentEnrollment.API.DTO.Student;

namespace StudentEnrollment.API.DTO.Course;

public class CourseDetailsDto
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public int Credits { get; set; }
    public List<StudentResponse> Students { get; set; } = new List<StudentResponse>();
}
