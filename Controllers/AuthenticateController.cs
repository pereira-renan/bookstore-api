using bookstore_api;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWTAuthentication.NET6._0.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class AuthenticateController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AuthenticateController(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        [EnableCors("AllowAll")]
        [HttpPost]
        [Route("Login")]
        [ProducesResponseType(typeof(LoginResponseModel_OK), StatusCodes.Status200OK)]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = GetToken(authClaims);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return Unauthorized();
        }

        [EnableCors("AllowAll")]
        [HttpPost]
        [Route("Register")]
        [ProducesResponseType(typeof(RegisterResponseModel_OK), StatusCodes.Status200OK)]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "This username is already in use." });

            var emailExists = await _userManager.FindByEmailAsync(model.Email);
            if (emailExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "This email is already in use." });

            IdentityUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }

        [Authorize(Roles = "Admin")]
        [EnableCors("AllowAll")]
        [HttpPost]
        [Route("RegisterAdmin")]
        [ProducesResponseType(typeof(RegisterResponseModel_OK), StatusCodes.Status200OK)]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

            IdentityUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));

            if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.Admin);
            }
            if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.User);
            }
            return Ok(new Response { Status = "Success", Message = "User created successfully." });
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }

        [HttpPost]
        [Route("ResetPassword")]
        [ProducesResponseType(typeof(ResetPasswordModel_OK), StatusCodes.Status200OK)]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.Action("ResetPassword", "Authenticate", new { userId = user.Id, resetToken = code }, protocol: HttpContext.Request.Scheme);
                string[] newCallbackUrl = callbackUrl.Split('?');

                //TBD add email service
                //await _emailSender.SendEmailAsync(model.Email, "Reset Password",
                //    $"Please reset your password by clicking here: <a href='{callbackUrl}'>link</a>");
                return Ok(new ResetPasswordModel_OK { Status = "Success", Message = "Email sent!", Callback_URL = newCallbackUrl[1], Code = code });
            }
            else
            {
                return Ok(new ResetPasswordModel_OK { Status = "Success", Message = "Email sent!", Callback_URL = null, Code = null });
            }
        }

        [HttpPost]
        [Route("ConfirmNewPassword")]
        [ProducesResponseType(typeof(ConfirmNewPasswordModel_OK), StatusCodes.Status200OK)]
        public async Task<IActionResult> ConfirmNewPassword([FromBody] ConfirmNewPasswordModel model)
        {
            var user = await _userManager.FindByIdAsync(model.User);
            if (user != null)
            {
                var result = await _userManager.ResetPasswordAsync(user, model.Code, model.NewPassword);
                if (result.Succeeded)
                {
                    return Ok(new ConfirmNewPasswordModel_OK { Status = "Success", Message = "Your password has been resetted." });
                }
                else
                {
                    return Unauthorized(new ConfirmNewPasswordModel_OK { Status = "Error", Message = "Your password reset request has failed." });
                }
            }
            else
            {
                return NotFound(new ConfirmNewPasswordModel_OK { Status = "Error", Message = "This username does not exist." });
            }
        }
    }
}