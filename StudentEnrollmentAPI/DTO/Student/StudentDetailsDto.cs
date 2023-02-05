using StudentEnrollment.API.DTO.Course;

namespace StudentEnrollment.API.DTO.Student;

public class StudentDetailsDto : StudentCreateRequest
{
    public List<CourseResponse> Courses { get; set; } = new List<CourseResponse>();
}
