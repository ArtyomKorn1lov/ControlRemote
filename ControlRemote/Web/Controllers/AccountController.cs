using Domain.Entity;
using Infrastructure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Web.Dto;
using Web.DtoConverter;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : Controller
    {
        private ControlRemoteDbContext _controlRemoteDbContext;
        public IConfiguration _configuration { get; }

        public AccountController(ControlRemoteDbContext controlRemoteDbContext, IConfiguration configuration)
        {
            _controlRemoteDbContext = controlRemoteDbContext;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                if(model.Login == _configuration.GetConnectionString("AdminLogin") && model.Password == _configuration.GetConnectionString("AdminPassword"))
                {
                    await Authenticate(_configuration.GetConnectionString("AdminLogin"), "admin");
                    return Ok("success");
                }
                User user = await _controlRemoteDbContext.Set<User>().FirstOrDefaultAsync(u => u.Login == model.Login && u.Password == model.Password);
                UserDto userDto = UserDtoConverter.ConvertToUserDto(user);
                if (userDto != null)
                {
                    await Authenticate(model.Login, userDto.Role);

                    return Ok("success");
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return BadRequest("error");
        }

        private async Task Authenticate(string userName, string role)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, role)
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Login == _configuration.GetConnectionString("AdminLogin"))
                {
                    return BadRequest("error");
                }
                User user = await _controlRemoteDbContext.Set<User>().FirstOrDefaultAsync(u => u.Login == model.Login);
                UserDto userDto = UserDtoConverter.ConvertToUserDto(user);
                if (userDto == null)
                {
                    User new_user = UserDtoConverter.CreateNewUser(model);
                    _controlRemoteDbContext.Set<User>().Add(new_user);
                    await _controlRemoteDbContext.SaveChangesAsync();

                    await Authenticate(model.Login, new_user.Role);

                    return Ok("success");
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return BadRequest("error");
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return Ok("success");
            }
            catch
            {
                return BadRequest("error");
            }
        }

        [HttpGet("is-authorized")]
        public AuthoriseModel IsUserAuthorized()
        {
            AuthoriseModel authorise = new AuthoriseModel(HttpContext.User.Identity.Name, HttpContext.User.Identity.AuthenticationType);
            return authorise;
        }
    }
}
