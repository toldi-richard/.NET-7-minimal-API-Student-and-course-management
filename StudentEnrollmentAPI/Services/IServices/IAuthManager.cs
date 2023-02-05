using Microsoft.AspNetCore.Identity;
using StudentEnrollment.API.DTO.Authentication;

namespace StudentEnrollment.API.Services.IServices;

public interface IAuthManager
{
    Task<AuthResponseDto> Login(LoginDto loginDto);
    Task<IEnumerable<IdentityError>> Register(RegisterDto registerDto);
}
