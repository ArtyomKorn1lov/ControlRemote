using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Dto
{
    public class ActionPointModel
    {
        public int Id { get; set; }
        public string UserLogin { get; set; }
        public DateTime DateTimeAction { get; set; }
        public string Station { get; set; }
        public byte FlagImg { get; set; }
    }
}
