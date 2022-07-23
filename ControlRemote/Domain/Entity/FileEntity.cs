using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class FileEntity
    {
        public int Id { get; set; }
        public int MangerId { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
    }
}
