using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.api.Models.DTO;
using NZWalks.api.Repositories;

namespace NZWalks.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto) 
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDto.Username,
                Email = registerRequestDto.Username
            };

            var identityResult = await userManager.CreateAsync(identityUser, registerRequestDto.Password);

            if (identityResult.Succeeded)
            {
                // If there are roles specified, add the user to all roles at once
                if (registerRequestDto.Roles != null && registerRequestDto.Roles.Any())
                {
                    // Add the user to multiple roles in a single call
                    var roleResult = await userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);

                    // Check if adding to roles succeeded
                    if (roleResult.Succeeded)
                    {
                        return Ok("User registered successfully with roles. Please log in.");
                    }

                    return BadRequest("Failed to add user to one or more roles.");
                }

                // If no roles are specified, the user is registered successfully without roles
                return Ok("User registered successfully. Please log in.");
            }

            return BadRequest("Something went wrong");
        
        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var user = await userManager.FindByEmailAsync(loginRequestDto.Username);
            if (user != null) 
            {
                var checkpasswordResult = await userManager.CheckPasswordAsync(user, loginRequestDto.Password);
                if (checkpasswordResult)
                {
                    //Get Roles for this user
                    var roles = await userManager.GetRolesAsync(user);
                    //Create Token.
                    if (roles != null)
                    {
                      var jwtToken = tokenRepository.CreateJWTToken(user, roles.ToList());
                        var response = new LoginResponseDto
                        {
                            JwtToken = jwtToken,
                        };
                        return Ok(response);
                    }
                }
            }
            return BadRequest("User Name or Password Incorrect");
        }

    }
}
