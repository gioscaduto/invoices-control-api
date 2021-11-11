using InvoicesControl.Application.ViewModels.Users;
using System;
using System.Threading.Tasks;

namespace InvoicesControl.Application.Interfaces.Services
{
    public interface IUserService : IDisposable
    {
        public Task<bool> Add(UserVm user, string userId);
        public Task<bool> Update(UserEditVm userVm);
        public Task<UserDetailsVm> Get(Guid id);
    }
}
