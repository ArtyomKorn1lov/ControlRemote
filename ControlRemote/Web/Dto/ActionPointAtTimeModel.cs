﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Dto
{
    public class ActionPointAtTimeModel
    {
        public DateTime HourTimeAction { get; set; }
        public byte FlagImg { get; set; }
        public bool EnableAction { get; set; }
    }
}
