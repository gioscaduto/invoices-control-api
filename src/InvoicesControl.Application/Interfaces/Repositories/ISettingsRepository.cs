using InvoicesControl.Domain.Entities;
using System.Threading.Tasks;

namespace InvoicesControl.Application.Interfaces.Repositories
{
    public interface ISettingsRepository : IRepository<Settings>
    {
        public Task<Settings> Get();
    }
}
