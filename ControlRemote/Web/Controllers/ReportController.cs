using Application;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Dto;
using Web.DtoConverter;
using Application.Command;

namespace Web.Controllers
{
    [ApiController]
    [Route("api/report")]
    public class ReportController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private IRequestService _requestService;

        public ReportController(IUnitOfWork unitOfWork, IRequestService requestService)
        {
            _unitOfWork = unitOfWork;
            _requestService = requestService;
        }

        [Authorize(Roles = "admin")]
        [HttpGet("report-list/{start}/{final}")]
        public async Task<List<ActionSortByUserLoginModel>> GetAllForTime(DateTime start, DateTime final)
        {
            List<ActionSortByUserLoginCommand> actionSortByUserLoginCommands = await _requestService.GetAllForTime(start, final);
            List<ActionSortByUserLoginModel> actionSortByUserLoginModels = actionSortByUserLoginCommands
                .Select(d => ActionPointDtoConverter.ConvertCommandToModel(d)).ToList();
            if(actionSortByUserLoginModels == null)
            {
                return null;
            }
            return actionSortByUserLoginModels;
        }

        [Authorize(Roles = "admin, manager")]
        [HttpGet("report-list{logins}/{start}/{final}")]
        public async Task<List<ActionSortByUserLoginModel>> GetByLoginEmployerForTime(List<string> logins, DateTime start, DateTime final)
        {
            List<ActionSortByUserLoginCommand> actionSortByUserLoginCommands = await _requestService.GetByLoginEmployerForTime(logins, start, final);
            List<ActionSortByUserLoginModel> actionSortByUserLoginModels = actionSortByUserLoginCommands
                .Select(d => ActionPointDtoConverter.ConvertCommandToModel(d)).ToList();
            if (actionSortByUserLoginModels == null)
            {
                return null;
            }
            return actionSortByUserLoginModels;
        }
    }
}
