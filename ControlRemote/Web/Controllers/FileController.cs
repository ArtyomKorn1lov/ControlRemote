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
using Microsoft.EntityFrameworkCore;
using Web.DtoConverter;
using Microsoft.AspNetCore.StaticFiles;

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
        private IFileService _fileService;
        private string _currentDirectory = "/Files/";

        public FileController(IUnitOfWork unitOfWork, ControlRemoteDbContext controlRemoteDbContext, 
            IWebHostEnvironment appEnvironment, IUserService userService, IFileService fileService)
        {
            _unitOfWork = unitOfWork;
            _controlRemoteDbContext = controlRemoteDbContext;
            _appEnvironment = appEnvironment;
            _userServicel = userService;
            _fileService = fileService;
        }

        [Authorize(Roles = "manager")]
        [HttpPost("create")]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> AddFile()
        {
            try
            {
                IFormFile uploadFile = Request.Form.Files[0];
                if (uploadFile != null)
                {
                    string path = _currentDirectory + uploadFile.FileName;
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
                return BadRequest("error");
            }
        }

        [Authorize(Roles = "manager")]
        [HttpGet("file-list")]
        public async Task<List<FileInfoModel>> GetFileNames()
        {
            int id = await _userServicel.GetIdByUserLogin(HttpContext.User.Identity.Name);
            List<FileEntity> fileEntities = await _controlRemoteDbContext.Set<FileEntity>().Where(f => f.MangerId == id).ToListAsync();
            List<FileInfoModel> fileInfoModels = fileEntities.Select(f => FileDtoConverter.ConvertEntityToModel(f)).ToList();
            return fileInfoModels;
        }

        [Authorize(Roles = "manager")]
        [HttpDelete("remove/{id}")]
        public async Task<IActionResult> RemoveFile(int id)
        {
            try
            {
                int userId = await _userServicel.GetIdByUserLogin(HttpContext.User.Identity.Name);
                FileEntity fileEntity = await _controlRemoteDbContext.Set<FileEntity>().FirstOrDefaultAsync(f => f.Id == id);
                string path = _appEnvironment.WebRootPath + fileEntity.Path;
                if (userId == fileEntity.MangerId)
                {
                    _fileService.DeleteFile(path);
                    if (fileEntity != null)
                        _controlRemoteDbContext.Set<FileEntity>().Remove(fileEntity);
                    await _unitOfWork.Commit();
                    return Ok("success");
                }
                return Ok("error");
            }
            catch
            {
                return BadRequest("error");
            }
        }

        [Authorize(Roles = "manager")]
        [HttpGet("download/{id}")]
        public async Task<FileResult> DownloadFile(int id)
        {
            int userId = await _userServicel.GetIdByUserLogin(HttpContext.User.Identity.Name);
            FileEntity fileEntity = await _controlRemoteDbContext.Set<FileEntity>().FirstOrDefaultAsync(f => f.Id == id);
            string path = _appEnvironment.WebRootPath + fileEntity.Path;
            string content = "";
            new FileExtensionContentTypeProvider().TryGetContentType(fileEntity.Name, out content);
            byte[] byteArray = await _fileService.ReadFile(path);
            FileContentResult fileContentResult = new FileContentResult(byteArray, content)
            {
                FileDownloadName = fileEntity.Name,
            };
            return fileContentResult;
        }
    }
}
