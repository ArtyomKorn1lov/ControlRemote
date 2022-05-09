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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                if(model.Login == _configuration.GetConnectionString("AdminLogin") && model.Password == _configuration.GetConnectionString("AdminPassword"))
                {
                    await Authenticate(_configuration.GetConnectionString("AdminLogin"), _configuration.GetConnectionString("AdminPassword"));
                    return Ok("success");
                }
                User user = await _controlRemoteDbContext.Set<User>().FirstOrDefaultAsync(u => u.Login == model.Login && u.Password == model.Password);
                UserDto userDto = UserDtoConverter.ConvertToUserDto(user);
                if (userDto != null)
                {
                    await Authenticate(model.Login, userDto.Role); // аутентификация

                    return Ok("success");
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return BadRequest("error");
        }

        private async Task Authenticate(string userName, string role)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, role)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        [HttpPost("register")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _controlRemoteDbContext.Set<User>().FirstOrDefaultAsync(u => u.Login == model.Login);
                UserDto userDto = UserDtoConverter.ConvertToUserDto(user);
                if (userDto == null)
                {
                    // добавляем пользователя в бд
                    User new_user = UserDtoConverter.ConvertToUserEntiy(userDto);
                    _controlRemoteDbContext.Set<User>().Add(new_user);
                    await _controlRemoteDbContext.SaveChangesAsync();

                    await Authenticate(model.Login, new_user.Role); // аутентификация

                    return Ok("success");
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return BadRequest("error");
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok("success");
        }

        [HttpGet("is-authorized")]
        [ValidateAntiForgeryToken]
        public string IsUserAuthorized()
        {
            return HttpContext.User.Identity.AuthenticationType;
        }
    }
}
