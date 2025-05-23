using AppLogic.Mapper;
using BusinessLogic.RepositoriesInterfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SharedUseCase.DTOs.User;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IRepoUser _repoUser;
    private readonly IConfiguration _config;

    public AuthController(IRepoUser repoUser, IConfiguration config)
    {
        _repoUser = repoUser;
        _config = config;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        try { 
        // Busca el usuario por email
        var user = _repoUser.GetByEmail(request.Email);
        if (user == null || user.Password.Value != request.Password)
            return Unauthorized("Credenciales inválidas");

        // Genera el token JWT
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.Email.Value),
            new Claim("Rol", user.Rol)
        };

        JwtSecurityToken token = null;
        try
        {
            var keyString = Environment.GetEnvironmentVariable("JWT_KEY") ?? _config["Jwt:Key"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: null,
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);
        }
        catch (Exception ex)
        {
            return Unauthorized($"Error al generar el token. Detalle: {ex.Message} {(ex.InnerException != null ? "Inner: " + ex.InnerException.Message : "")}");
        }

        // Corrige la inicialización de UserDto proporcionando todos los argumentos requeridos
        var userData = new UserDto
        (
            user.Id,
            user.Ci,
            user.Name.Value,
            user.Email.Value,
            user.Password.Value,
            user.Phone,
            user.Rol
        );

        return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token), userData });
        }
        
        catch (Exception ex)
        {
            var innerMessage = ex.InnerException != null ? $" Inner: {ex.InnerException.Message}" : "";
            return Unauthorized($"Error al iniciar sesión. Detalle: {ex.Message}{innerMessage}");
        }
    }
}

