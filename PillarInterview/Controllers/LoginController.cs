using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PillarInterview.Models.AccountViewModels;
using Microsoft.AspNetCore.Identity;
using PillarInterview.Data.Models;
using PillarInterview.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace PillarInterview.Controllers
{
   
    [Route("api/login/[action]")]
    public class LoginController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;


        public LoginController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IEmailSender emailSender,
            IConfiguration configuration,
            ILogger<LoginController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _configuration = configuration;
            _logger = logger;
        }
        
        [HttpPost]
        public async Task<IActionResult> LoginRequest([FromBody] LoginByUsernameViewModel model)
        {
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    var appUser = _userManager.Users.SingleOrDefault(r => r.UserName == model.Username);
                    var roles = await _userManager.GetRolesAsync(appUser);
                    string token = JWTGenerator.Generate(appUser.Email, appUser, roles, _configuration);
                    _logger.LogInformation("User logged in.");
                    return new OkObjectResult(new { Token = token, Roles = roles, UserName = appUser.Name });
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return new BadRequestObjectResult("Invalid login or password");
                }
            }
            else return new BadRequestObjectResult("Invalid data");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            return new OkResult();
        }

       
    }
}