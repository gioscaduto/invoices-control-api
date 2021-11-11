using InvoicesControl.Application.Interfaces.Repositories;
using InvoicesControl.Domain.Entities;
using InvoicesControl.Infra.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace InvoicesControl.Infra.Data.Repositories
{
    public class SettingsRepository : Repository<Settings>, ISettingsRepository
    {
        public SettingsRepository(ApplicationDbContext db) : base(db)
        {
        }

        public async Task<Settings> Get()
        {
            return await Db.Settings.FirstOrDefaultAsync(s => !s.Deleted);
        }
    }
}
