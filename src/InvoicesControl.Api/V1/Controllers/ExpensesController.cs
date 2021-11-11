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
    [Route("api/v{version:apiVersion}/expenses")]
    public class ExpensesController : MainController
    {
        private readonly IExpenseService _expenseService;

        public ExpensesController(INotifier notifier, IUser appUser, IExpenseService expenseService) 
            : base(notifier, appUser)
        {
            _expenseService = expenseService;
        }

        [HttpPost("{categoryId:Guid}")]
        public async Task<ActionResult> Post(Guid categoryId, [FromBody] ExpenseVm expenseVm)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var id = await _expenseService.Add(categoryId, expenseVm);

            if (id != null) return CustomResponse(FormatCreatedResponse(id.Value), HttpStatusCode.Created);

            return CustomResponse();
        }

        [HttpPut("{id:Guid}")]
        public async Task<ActionResult> Put(Guid id, [FromBody] ExpenseEditVm expenseVm)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            if (AreIdsDifferents(id, expenseVm.Id)) return CustomResponse();

            var succeeded = await _expenseService.Update(expenseVm);

            if (succeeded) return CustomResponse(successStatusCode: HttpStatusCode.NoContent);
            
            return CustomResponse();
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult> Get(Guid id)
        {
            var expense = await _expenseService.Get(id);

            if (expense == null) return CustomResponse(errorStatusCode: HttpStatusCode.NotFound);

            return CustomResponse(expense);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var succeeded = await _expenseService.Delete(id);

            if (succeeded)  return CustomResponse();

            return CustomResponse(errorStatusCode: HttpStatusCode.NotFound);
        }
    }
}
