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
    [Route("api/v{version:apiVersion}/categories")]
    public class ExpenseCategoriesController : MainController
    {
        private readonly IExpenseCategoryService _expenseCategoryService;

        public ExpenseCategoriesController(INotifier notifier, IUser appUser, IExpenseCategoryService expenseCategoryService)
            : base(notifier, appUser)
        {
            _expenseCategoryService = expenseCategoryService;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ExpenseCategoryVm categoryVm)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var id = await _expenseCategoryService.Add(categoryVm);

            if (id != null) return CustomResponse(FormatCreatedResponse(id.Value), HttpStatusCode.Created);

            return CustomResponse();
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult> Get(Guid id)
        {
            var expenseCategory = await _expenseCategoryService.Get(id);

            if (expenseCategory == null) return CustomResponse(errorStatusCode: HttpStatusCode.NotFound);

            return CustomResponse(expenseCategory);
        }

        [HttpGet]
        public async Task<ActionResult> Get([FromQuery] string name)
        {
            var expenseCategories = await _expenseCategoryService.List(name);

            if (expenseCategories == null) return CustomResponse(errorStatusCode: HttpStatusCode.NotFound);

            return CustomResponse(expenseCategories);
        }

        [HttpPut("{id:Guid}")]
        public async Task<ActionResult> Put(Guid id, [FromBody] ExpenseCategoryEditVm categoryVm)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            if (AreIdsDifferents(id, categoryVm.Id)) return CustomResponse();

            var succeeded = await _expenseCategoryService.Update(categoryVm);

            if (succeeded) return CustomResponse( successStatusCode: HttpStatusCode.NoContent);

            return CustomResponse();
        }

        [HttpPut("{id:guid}/activate")]
        public async Task<ActionResult> Activate(Guid id)
        {
            var succeeded = await _expenseCategoryService.Activate(id);

            if (succeeded) return CustomResponse(successStatusCode: HttpStatusCode.NoContent);

            return CustomResponse();
        }

        [HttpPut("{id:guid}/inactivate")]
        public async Task<ActionResult> Inactivate(Guid id)
        {
            var succeeded = await _expenseCategoryService.Inactivate(id);

            if (succeeded) return CustomResponse(successStatusCode: HttpStatusCode.NoContent);

            return CustomResponse();
        }
    }
}
