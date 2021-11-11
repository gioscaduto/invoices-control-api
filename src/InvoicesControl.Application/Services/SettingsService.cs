using InvoicesControl.Application.Interfaces.Repositories;
using InvoicesControl.Application.Interfaces.Services;
using InvoicesControl.Application.Notifications;
using InvoicesControl.Application.ViewModels;
using InvoicesControl.Domain.Entities.Validations;
using System.Threading.Tasks;

namespace InvoicesControl.Application.Services
{
    public class SettingsService : BaseService, ISettingsService
    {
        private const string SETTINGS_NOT_FOUND_MESSAGE = "Settings not found";

        private readonly ISettingsRepository _settingsRepository;

        public SettingsService(INotifier notifier, ISettingsRepository settingsRepository) : base(notifier)
        {
            _settingsRepository = settingsRepository;
        }

        public async Task<bool> Update(SettingsEditVm settingsEditVm)
        {
            var settings = await _settingsRepository.Get();

            if (settings == null)
            {
                Notify(SETTINGS_NOT_FOUND_MESSAGE);
                return false;
            }

            settingsEditVm.UpdateSettings(settings);

            if (!ExecuteValidation(new SettingsValidation(), settings)) return false;

            await _settingsRepository.Update(settings);

            return true;
        }

        public async Task<SettingsDetailsVm> Get()
        {
            var settings = await _settingsRepository.Get();

            if (settings == null)
            {
                Notify(SETTINGS_NOT_FOUND_MESSAGE);
                return null;
            }

            return new SettingsDetailsVm(settings);
        }

        public void Dispose()
        {
            _settingsRepository?.Dispose();
        }
    }
}
