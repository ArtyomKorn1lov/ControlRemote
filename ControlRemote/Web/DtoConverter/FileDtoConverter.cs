using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entity;
using Web.Dto;

namespace Web.DtoConverter
{
    public static class FileDtoConverter
    {
        public static FileInfoModel ConvertEntityToModel(FileEntity fileEntity)
        {
            if(fileEntity == null)
            {
                return null;
            }
            return new FileInfoModel
            {
                Id = fileEntity.Id,
                Name = fileEntity.Name
            };
        }
    }
}
