using InvoicesControl.Api.Extensions;
using InvoicesControl.Application.ViewModels.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace InvoicesControl.Api.Helper
{
    public class JwtHelper
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppSettings _appSettings;

        public JwtHelper(UserManager<IdentityUser> userManager, IOptions<AppSettings> appSettings)
        {
            _userManager = userManager;
            _appSettings = appSettings.Value;
        }

        public async Task<LoginResponseVm> GenerateJwt(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var claimsIdentity = await GetClaimsIdentity(user);
            var encodedToken = GetEncodedToken(claimsIdentity);

            var response = new LoginResponseVm(encodedToken, _appSettings.HoursToExpirations, user, claimsIdentity.Claims);

            return response;
        }

        private string GetEncodedToken(ClaimsIdentity identityClaims)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _appSettings.Issuer,
                Audience = _appSettings.ValidUrls,
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(_appSettings.HoursToExpirations),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            return tokenHandler.WriteToken(token);
        }

        private async Task<ClaimsIdentity> GetClaimsIdentity(IdentityUser user)
        {
            var claims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));

            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim("role", userRole));
            }

            var claimsIdentity = new ClaimsIdentity();
            claimsIdentity.AddClaims(claims);

            return claimsIdentity;
        }

        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
    }
}
