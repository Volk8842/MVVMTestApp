using System.Collections.Generic;
using System.Threading.Tasks;

using Common.Intefaces;

using Microsoft.EntityFrameworkCore;

using Models;
using Models.Context;

namespace Repositories
{
    public class PersonRepository : IPersonRepository
    {
        public async Task<Person> Find(int id)
        {
            using (var personContext = new PersonContext())
            {
                return await personContext.Persons.Include(p => p.Job).FirstOrDefaultAsync(p => p.Id == id);
            }
        }

        public async Task<List<Person>> GetAll()
        {
            using (var personContext = new PersonContext())
            {
                return await personContext.Persons.Include(p => p.Job).ToListAsync();
            }
        }
    }
}
