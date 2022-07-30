using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Domain.Entity;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;
using Application.Services;
using Web.Dto;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/file")]
    public class FileController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private ControlRemoteDbContext _controlRemoteDbContext;
        private IWebHostEnvironment _appEnvironment;
        private IUserService _userServicel;

        public FileController(IUnitOfWork unitOfWork, ControlRemoteDbContext controlRemoteDbContext, IWebHostEnvironment appEnvironment, IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _controlRemoteDbContext = controlRemoteDbContext;
            _appEnvironment = appEnvironment;
            _userServicel = userService;
        }

        [Authorize("manager")]
        [HttpPost("create")]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> AddFile()
        {
            try
            {
                IFormFile uploadFile = Request.Form.Files[0];
                if (uploadFile != null)
                {
                    string path = "/Files/" + uploadFile.FileName;
                    using(FileStream fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        await uploadFile.CopyToAsync(fileStream);
                    }
                    int id = await _userServicel.GetIdByUserLogin(HttpContext.User.Identity.Name);
                    FileEntity file = new FileEntity
                    {
                        MangerId = id,
                        Name = uploadFile.FileName,
                        Path = path
                    };
                    await _controlRemoteDbContext.Set<FileEntity>().AddAsync(file);
                    await _unitOfWork.Commit();
                    return Ok("success");
                }
                return Ok("error");
            }
            catch
            {
                return BadRequest("Error");
            }
        }
    }
}
