using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using CMSApplication.Data.Entity; 

namespace CMSApplication.Identity
{
    public class MyClaimFactory<TUser> : UserClaimsPrincipalFactory<TUser>
        where TUser : User

    {

        public MyClaimFactory
                (
                    UserManager<TUser> userManager,
                    IOptions<IdentityOptions> optionsAccessor

                ) : base(userManager, optionsAccessor)
        {

        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(TUser user)
        {
            var claims = new ClaimsIdentity
                                        (
                                            "Identity.Application",
                                            Options.ClaimsIdentity.UserNameClaimType,
                                            Options.ClaimsIdentity.RoleClaimType
                                        );

            var userId = await UserManager.GetUserIdAsync(user);
            var userName = await UserManager.GetUserNameAsync(user);

            claims.AddClaim(new Claim(Options.ClaimsIdentity.UserIdClaimType, userId));
            claims.AddClaim(new Claim(Options.ClaimsIdentity.UserNameClaimType, userName));
            claims.AddClaim(new Claim(ClaimTypes.Authentication, CookieAuthenticationDefaults.AuthenticationScheme));
            claims.AddClaim(new Claim(nameof(user.Id), user.Id.ToString()));

            if (!string.IsNullOrWhiteSpace(user.FullName))
                claims.AddClaim(new Claim(nameof(user.FullName), user.FullName));
            if (!string.IsNullOrWhiteSpace(user.UserName))
                claims.AddClaim(new Claim(nameof(user.UserName), user.UserName));
            if (!string.IsNullOrWhiteSpace(user.PhoneNumber))
                claims.AddClaim(new Claim(nameof(user.PhoneNumber), user.PhoneNumber));

            var roles = await UserManager.GetRolesAsync(user);
            foreach (var roleName in roles)
                claims.AddClaim(new Claim(Options.ClaimsIdentity.RoleClaimType, roleName));
             
            return claims;
        }
    }
}
