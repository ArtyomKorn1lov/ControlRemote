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
        public Task Create(Employer employer)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Employer>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Employer> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task Update(Employer employer)
        {
            throw new NotImplementedException();
        }
    }
}
