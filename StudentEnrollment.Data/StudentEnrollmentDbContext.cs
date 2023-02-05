using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using StudentEnrollment.DATA.Configuration;

namespace StudentEnrollment.DATA;

public class StudentEnrollmentDbContext : IdentityDbContext<SchoolUser>
{
    public StudentEnrollmentDbContext(DbContextOptions<StudentEnrollmentDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfiguration(new CourseConfiguration());
        builder.ApplyConfiguration(new UserRoleConfiguration());
        builder.ApplyConfiguration(new SchoolUserRoleConfiguration());
        builder.ApplyConfiguration(new RoleConfiguration());
    }

    public DbSet<Course> Courses { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }

    public class StundentEnrollmentDbContextFactory : IDesignTimeDbContextFactory<StudentEnrollmentDbContext>
    {
        public StudentEnrollmentDbContext CreateDbContext(string[] args)
        {
            // Get environment
            //string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            // Build config
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // Get connection string
            var optionsBuilder = new DbContextOptionsBuilder<StudentEnrollmentDbContext>();
            var connectionString = config.GetConnectionString("SchoolDbConnection");
            optionsBuilder.UseSqlServer(connectionString);
            return new StudentEnrollmentDbContext(optionsBuilder.Options);
        }
    }
}
