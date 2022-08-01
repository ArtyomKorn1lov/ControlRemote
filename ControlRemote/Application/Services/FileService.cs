using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class FileService : IFileService
    {
        public void DeleteFile(string path)
        {
            File.Delete(path);
        }

        public async Task<byte[]> ReadFile(string path)
        {
            return await File.ReadAllBytesAsync(path);
        }
    }
}
