using System;
using System.Linq;
using System.Threading.Tasks;
using Domain.App.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace WebApp.ApiControllers.Identity
{   
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IConfiguration _configuration;

        public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, ILogger<RegisterModel> logger, IConfiguration configuration)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] DTO.App.Login dto)
        {
            var appUser = await _userManager.FindByEmailAsync(dto.Email);
            if (appUser == null)
            {
                _logger.LogWarning("WebApi login failed. User {User} not found!", dto.Email);
                return NotFound(new DTO.App.Message("User/password error!"));
            }

            var result = await _signInManager.CheckPasswordSignInAsync(appUser, dto.Password, false);
            if (result.Succeeded)
            {
                var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(appUser);
                var jwt = Extensions.Base.IdentityExtensions.GenerateJwt(
                    claimsPrincipal.Claims,
                    _configuration["JWT:Key"],
                    _configuration["JWT:Issuer"],
                    _configuration["JWT:Issuer"],
                    DateTime.Now.AddDays(_configuration.GetValue<int>("JWT:ExpireDays"))
                );
                _logger.LogInformation("WebApi login. User {User}", dto.Email);
                return Ok(new DTO.App.JwtResponse()
                {
                    Token = jwt,
                    Firstname = appUser.Firstname,
                    Lastname = appUser.Lastname
                });
            }

            _logger.LogWarning("WebApi login failed. User {User}, bad password!", dto.Email);
            return NotFound(new DTO.App.Message("User/password error!"));
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] DTO.App.Register dto)
        {
            var appUser = await _userManager.FindByEmailAsync(dto.Email);

            if (appUser != null)
            {
                _logger.LogWarning("User {User} already registered", dto.Email);
                return BadRequest(new DTO.App.Message("User already registered"));
            }

            appUser = new Domain.App.Identity.AppUser()
            {
                Email = dto.Email,
                UserName = dto.Password,
                Firstname = dto.Firstname,
                Lastname = dto.Lastname
            };

            var result = await _userManager.CreateAsync(appUser, dto.Password);

            if (result.Succeeded)
            {
                _logger.LogInformation("User {User} created new account with password", dto.Password);

                var user = await _userManager.FindByEmailAsync(appUser.Email);
                if (user != null)
                {
                    var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(user);
                    var jwt = Extensions.Base.IdentityExtensions.GenerateJwt(
                        claimsPrincipal.Claims,
                        _configuration["JWT:Key"],
                        _configuration["JWT:Issuer"],
                        _configuration["JWT:Issuer"],
                        DateTime.Now.AddDays(_configuration.GetValue<int>("JWT:ExpireDays"))
                    );
                    _logger.LogInformation("WebApi login. User {User}", dto.Email);
                    return Ok(new DTO.App.JwtResponse()
                    {
                        Token = jwt,
                        Firstname = appUser.Firstname,
                        Lastname = appUser.Lastname
                    });
                }
                _logger.LogInformation("User {Email} not found after creation", appUser.Email);
                return BadRequest(new DTO.App.Message("User not found after creation!"));
            }

            var errors = result.Errors.Select(error => error.Description).ToList();
            return BadRequest(new DTO.App.Message() {Messages = errors}); 
        }
    }
}