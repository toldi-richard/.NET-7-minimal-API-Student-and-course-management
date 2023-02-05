using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StudentEnrollment.DATA.Configuration;

internal class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.HasData
        (
            new Course
            {
                Id = 1,
                Title = "Test1",
                Credits = 3
            },
            new Course
            {
                Id = 2,
                Title = "Test2",
                Credits = 5
            }
        );
    }
}
