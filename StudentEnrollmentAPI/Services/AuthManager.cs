using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using StudentEnrollment.API.DTO.Authentication;
using StudentEnrollment.API.Services.IServices;
using StudentEnrollment.DATA;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StudentEnrollment.API.Services;

public class AuthManager : IAuthManager
{
    private readonly UserManager<SchoolUser> _userManager;
    private readonly IConfiguration _configuration;

    public AuthManager(UserManager<SchoolUser> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }
    public async Task<AuthResponseDto> Login(LoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.EmailAddress);
        if (user is null)
        {
            return default;
        }

        bool isValidCredentials = await _userManager.CheckPasswordAsync(user, loginDto.Password);
        if (!isValidCredentials)
        {
            return default;
        }

        var token = await GenerateTokenAsync(user);

        return new AuthResponseDto
        {
            Token = token,
            UserId = user.Id
        };
    }

    public async Task<IEnumerable<IdentityError>> Register(RegisterDto registerDto)
    {
        var user = new SchoolUser
        {
            DateOfBirth = registerDto.DateOfBirth,
            Email = registerDto.EmailAddress,
            UserName = registerDto.EmailAddress,
            FirstName = registerDto.FirstName,
            LastName = registerDto.LastName
        };

        var result = await _userManager.CreateAsync(user, registerDto.Password);

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, "User");
        }

        return result.Errors;
    }

    private async Task<string> GenerateTokenAsync(SchoolUser user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var roles = await _userManager.GetRolesAsync(user);
        var roleClaims = roles.Select(x => new Claim(ClaimTypes.Role, x)).ToList();
        var userClaims = await _userManager.GetClaimsAsync(user);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim("userId", user.Id)
        }.Union(userClaims).Union(roleClaims);

        var token = new JwtSecurityToken
        (
            issuer: _configuration["JwtSettings:Issuer"],
            audience: _configuration["JwtSettings:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(Convert.ToInt32(_configuration["JwtSettings:DurationInHours"])),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
