using InvoicesControl.Application.Interfaces;
using InvoicesControl.Application.Notifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Linq;
using System.Net;

namespace InvoicesControl.Api.Controllers
{
    [ApiController]
    public abstract class MainController : ControllerBase
    {
        private readonly INotifier _notifier;
        protected readonly IUser AppUser;
        protected readonly Guid UserId;
        protected readonly bool AuthenticatedUser;

        protected MainController(INotifier notifier,
            IUser appUser)
        {
            _notifier = notifier;
            AppUser = appUser;

            if (appUser.IsAuthenticated())
            {
                UserId = appUser.GetUserId();
                AuthenticatedUser = true;
            }
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            if(!modelState.IsValid) NotifyErrorInvalidModel(modelState);

            return CustomResponse();
        }

        protected ActionResult CustomResponse(object result = null, 
            HttpStatusCode successStatusCode = HttpStatusCode.OK, 
            HttpStatusCode errorStatusCode = HttpStatusCode.BadRequest)
        {
            if (IsValidOperation())
            {
                return StatusCode((int)successStatusCode, new
                {
                    success = true,
                    data = result
                });
            }

            return StatusCode((int)errorStatusCode, new
            {
                success = false,
                errors = _notifier.GetNotifications().Select(n => n.Message)
            });
        }

        protected void NotifyErrorInvalidModel(ModelStateDictionary modelState)
        {
            var errors = modelState?.Values.SelectMany(e => e.Errors);

            foreach (var error in errors)
            {
                var errorMessage = error.Exception == null ? error.ErrorMessage : error.Exception.Message;
                NotifyError(errorMessage);
            }
        }

        protected bool IsValidOperation()
        {
            return !_notifier.HasNotification();
        }

        protected bool AreIdsDifferents(Guid id1, Guid id2)
        {
            if (id1 != id2)
            {
                NotifyError("The ids are differents");
                return true;
            }

            return false;
        }

        protected void NotifyError(string message)
        {
            _notifier.Handle(new Notification(message));
        }

        protected object FormatCreatedResponse(Guid id)
        {
            return new { id = id };
        }
    }
}
