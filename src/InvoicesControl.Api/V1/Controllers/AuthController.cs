using InvoicesControl.Api.Controllers;
using InvoicesControl.Api.Helper;
using InvoicesControl.Application.Interfaces;
using InvoicesControl.Application.Notifications;
using InvoicesControl.Application.ViewModels.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace InvoicesControl.Api.V1.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/auth")]
    public class AuthController : MainController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly JwtHelper _jwtHelper;

        public AuthController(INotifier notifier,
            SignInManager<IdentityUser> signInManager,
            JwtHelper jwtHelper,
            IUser appUser) : base(notifier, appUser)
        {
            _signInManager = signInManager;
            _jwtHelper = jwtHelper;
        }

        [AllowAnonymous]
        [HttpPost("")]
        public async Task<ActionResult> Login(LoginUserVm loginUser)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var result = await _signInManager.PasswordSignInAsync(loginUser.Email, loginUser.Password, false, true);

            if (result.Succeeded)
            {
                var jwt = await _jwtHelper.GenerateJwt(loginUser.Email);
                return CustomResponse(jwt);
            }

            if (result.IsLockedOut)
            {
                NotifyError("Your account has been temporarily locked due to too many incorrect password attempts");
                return CustomResponse(loginUser);
            }

            NotifyError("User or password are incorrect.");
            return CustomResponse(loginUser);
        }
    }
}
