
using StudentEnrollment.DATA.Repositories.IRepositories;

namespace StudentEnrollment.DATA.Repositories;

public class EnrollmentRepository : GenericRepository<Enrollment>, IEnrollmentRepository
{
	public EnrollmentRepository(StudentEnrollmentDbContext db) : base(db)
    {
	}
}
