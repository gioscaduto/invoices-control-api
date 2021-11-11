using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace InvoicesControl.Application.ViewModels.Users
{
    public class UserResponseVm
    {
        public UserResponseVm(string id, string email, IEnumerable<Claim> claims)
        {
            Id = id;
            Email = email;
            Claims = claims?.Select(c => new ClaimVm(c.Type, c.Value));
        }

        public string Id { get; private set; }
        public string Email { get; private set; }
        public IEnumerable<ClaimVm> Claims { get; private set; }
    }

    public class ClaimVm
    {
        public ClaimVm(string type, string value)
        {
            Type = type;
            Value = value;
        }

        public string Type { get; private set; }
        public string Value { get; private set; }
    }

    public class LoginResponseVm
    {
        public LoginResponseVm(string accessToken, int hoursToExpirations, IdentityUser user, IEnumerable<Claim> claims)
        {
            AccessToken = accessToken;
            ExpiresIn = TimeSpan.FromHours(hoursToExpirations).TotalSeconds;
            User = new UserResponseVm(user.Id, user.Email, claims);
        }

        public string AccessToken { get; set; }
        public double ExpiresIn { get; set; }
        public UserResponseVm User { get; set; }
    }
}
