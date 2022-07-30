using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Dto
{
    public class UploadFileModel
    {
        public string NameFile { get; set; }
        public IFormFile UploadedFile { get; set; }
    }
}
