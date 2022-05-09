using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepositories
{
    public interface IEmployerRepository
    {
        Task<List<User>> GetAll();
        Task<User> GetById(int id);
        Task Create(User employer);
        Task Update(User employer);
        Task Delete(int id);
    }
}
