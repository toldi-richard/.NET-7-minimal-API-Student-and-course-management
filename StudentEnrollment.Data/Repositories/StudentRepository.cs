using Microsoft.EntityFrameworkCore;
using StudentEnrollment.DATA.Repositories.IRepositories;

namespace StudentEnrollment.DATA.Repositories;

public class StudentRepository : GenericRepository<Student>, IStudentRepository
{
    public StudentRepository(StudentEnrollmentDbContext db) : base(db)
    {
    }

    public async Task<Student> GetStudentDetails(int studentId)
    {
        var student = await _db.Students
            .Include(q => q.Enrollments).ThenInclude(q => q.Course)
            .FirstOrDefaultAsync(q => q.Id == studentId);

        return student;
    }
}
