using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StudentEnrollment.DATA.Configuration;

internal class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
    {
        builder.HasData
        (
            new IdentityUserRole<string>
            {
                RoleId = "57847633-c07e-4953-8eda-fc653de57353",
                UserId = "408aa945-3d84-4421-8342-7269ec64d949"
            },
            new IdentityUserRole<string>
            {
                RoleId = "eb521001-11c8-4eae-a29d-e3acea410518",
                UserId = "3f4631bd-f907-4409-b416-ba356312e659"
            }
        );
    }
}