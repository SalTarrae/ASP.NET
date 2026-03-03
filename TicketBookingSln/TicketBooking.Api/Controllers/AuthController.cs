using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace TicketBooking.Api.Controllers {
	[ApiController]
	[Route("api/[controller]")]
	public class AuthController : ControllerBase {
		private readonly UserManager<IdentityUser> _userManager;
		private readonly IConfiguration _config;

		public AuthController(UserManager<IdentityUser> userManager, IConfiguration config) {
			_userManager = userManager;
			_config = config;
		}

		public class LoginRequest {
			[Required, EmailAddress]
			public string Email { get; set; } = string.Empty;

			[Required]
			public string Password { get; set; } = string.Empty;
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] LoginRequest model) {
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var user = await _userManager.FindByEmailAsync(model.Email);
			if (user == null)
				return Unauthorized("Invalid credentials");

			var valid = await _userManager.CheckPasswordAsync(user, model.Password);
			if (!valid)
				return Unauthorized("Invalid credentials");

			var claims = new List<Claim>
			{
				new Claim(JwtRegisteredClaimNames.Sub, user.Id),
				new Claim(JwtRegisteredClaimNames.Email, user.Email ?? ""),
				new Claim(ClaimTypes.Name, user.UserName ?? user.Email ?? ""),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
			};

			var roles = await _userManager.GetRolesAsync(user);
			foreach (var r in roles)
				claims.Add(new Claim(ClaimTypes.Role, r));

			var jwtSection = _config.GetSection("Jwt");
			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSection["Key"]!));

			var token = new JwtSecurityToken(
				issuer: jwtSection["Issuer"],
				audience: jwtSection["Audience"],
				claims: claims,
				expires: DateTime.UtcNow.AddMinutes(60),
				signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
			);

			return Ok(new {
				token = new JwtSecurityTokenHandler().WriteToken(token),
				expires = token.ValidTo
			});
		}

		public class RegisterRequest {
			[Required, EmailAddress]
			public string Email { get; set; } = string.Empty;

			[Required]
			public string Password { get; set; } = string.Empty;

			[Required]
			[Compare(nameof(Password))]
			public string ConfirmPassword { get; set; } = string.Empty;
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] RegisterRequest model) {
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var user = new IdentityUser {
				UserName = model.Email,
				Email = model.Email
			};

			var result = await _userManager.CreateAsync(user, model.Password);

			if (!result.Succeeded) {
				return BadRequest(new {
					errors = result.Errors.Select(e => e.Description).ToArray()
				});
			}

			return Ok(new { message = "User created", userId = user.Id });
		}
	}
}
