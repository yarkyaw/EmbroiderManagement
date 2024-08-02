using EmbroideryData;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EmbroiderManagementSystem.Authorization
{
    public class ProfileService : IProfileService
    {
        /// <summary>
        /// Defines the _userManager.
        /// </summary>
        private readonly UserManager<ApplicationUser> _userManager;

        /// <summary>
        /// Defines the _claimsFactory.
        /// </summary>
        private readonly IUserClaimsPrincipalFactory<ApplicationUser> _claimsFactory;

        public ProfileService(
      UserManager<ApplicationUser> userManager,
      IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory)
        {
            this._userManager = userManager;
            this._claimsFactory = claimsFactory;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            string sub = context.Subject.GetSubjectId();
            ApplicationUser user = await this._userManager.FindByIdAsync(sub);
            ClaimsPrincipal principal = await this._claimsFactory.CreateAsync(user);
            List<Claim> claims = principal.Claims.ToList<Claim>();
            claims = claims.Where<Claim>((Func<Claim, bool>)(claim => context.RequestedClaimTypes.Contains<string>(claim.Type))).ToList<Claim>();
            if (user.JobTitle != null)
                claims.Add(new Claim("jobtitle", user.JobTitle));
            if (user.FullName != null)
                claims.Add(new Claim("fullname", user.FullName));
            if (user.Configuration != null)
                claims.Add(new Claim("configuration", user.Configuration));
            context.IssuedClaims = claims;
            sub = (string)null;
            user = (ApplicationUser)null;
            principal = (ClaimsPrincipal)null;
            claims = (List<Claim>)null;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            string sub = context.Subject.GetSubjectId();
            ApplicationUser user = await this._userManager.FindByIdAsync(sub);
            context.IsActive = user != null && user.IsEnabled;
            sub = (string)null;
            user = (ApplicationUser)null;
        }
    }
}
