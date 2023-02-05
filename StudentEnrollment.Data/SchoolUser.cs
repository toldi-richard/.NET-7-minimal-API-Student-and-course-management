using Microsoft.AspNetCore.Identity;

namespace StudentEnrollment.DATA;

public class SchoolUser : IdentityUser
{
    public string FirstName{ get; set; }
    public string LastName { get; set; }
    public DateTime? DateOfBirth { get; set; }
}
