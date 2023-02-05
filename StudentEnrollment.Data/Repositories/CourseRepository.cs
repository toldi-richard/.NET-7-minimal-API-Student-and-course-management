
using Microsoft.EntityFrameworkCore;
using StudentEnrollment.DATA.Repositories.IRepositories;

namespace StudentEnrollment.DATA.Repositories;

public class CourseRepository : GenericRepository<Course>, ICourseRepository
{
    public CourseRepository(StudentEnrollmentDbContext db) : base(db)
    {

    }
    public async Task<Course> GetStudentList(int courseId)
    {
        var course = await _db.Courses
            .Include(q => q.Enrollments)
            .ThenInclude(q => q.Student)
            .FirstOrDefaultAsync(q => q.Id == courseId);

        return course;
    }
}
