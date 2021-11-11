using InvoicesControl.Application.ViewModels;
using System;
using System.Threading.Tasks;

namespace InvoicesControl.Application.Interfaces.Services
{
    public interface ISettingsService : IDisposable
    {
        Task<bool> Update(SettingsEditVm settingsEditVm);
        Task<SettingsDetailsVm> Get();
    }
}
