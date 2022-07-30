using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using RealEstateSystem.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RealEstateSystem.Utalities
{
    public class MyUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser, IdentityRole>
    {
        public MyUserClaimsPrincipalFactory(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager, IOptions<IdentityOptions> options) : base(userManager, roleManager, options)
        {
        }

        //public MyUserClaimsPrincipalFactory(
        //    UserManager<ApplicationUser> userManager,
        //    RoleManager<IdentityRole> roleManager,
        //    IOptions<IdentityOptions> optionsAccessor)
        //        : base(userManager, roleManager)
        //{
        //}


        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim(ClaimTypes.Email, user.Email ?? "[Click to edit profile]"));
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString() ?? "[Click to edit profile]"));
            identity.AddClaim(new Claim("FullName", user.UserName ?? "[Click to edit profile]"));
            return identity;
        }
    }
}
