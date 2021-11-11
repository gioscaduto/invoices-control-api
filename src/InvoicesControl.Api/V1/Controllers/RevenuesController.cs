using InvoicesControl.Api.Controllers;
using InvoicesControl.Application.Interfaces;
using InvoicesControl.Application.Interfaces.Services;
using InvoicesControl.Application.Notifications;
using InvoicesControl.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace InvoicesControl.Api.V1.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/revenues")]
    public class RevenuesController : MainController
    {
        private readonly IRevenueService _revenueService;

        public RevenuesController(INotifier notifier, IUser appUser, IRevenueService revenueService) 
            : base(notifier, appUser)
        {
            _revenueService = revenueService;
        }


        [HttpPost("{customerId:guid}")]
        public async Task<ActionResult> Post(Guid customerId, [FromBody] RevenueVm revenueVm)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var id = await _revenueService.Add(customerId, revenueVm);
            
            if (id != null) return CustomResponse(FormatCreatedResponse(id.Value), HttpStatusCode.Created);
            
            return CustomResponse();
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Put(Guid id, [FromBody] RevenueEditVm revenueVm)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            if (AreIdsDifferents(id, revenueVm.Id)) return CustomResponse();

            var succeeded = await _revenueService.Update(revenueVm);

            if (succeeded) return CustomResponse(successStatusCode: HttpStatusCode.NoContent);

            return CustomResponse();
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult> Get(Guid id)
        {
            var revenue = await _revenueService.Get(id);

            if (revenue == null) return CustomResponse(errorStatusCode: HttpStatusCode.NotFound);

            return CustomResponse(revenue);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var succeeded = await _revenueService.Delete(id);

            if(succeeded) return CustomResponse();

            return CustomResponse(errorStatusCode: HttpStatusCode.NotFound);
        }
    }
}
