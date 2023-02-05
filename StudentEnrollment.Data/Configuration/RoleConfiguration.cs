using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StudentEnrollment.DATA.Configuration;

internal class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        builder.HasData
        (
            new IdentityRole
            {
                Id = "57847633-c07e-4953-8eda-fc653de57353",
                Name = "Aministrator",
                NormalizedName = "ADMINISTRATOR"
            },
            new IdentityRole
            {
                Id = "eb521001-11c8-4eae-a29d-e3acea410518",
                Name = "User",
                NormalizedName = "USER"
            }
        );
    }
}
