global using AutoMapper;
using StudentEnrollment.API.DTO.Authentication;
using StudentEnrollment.API.DTO.Course;
using StudentEnrollment.API.DTO.Enrollment;
using StudentEnrollment.API.DTO.Student;
using StudentEnrollment.DATA;

namespace StudentEnrollment.API.Configurations;

public class MapperConfig : Profile
{
    public MapperConfig()
    {
        CreateMap<Course, CourseResponse>().ReverseMap();
        CreateMap<Course, CourseCreateRequest>().ReverseMap();
        CreateMap<Course, CourseDetailsDto>()
            .ForMember(q => q.Students, x => x.MapFrom(course => course.Enrollments.Select(s => s.Student)))
            .ReverseMap();

        CreateMap<Student, StudentCreateRequest>().ReverseMap();
        CreateMap<Student, StudentResponse>().ReverseMap();
        CreateMap<Student, StudentDetailsDto>()
            .ForMember(q => q.Courses, x => x.MapFrom(student => student.Enrollments.Select(c => c.Course)))
            .ReverseMap();

        CreateMap<Enrollment, EnrollmentCreateRequest>().ReverseMap();
        CreateMap<Enrollment, EnrollmentResponse>().ReverseMap();
    }
}
