using InvoicesControl.Api.Controllers;
using InvoicesControl.Application.Interfaces;
using InvoicesControl.Application.Interfaces.Services;
using InvoicesControl.Application.Notifications;
using InvoicesControl.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace InvoicesControl.Api.V1.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/settings")]
    public class SettingsController : MainController
    {
        private readonly ISettingsService _settingsService;

        public SettingsController(INotifier notifier, IUser appUser, ISettingsService settingsService) 
            : base(notifier, appUser)
        {
            _settingsService = settingsService;
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] SettingsEditVm settings)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var succeeded = await _settingsService.Update(settings);

            if (succeeded) return CustomResponse(successStatusCode: HttpStatusCode.NoContent);

            return CustomResponse();
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var settings = await _settingsService.Get();

            if (settings == null) return NotFound();

            return CustomResponse(settings);
        }
    }
}
