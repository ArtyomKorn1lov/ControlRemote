using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Dto
{
    public class ActionSortByDateTimeModel
    {
        public DateTime DateTimeAction { get; set; }
        public List<ActionPointAtTimeModel> Commands { get; set; }
    }
}
