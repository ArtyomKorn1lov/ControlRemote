using Domain.Entity;
using Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class EmployerRepository : IEmployerRepository
    {
        public Task Create(Manager employer)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Manager>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Manager> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task Update(Manager employer)
        {
            throw new NotImplementedException();
        }
    }
}
