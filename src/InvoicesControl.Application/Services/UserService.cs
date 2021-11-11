using InvoicesControl.Application.Interfaces.Repositories;
using InvoicesControl.Application.Interfaces.Services;
using InvoicesControl.Application.Notifications;
using InvoicesControl.Application.ViewModels.Users;
using InvoicesControl.Domain.Entities.Validations;
using System;
using System.Threading.Tasks;

namespace InvoicesControl.Application.Services
{
    public class UserService : BaseService, IUserService
    {
        private readonly IPersonRepository _personRepository;

        public UserService(INotifier notifier, IPersonRepository personRepository) : base(notifier)
        {
            _personRepository = personRepository;
        }

        public async Task<bool> Add(UserVm userVm, string userId)
        {
            var person = userVm.ToPerson(userId);

            if (!ExecuteValidation(new PersonValidation(), person)) return false;

            await _personRepository.Add(person);
            return true;
        }

        public async Task<bool> Update(UserEditVm userVm)
        {
            var person = await _personRepository.GetByUserId(userVm.Id.ToString());

            if (person == null)
            {
                Notify("User not found");
                return false;
            }

            userVm.UpdatePerson(person);

            if (!ExecuteValidation(new PersonValidation(), person)) return false;

            await _personRepository.Update(person);
            return true;
        }

        public async Task<UserDetailsVm> Get(Guid id)
        {
            var person = await _personRepository.GetByUserId(id.ToString());

            if (person == null) return null;

            return new UserDetailsVm(person);
        }

        public void Dispose()
        {
            _personRepository?.Dispose();
        }
    }
}
