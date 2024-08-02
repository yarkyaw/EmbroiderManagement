// Decompiled with JetBrains decompiler
// Type: EmbroideryData.Interface.AccountManager
// Assembly: EmbroideryData, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85FC7BD1-C71A-46CF-AFDC-ECA6425FE3FE
// Assembly location: D:\RealProject\EmbroiderManagementSystem\EmbroiderManagement\obj\Release\netcoreapp3.1\PubTmp\Out\EmbroideryData.dll

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EmbroideryData.Interface
{
  public class AccountManager : IAccountManager
  {
    private readonly EmbroideryContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<ApplicationRole> _roleManager;

    public AccountManager(
      EmbroideryContext context,
      UserManager<ApplicationUser> userManager,
      RoleManager<ApplicationRole> roleManager,
      IHttpContextAccessor httpAccessor)
    {
      this._context = context;
      this._context.CurrentUserId = httpAccessor.HttpContext?.User.FindFirst("sub")?.Value?.Trim();
      this._userManager = userManager;
      this._roleManager = roleManager;
    }

    public async Task<ApplicationUser> GetUserByIdAsync(string userId) => await this._userManager.FindByIdAsync(userId);

    public async Task<ApplicationUser> GetUserByUserNameAsync(string userName) => await this._userManager.FindByNameAsync(userName);

    public async Task<ApplicationUser> GetUserByEmailAsync(string email) => await this._userManager.FindByEmailAsync(email);

    public async Task<IList<string>> GetUserRolesAsync(ApplicationUser user) => await this._userManager.GetRolesAsync(user);

    public async Task<(ApplicationUser User, string[] Roles)?> GetUserAndRolesAsync(
      string userId)
    {
      ApplicationUser user = await this._context.Users.Include<ApplicationUser, ICollection<IdentityUserRole<string>>>((Expression<Func<ApplicationUser, ICollection<IdentityUserRole<string>>>>) (u => u.Roles)).Where<ApplicationUser>((Expression<Func<ApplicationUser, bool>>) (u => u.Id == userId)).SingleOrDefaultAsync<ApplicationUser>();
      if (user == null)
        return new (ApplicationUser, string[])?();
      List<string> userRoleIds = user.Roles.Select<IdentityUserRole<string>, string>((Func<IdentityUserRole<string>, string>) (r => r.RoleId)).ToList<string>();
      IQueryable<ApplicationRole> source = this._context.Roles.Where<ApplicationRole>((Expression<Func<ApplicationRole, bool>>) (r => userRoleIds.Contains(r.Id)));
      Expression<Func<ApplicationRole, string>> selector = (Expression<Func<ApplicationRole, string>>) (r => r.Name);
      return new (ApplicationUser, string[])?((user, await source.Select<ApplicationRole, string>(selector).ToArrayAsync<string>()));
    }

    public async Task<List<(ApplicationUser User, string[] Roles)>> GetUsersAndRolesAsync(
      int page,
      int pageSize)
    {
      IQueryable<ApplicationUser> source = (IQueryable<ApplicationUser>) this._context.Users.Include<ApplicationUser, ICollection<IdentityUserRole<string>>>((Expression<Func<ApplicationUser, ICollection<IdentityUserRole<string>>>>) (u => u.Roles)).OrderBy<ApplicationUser, string>((Expression<Func<ApplicationUser, string>>) (u => u.UserName));
      if (page != -1)
        source = source.Skip<ApplicationUser>((page - 1) * pageSize);
      if (pageSize != -1)
        source = source.Take<ApplicationUser>(pageSize);
      List<ApplicationUser> users = await source.ToListAsync<ApplicationUser>();
      List<string> userRoleIds = users.SelectMany<ApplicationUser, string>((Func<ApplicationUser, IEnumerable<string>>) (u => u.Roles.Select<IdentityUserRole<string>, string>((Func<IdentityUserRole<string>, string>) (r => r.RoleId)))).ToList<string>();
      ApplicationRole[] roles = await this._context.Roles.Where<ApplicationRole>((Expression<Func<ApplicationRole, bool>>) (r => userRoleIds.Contains(r.Id))).ToArrayAsync<ApplicationRole>();
      List<(ApplicationUser, string[])> list = users.Select<ApplicationUser, (ApplicationUser, string[])>((Func<ApplicationUser, (ApplicationUser, string[])>) (u => (u, ((IEnumerable<ApplicationRole>) roles).Where<ApplicationRole>((Func<ApplicationRole, bool>) (r => u.Roles.Select<IdentityUserRole<string>, string>((Func<IdentityUserRole<string>, string>) (ur => ur.RoleId)).Contains<string>(r.Id))).Select<ApplicationRole, string>((Func<ApplicationRole, string>) (r => r.Name)).ToArray<string>()))).ToList<(ApplicationUser, string[])>();
      users = (List<ApplicationUser>) null;
      return list;
    }

    public async Task<(bool Succeeded, string[] Errors)> CreateUserAsync(
      ApplicationUser user,
      IEnumerable<string> roles,
      string password)
    {
      IdentityResult result = await this._userManager.CreateAsync(user, password);
      if (!result.Succeeded)
        return (false, result.Errors.Select<IdentityError, string>((Func<IdentityError, string>) (e => e.Description)).ToArray<string>());
      user = await this._userManager.FindByNameAsync(user.UserName);
      try
      {
        result = await this._userManager.AddToRolesAsync(user, roles.Distinct<string>());
      }
      catch (Exception ex)
      {
        throw ex;
      }
      if (result.Succeeded)
        return (true, new string[0]);
      (bool, string[]) valueTuple1 = await this.DeleteUserAsync(user);
      return (false, result.Errors.Select<IdentityError, string>((Func<IdentityError, string>) (e => e.Description)).ToArray<string>());
    }

    public async Task<(bool Succeeded, string[] Errors)> UpdateUserAsync(ApplicationUser user) => await this.UpdateUserAsync(user, (IEnumerable<string>) null);

    public async Task<(bool Succeeded, string[] Errors)> UpdateUserAsync(
      ApplicationUser user,
      IEnumerable<string> roles)
    {
      IdentityResult identityResult1 = await this._userManager.UpdateAsync(user);
      if (!identityResult1.Succeeded)
        return (false, identityResult1.Errors.Select<IdentityError, string>((Func<IdentityError, string>) (e => e.Description)).ToArray<string>());
      if (roles != null)
      {
        IList<string> rolesAsync1 = await this._userManager.GetRolesAsync(user);
        string[] array = rolesAsync1.Except<string>(roles).ToArray<string>();
        string[] rolesToAdd = roles.Except<string>((IEnumerable<string>) rolesAsync1).Distinct<string>().ToArray<string>();
        if (((IEnumerable<string>) array).Any<string>())
        {
          IdentityResult identityResult2 = await this._userManager.RemoveFromRolesAsync(user, (IEnumerable<string>) array);
          if (!identityResult2.Succeeded)
            return (false, identityResult2.Errors.Select<IdentityError, string>((Func<IdentityError, string>) (e => e.Description)).ToArray<string>());
        }
        if (((IEnumerable<string>) rolesToAdd).Any<string>())
        {
          IdentityResult rolesAsync2 = await this._userManager.AddToRolesAsync(user, (IEnumerable<string>) rolesToAdd);
          if (!rolesAsync2.Succeeded)
            return (false, rolesAsync2.Errors.Select<IdentityError, string>((Func<IdentityError, string>) (e => e.Description)).ToArray<string>());
        }
        rolesToAdd = (string[]) null;
      }
      return (true, new string[0]);
    }

    public async Task<(bool Succeeded, string[] Errors)> ResetPasswordAsync(
      ApplicationUser user,
      string newPassword)
    {
      string passwordResetTokenAsync = await this._userManager.GeneratePasswordResetTokenAsync(user);
      IdentityResult identityResult = await this._userManager.ResetPasswordAsync(user, passwordResetTokenAsync, newPassword);
      return identityResult.Succeeded ? (true, new string[0]) : (false, identityResult.Errors.Select<IdentityError, string>((Func<IdentityError, string>) (e => e.Description)).ToArray<string>());
    }

    public async Task<(bool Succeeded, string[] Errors)> UpdatePasswordAsync(
      ApplicationUser user,
      string currentPassword,
      string newPassword)
    {
      IdentityResult identityResult = await this._userManager.ChangePasswordAsync(user, currentPassword, newPassword);
      return identityResult.Succeeded ? (true, new string[0]) : (false, identityResult.Errors.Select<IdentityError, string>((Func<IdentityError, string>) (e => e.Description)).ToArray<string>());
    }

    public async Task<bool> CheckPasswordAsync(ApplicationUser user, string password)
    {
      if (await this._userManager.CheckPasswordAsync(user, password))
        return true;
      if (!this._userManager.SupportsUserLockout)
      {
        IdentityResult identityResult = await this._userManager.AccessFailedAsync(user);
      }
      return false;
    }

    public async Task<bool> TestCanDeleteUserAsync(string userId)
    {
      int num = await Task.FromResult<int>(0);
      return true;
    }

    public async Task<(bool Succeeded, string[] Errors)> DeleteUserAsync(string userId)
    {
      ApplicationUser byIdAsync = await this._userManager.FindByIdAsync(userId);
      return byIdAsync != null ? await this.DeleteUserAsync(byIdAsync) : (true, new string[0]);
    }

    public async Task<(bool Succeeded, string[] Errors)> DeleteUserAsync(ApplicationUser user)
    {
      IdentityResult identityResult = await this._userManager.DeleteAsync(user);
      return (identityResult.Succeeded, identityResult.Errors.Select<IdentityError, string>((Func<IdentityError, string>) (e => e.Description)).ToArray<string>());
    }

    public async Task<ApplicationRole> GetRoleByIdAsync(string roleId) => await this._roleManager.FindByIdAsync(roleId);

    public async Task<ApplicationRole> GetRoleByNameAsync(string roleName) => await this._roleManager.FindByNameAsync(roleName);

    public async Task<ApplicationRole> GetRoleLoadRelatedAsync(string roleName) => await this._context.Roles.Include<ApplicationRole, ICollection<IdentityRoleClaim<string>>>((Expression<Func<ApplicationRole, ICollection<IdentityRoleClaim<string>>>>) (r => r.Claims)).Include<ApplicationRole, ICollection<IdentityUserRole<string>>>((Expression<Func<ApplicationRole, ICollection<IdentityUserRole<string>>>>) (r => r.Users)).Where<ApplicationRole>((Expression<Func<ApplicationRole, bool>>) (r => r.Name == roleName)).SingleOrDefaultAsync<ApplicationRole>();

    public async Task<List<ApplicationRole>> GetRolesLoadRelatedAsync(
      int page,
      int pageSize)
    {
      IQueryable<ApplicationRole> source = (IQueryable<ApplicationRole>) this._context.Roles.Include<ApplicationRole, ICollection<IdentityRoleClaim<string>>>((Expression<Func<ApplicationRole, ICollection<IdentityRoleClaim<string>>>>) (r => r.Claims)).Include<ApplicationRole, ICollection<IdentityUserRole<string>>>((Expression<Func<ApplicationRole, ICollection<IdentityUserRole<string>>>>) (r => r.Users)).OrderBy<ApplicationRole, string>((Expression<Func<ApplicationRole, string>>) (r => r.Name));
      if (page != -1)
        source = source.Skip<ApplicationRole>((page - 1) * pageSize);
      if (pageSize != -1)
        source = source.Take<ApplicationRole>(pageSize);
      return await source.ToListAsync<ApplicationRole>();
    }

    public async Task<(bool Succeeded, string[] Errors)> CreateRoleAsync(
      ApplicationRole role,
      IEnumerable<string> claims)
    {
      if (claims == null)
        claims = (IEnumerable<string>) new string[0];
      string[] array = claims.Where<string>((Func<string, bool>) (c => ApplicationPermissions.GetPermissionByValue(c) == null)).ToArray<string>();
      if (((IEnumerable<string>) array).Any<string>())
        return (false, new string[1]
        {
          "The following claim types are invalid: " + string.Join(", ", array)
        });
      IdentityResult result = await this._roleManager.CreateAsync(role);
      if (!result.Succeeded)
        return (false, result.Errors.Select<IdentityError, string>((Func<IdentityError, string>) (e => e.Description)).ToArray<string>());
      role = await this._roleManager.FindByNameAsync(role.Name);
      foreach (string permissionValue in claims.Distinct<string>())
      {
        result = await this._roleManager.AddClaimAsync(role, new Claim("permission", (string) ApplicationPermissions.GetPermissionByValue(permissionValue)));
        if (!result.Succeeded)
        {
          (bool, string[]) valueTuple = await this.DeleteRoleAsync(role);
          return (false, result.Errors.Select<IdentityError, string>((Func<IdentityError, string>) (e => e.Description)).ToArray<string>());
        }
      }
      return (true, new string[0]);
    }

    public async Task<(bool Succeeded, string[] Errors)> UpdateRoleAsync(
      ApplicationRole role,
      IEnumerable<string> claims)
    {
      if (claims != null)
      {
        string[] array = claims.Where<string>((Func<string, bool>) (c => ApplicationPermissions.GetPermissionByValue(c) == null)).ToArray<string>();
        if (((IEnumerable<string>) array).Any<string>())
          return (false, new string[1]
          {
            "The following claim types are invalid: " + string.Join(", ", array)
          });
      }
      IdentityResult identityResult1 = await this._roleManager.UpdateAsync(role);
      if (!identityResult1.Succeeded)
        return (false, identityResult1.Errors.Select<IdentityError, string>((Func<IdentityError, string>) (e => e.Description)).ToArray<string>());
      if (claims != null)
      {
        IEnumerable<Claim> roleClaims = (await this._roleManager.GetClaimsAsync(role)).Where<Claim>((Func<Claim, bool>) (c => c.Type == "permission"));
        string[] array1 = roleClaims.Select<Claim, string>((Func<Claim, string>) (c => c.Value)).ToArray<string>();
        string[] array2 = ((IEnumerable<string>) array1).Except<string>(claims).ToArray<string>();
        string[] claimsToAdd = claims.Except<string>((IEnumerable<string>) array1).Distinct<string>().ToArray<string>();
        string[] strArray;
        int index;
        if (((IEnumerable<string>) array2).Any<string>())
        {
          strArray = array2;
          for (index = 0; index < strArray.Length; ++index)
          {
            string claim = strArray[index];
            IdentityResult identityResult2 = await this._roleManager.RemoveClaimAsync(role, roleClaims.Where<Claim>((Func<Claim, bool>) (c => c.Value == claim)).FirstOrDefault<Claim>());
            if (!identityResult2.Succeeded)
              return (false, identityResult2.Errors.Select<IdentityError, string>((Func<IdentityError, string>) (e => e.Description)).ToArray<string>());
          }
          strArray = (string[]) null;
        }
        if (((IEnumerable<string>) claimsToAdd).Any<string>())
        {
          strArray = claimsToAdd;
          for (index = 0; index < strArray.Length; ++index)
          {
            string permissionValue = strArray[index];
            IdentityResult identityResult2 = await this._roleManager.AddClaimAsync(role, new Claim("permission", (string) ApplicationPermissions.GetPermissionByValue(permissionValue)));
            if (!identityResult2.Succeeded)
              return (false, identityResult2.Errors.Select<IdentityError, string>((Func<IdentityError, string>) (e => e.Description)).ToArray<string>());
          }
          strArray = (string[]) null;
        }
        roleClaims = (IEnumerable<Claim>) null;
        claimsToAdd = (string[]) null;
      }
      return (true, new string[0]);
    }

    public async Task<bool> TestCanDeleteRoleAsync(string roleId) => !await this._context.UserRoles.Where<IdentityUserRole<string>>((Expression<Func<IdentityUserRole<string>, bool>>) (r => r.RoleId == roleId)).AnyAsync<IdentityUserRole<string>>();

    public async Task<(bool Succeeded, string[] Errors)> DeleteRoleAsync(string roleName)
    {
      ApplicationRole byNameAsync = await this._roleManager.FindByNameAsync(roleName);
      return byNameAsync != null ? await this.DeleteRoleAsync(byNameAsync) : (true, new string[0]);
    }

    public async Task<(bool Succeeded, string[] Errors)> DeleteRoleAsync(ApplicationRole role)
    {
      IdentityResult identityResult = await this._roleManager.DeleteAsync(role);
      return (identityResult.Succeeded, identityResult.Errors.Select<IdentityError, string>((Func<IdentityError, string>) (e => e.Description)).ToArray<string>());
    }
  }
}
