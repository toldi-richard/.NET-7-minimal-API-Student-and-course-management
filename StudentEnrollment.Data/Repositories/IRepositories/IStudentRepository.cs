namespace StudentEnrollment.DATA.Repositories.IRepositories;

public interface IStudentRepository : IGenericRepository<Student>
{
    Task<Student> GetStudentDetails(int studentId);
}
