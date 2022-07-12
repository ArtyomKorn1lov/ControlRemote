﻿using Application.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.IRepositories;
using Domain.Entity;
using Application.CommandConverter;

namespace Application.Services
{
    public class RequestService : IRequestService
    {
        private const int HourFormat = 24;
        private IRequestRepository _requestRepository;

        public RequestService(IRequestRepository requestRepository)
        {
            _requestRepository = requestRepository;
        }

        public async Task<List<ActionSortByUserLoginCommand>> GetAllForTime(DateTime startDateTime, DateTime finalDateTime)
        {
            try
            {
                List<ActionPoint> actionPoints = await _requestRepository.GetAllForTime(startDateTime, finalDateTime);
                List<ActionPointCommand> actionPointCommands = actionPoints.Select(data => ActionPointCommandConverter.ConvertEntityToCommand(data)).ToList();
                List<ActionSortByUserLoginCommand> actionSortByUserLoginCommands = GenerateReport(actionPointCommands);
                return actionSortByUserLoginCommands;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<ActionSortByUserLoginCommand>> GetByLoginEmployerForTime(List<string> logins, DateTime startDateTime, DateTime finalDateTime)
        {
            try
            {
                if(logins == null)
                {
                    return null;
                }
                List<ActionPoint> actionPoints = new List<ActionPoint>();
                for(int count = 0; count < logins.Count; count++)
                {
                    actionPoints = actionPoints.Concat(await _requestRepository.GetByLoginEmployerForTime(logins[count], startDateTime, finalDateTime)).ToList();
                }
                List<ActionPointCommand> actionPointCommands = actionPoints.Select(data => ActionPointCommandConverter.ConvertEntityToCommand(data)).ToList();
                List<ActionSortByUserLoginCommand> actionSortByUserLoginCommands = GenerateReport(actionPointCommands);
                return actionSortByUserLoginCommands;
            }
            catch
            {
                return null;
            }
        }

        public List<ActionSortByUserLoginCommand> GenerateReport(List<ActionPointCommand> actionPointCommands)
        {
            List<ActionSortByUserLoginCommand> actionSortByUserLoginCommands = new List<ActionSortByUserLoginCommand>();
            string currentLogin = "";
            for(int count = 0; count < actionPointCommands.Count; count++)
            {
                if(currentLogin != actionPointCommands[count].UserLogin)
                {
                    currentLogin = actionPointCommands[count].UserLogin;
                    List<ActionPointCommand> _actionPointCommands = actionPointCommands.Where(d => d.UserLogin == currentLogin).ToList();
                    actionSortByUserLoginCommands.Add(new ActionSortByUserLoginCommand
                    {
                        UserLogin = actionPointCommands[count].UserLogin,
                        Commands = GenerateActionSortByDateTimeCommands(_actionPointCommands)
                    });
                }
            }
            return actionSortByUserLoginCommands;
        }

        public List<ActionSortByDateTimeCommand> GenerateActionSortByDateTimeCommands(List<ActionPointCommand> actionPointCommands)
        {
            List<ActionSortByDateTimeCommand> actionSortByDateTimeCommands = new List<ActionSortByDateTimeCommand>();
            DateTime dateTime = new DateTime();
            for(int count = 0; count < actionPointCommands.Count; count++)
            {
                if(dateTime != actionPointCommands[count].DateTimeAction.Date)
                {
                    dateTime = actionPointCommands[count].DateTimeAction.Date;
                    List<ActionPointCommand> _actionPointCommands = actionPointCommands.Where(d => d.DateTimeAction.Date == dateTime).ToList();
                    actionSortByDateTimeCommands.Add(new ActionSortByDateTimeCommand 
                    {
                        DateTimeAction = actionPointCommands[count].DateTimeAction,
                        Commands = GenerateActionPointAtTimeCommand(_actionPointCommands)
                    });
                }
            }
            return actionSortByDateTimeCommands;
        }

        public List<ActionPointAtTimeCommand> GenerateActionPointAtTimeCommand(List<ActionPointCommand> actionPointCommands)
        {
            List<ActionPointAtTimeCommand> actionPointAtTimeCommands = new List<ActionPointAtTimeCommand>();
            int hourTime = 0;
            int action_count = 0;
            for(int count = 0; count < HourFormat; count++)
            {
                hourTime = count;
                if(hourTime == actionPointCommands[action_count].DateTimeAction.Hour)
                {
                    actionPointAtTimeCommands.Add(new ActionPointAtTimeCommand
                    {
                        HourTimeAction = actionPointCommands[action_count].DateTimeAction,
                        FlagImg = actionPointCommands[action_count].FlagImg,
                        EnableAction = true
                    });
                    if(action_count + 1 < actionPointCommands.Count)
                    {
                        action_count++;
                    }
                }
                else
                {
                    actionPointAtTimeCommands.Add(new ActionPointAtTimeCommand
                    {
                        HourTimeAction = actionPointCommands[action_count].DateTimeAction,
                        FlagImg = actionPointCommands[action_count].FlagImg,
                        EnableAction = false
                    });
                }
            }
            return actionPointAtTimeCommands;
        }
    }
}
