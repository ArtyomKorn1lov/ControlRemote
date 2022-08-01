﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IFileService
    {
        void DeleteFile(string path);
        Task<byte[]> ReadFile(string path);
    }
}
