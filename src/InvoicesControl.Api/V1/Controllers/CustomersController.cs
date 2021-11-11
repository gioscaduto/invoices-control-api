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
    [Route("api/v{version:apiVersion}/customers")]
    public class CustomersController : MainController
    {
        private readonly ICustomerService _customerService;

        public CustomersController(INotifier notifier, IUser appUser, ICustomerService customerService) 
            : base(notifier, appUser)
        {
            _customerService = customerService;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CustomerVm customer)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var id = await _customerService.Add(customer);

            if (id != null) return CustomResponse(FormatCreatedResponse(id.Value), HttpStatusCode.Created);

            return CustomResponse();
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Put(Guid id, [FromBody] CustomerEditVm customerVm)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            if (AreIdsDifferents(id, customerVm.Id)) return CustomResponse();

            var succeeded = await _customerService.Update(customerVm);

            if (succeeded) return CustomResponse(successStatusCode: HttpStatusCode.NoContent);

            return CustomResponse();
        }

        [HttpPut("{id:guid}/activate")]
        public async Task<ActionResult> Activate(Guid id)
        {
            var succeeded = await _customerService.Activate(id);

            if (succeeded) return CustomResponse(successStatusCode: HttpStatusCode.NoContent);

            return CustomResponse();
        }

        [HttpPut("{id:guid}/inactivate")]
        public async Task<ActionResult> Inactivate(Guid id)
        {
            var succeeded = await _customerService.Inactivate(id);

            if (succeeded) return CustomResponse(successStatusCode: HttpStatusCode.NoContent);

            return CustomResponse();
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult> Get(Guid id)
        {
            var customer = await _customerService.Get(id);

            if (customer == null) return CustomResponse(errorStatusCode: HttpStatusCode.NotFound);

            return CustomResponse(customer);
        }

        [HttpGet]
        public async Task<ActionResult> Get([FromQuery] string legalName, [FromQuery] string document)
        {
            var customers = await _customerService.List(legalName, document);

            if (customers == null) return CustomResponse(errorStatusCode: HttpStatusCode.NotFound);

            return CustomResponse(customers);
        }
    }
}
