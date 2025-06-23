using LifeInsurance.Application.Authentication.Services;
using LifeInsurance.Application.Common.Settings;
using LifeInsurance.Infrastructure.Identity;
using LifeInsurance.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LifeInsurance.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JwtSettings _jwtSettings;
        private readonly AppIdentityDbContext _appIdentityDbContext;

        public AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IOptions<JwtSettings> jwtOptions, AppIdentityDbContext appIdentityDbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtSettings = jwtOptions.Value;
            _appIdentityDbContext = appIdentityDbContext;
        }

        public async Task<string> LoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user is null)
                throw new UnauthorizedAccessException("Credenciales inválidas");

            var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);

            if (!result.Succeeded)
                throw new UnauthorizedAccessException("Credenciales inválidas");

            return await GenerateJwtTokenAsync(user);
        }

        public async Task<string> RegisterAsync(string email, string password, string firstName, string lastName)
        {
            using var transaction = await _appIdentityDbContext.Database.BeginTransactionAsync();

            try
            {
                var user = new ApplicationUser 
                { 
                    UserName = email,
                    Email = email,
                    EmailConfirmed = true,
                    FirstName = firstName,
                    LastName = lastName
                };

                var result = await _userManager.CreateAsync(user, password);

                if (!result.Succeeded)
                    throw new ApplicationException("Error al crear usuario: " + string.Join(", ", result.Errors.Select(e => e.Description)));

                var roleResult = await _userManager.AddToRoleAsync(user, "User");

                if (!roleResult.Succeeded)
                    throw new ApplicationException("Error al asignar rol: " + string.Join(", ", roleResult.Errors.Select(e => e.Description)));

                await transaction.CommitAsync();
                return "Usuario registrado correctamente.";
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
            
        }

        private async Task<string> GenerateJwtTokenAsync(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Name, user.UserName ?? "")
            };

            var roles = await _userManager.GetRolesAsync(user);
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
