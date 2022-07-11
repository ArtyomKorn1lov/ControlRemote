﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Command
{
    public class ActionSortByDateTimeCommand
    {
        public DateTime DateTimeAction { get; set; }
        public List<ActionPointAtTimeCommand> Commands { get; set; }
    }
}
