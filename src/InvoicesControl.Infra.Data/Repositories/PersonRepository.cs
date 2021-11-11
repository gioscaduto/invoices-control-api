using InvoicesControl.Application.Interfaces.Repositories;
using InvoicesControl.Domain.Entities;
using InvoicesControl.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace InvoicesControl.Infra.Data.Repositories
{
    public class PersonRepository : Repository<Person>, IPersonRepository
    {
        public PersonRepository(ApplicationDbContext db) : base(db)
        {
        }

        public async Task<Person> GetByUserId(string id)
        {
            return await Db.Persons.FirstOrDefaultAsync(p => p.UserId == id);
        }
    }
}
