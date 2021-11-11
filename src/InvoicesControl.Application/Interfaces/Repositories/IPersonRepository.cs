using InvoicesControl.Domain.Entities;
using System.Threading.Tasks;

namespace InvoicesControl.Application.Interfaces.Repositories
{
    public interface IPersonRepository : IRepository<Person>
    {
        public Task<Person> GetByUserId(string id);
    }
}
