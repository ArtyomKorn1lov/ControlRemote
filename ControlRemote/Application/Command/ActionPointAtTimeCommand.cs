using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Command
{
    public class ActionPointAtTimeCommand
    {
        public DateTime HourTimeAction { get; set; }
        public byte FlagImg { get; set; }
        public bool EnableAction { get; set; }
    }
}
