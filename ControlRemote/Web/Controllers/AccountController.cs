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
        public async Task<string> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                if(model.Login == _configuration.GetConnectionString("AdminLogin") && model.Password == _configuration.GetConnectionString("AdminPassword"))
                {
                    await Authenticate(_configuration.GetConnectionString("AdminLogin"), "admin");
                    return "success";
                }
                User user = await _controlRemoteDbContext.Set<User>().FirstOrDefaultAsync(u => u.Login == model.Login && u.Password == model.Password);
                UserDto userDto = UserDtoConverter.ConvertToUserDto(user);
                if (userDto != null)
                {
                    await Authenticate(model.Login, userDto.Role); // аутентификация

                    return "success";
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return "error";
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
        public async Task<string> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _controlRemoteDbContext.Set<User>().FirstOrDefaultAsync(u => u.Login == model.Login);
                UserDto userDto = UserDtoConverter.ConvertToUserDto(user);
                if (userDto == null)
                {
                    // добавляем пользователя в бд
                    User new_user = UserDtoConverter.CreateNewUser(model);
                    _controlRemoteDbContext.Set<User>().Add(new_user);
                    await _controlRemoteDbContext.SaveChangesAsync();

                    await Authenticate(model.Login, new_user.Role); // аутентификация

                    return "success";
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return "error";
        }

        [HttpPost("logout")]
        public async Task<string> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return "success";
        }

        [HttpGet("is-authorized")]
        public string IsUserAuthorized()
        {
            return HttpContext.User.Identity.Name;
        }
    }
}
