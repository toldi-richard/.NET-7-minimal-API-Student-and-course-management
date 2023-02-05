
namespace StudentEnrollment.DATA.Repositories.IRepositories;

public interface ICourseRepository : IGenericRepository<Course>
{
    Task<Course> GetStudentList(int courseId);
}



