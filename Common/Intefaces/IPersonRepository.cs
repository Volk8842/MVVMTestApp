using System.Collections.Generic;
using System.Threading.Tasks;

using Models;

namespace Common.Intefaces
{
    public interface IPersonRepository
    {
        Task<List<Person>> GetAll();

        Task<Person> Find(int id);

        Task Modify(Person person);
    }
}
