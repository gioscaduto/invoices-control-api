using InvoicesControl.Api.Helper;
using InvoicesControl.Application.Interfaces;
using InvoicesControl.Application.Interfaces.Services;
using InvoicesControl.Application.Notifications;
using InvoicesControl.Application.ViewModels.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace InvoicesControl.Api.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/users")]
    public class UsersController : MainController
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IPasswordValidator<IdentityUser> _passwordValidator;
        private readonly IPasswordHasher<IdentityUser> _passwordHasher;
        private readonly JwtHelper _jwtHelper;
        private readonly IUserService _userService;

        public UsersController(INotifier notifier, IUser appUser, UserManager<IdentityUser> userManager,
             SignInManager<IdentityUser> signInManager,
             IPasswordValidator<IdentityUser> passwordValidator,
             IPasswordHasher<IdentityUser> passwordHasher,
             JwtHelper jwtHelper, IUserService userService) 
            : base(notifier, appUser)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _passwordValidator = passwordValidator;
            _passwordHasher = passwordHasher;
            _jwtHelper = jwtHelper;
            _userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult> Post(UserVm registerUser)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var user = GenerateIdentityUser(registerUser.Email);
            var identityResult = await _userManager.CreateAsync(user, registerUser.Password);

            if (identityResult.Succeeded)
            {
                var succeeded = await _userService.Add(registerUser, user.Id);

                if (succeeded)
                {
                    var jwt = await SignInAndGetJwt(user);
                    return CustomResponse(jwt, HttpStatusCode.Created);                    
                }

                await _userManager.DeleteAsync(user);
            }

            NotifyIdentityErrors(identityResult);
            return CustomResponse();
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Put(Guid id, [FromBody] UserEditVm userEdit)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            if(AreIdsDifferents(id, userEdit.Id)) return CustomResponse();

            var user = await GetIdentityUserAndSetValues(userEdit);

            if (user == null) return NotFound();

            var validPasswordResult = await CheckAndChangeIfPasswordIsValid(user, userEdit.Password);

            if (validPasswordResult.Succeeded)
            {
                var identityResult = await _userManager.UpdateAsync(user);

                if (identityResult.Succeeded)
                {
                    var succeeded = await _userService.Update(userEdit);

                    if (succeeded) 
                        return CustomResponse(successStatusCode: HttpStatusCode.NoContent);
                }

                NotifyIdentityErrors(identityResult);
            }
            else
            {
                NotifyIdentityErrors(validPasswordResult);
            }

            return CustomResponse();
        }

        private async Task<IdentityResult> CheckAndChangeIfPasswordIsValid(IdentityUser user, string password)
        {
            var validPasswordResult = await _passwordValidator.ValidateAsync(_userManager, user, password);

            if (validPasswordResult.Succeeded)
            {
                user.PasswordHash = _passwordHasher.HashPassword(user, password);                
            }

            return validPasswordResult;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult> Get(Guid id)
        {
            var user = await _userService.Get(id);

            if (user == null) return NotFound();

            return CustomResponse(user);
        }

        private void NotifyIdentityErrors(IdentityResult result)
        {
            foreach (var error in result?.Errors)
            {
                NotifyError(error.Description);
            }
        }

        private IdentityUser GenerateIdentityUser(string email)
        {
            return new IdentityUser
            {
                UserName = email,
                Email = email,
                EmailConfirmed = true
            };
        }

        private async Task<IdentityUser> GetIdentityUserAndSetValues(UserEditVm  user)
        {
            var identityUser = await _userManager.FindByIdAsync(user.Id.ToString());

            if (identityUser == null) return null;

            identityUser.UserName = user.Email;
            identityUser.Email = user.Email;

            return identityUser;
        }

        private async Task<LoginResponseVm> SignInAndGetJwt(IdentityUser user)
        {
            await _signInManager.SignInAsync(user, isPersistent: false);
            return await _jwtHelper.GenerateJwt(user.Email);
        }
    }
}
