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
        Task<List<Manager>> GetAll();
        Task<Manager> GetById(int id);
        Task Create(Manager employer);
        Task Update(Manager employer);
        Task Delete(int id);
    }
}
