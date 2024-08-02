// Decompiled with JetBrains decompiler
// Type: EmbroiderManagementSystem.Authorization.AssignRolesAuthorizationHandler
// Assembly: EmbroiderManagement, Version=3.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6C04B8B6-47A6-4678-8DD0-C49AAE9CC5B4
// Assembly location: D:\RealProject\EmbroiderManagementSystem\EmbroiderManagement\obj\Debug\netcoreapp3.1\EmbroiderManagementSystem.dll

using EmbroideryData;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EmbroiderManagementSystem.Authorization
{
  public class AssignRolesAuthorizationHandler : 
    AuthorizationHandler<AssignRolesAuthorizationRequirement, (string[] newRoles, string[] currentRoles)>
  {
    protected override Task HandleRequirementAsync(
      AuthorizationHandlerContext context,
      AssignRolesAuthorizationRequirement requirement,
      (string[] newRoles, string[] currentRoles) roles)
    {
      if (!this.GetIsRolesChanged(roles.newRoles, roles.currentRoles))
        context.Succeed((IAuthorizationRequirement) requirement);
      else if (context.User.HasClaim("permission", (string) ApplicationPermissions.AssignRoles))
      {
        if (context.User.HasClaim("permission", (string) ApplicationPermissions.ViewRoles))
          context.Succeed((IAuthorizationRequirement) requirement);
        else if (this.GetIsUserInAllAddedRoles(context.User, roles.newRoles, roles.currentRoles))
          context.Succeed((IAuthorizationRequirement) requirement);
      }
      return Task.CompletedTask;
    }

    private bool GetIsRolesChanged(string[] newRoles, string[] currentRoles)
    {
      if (newRoles == null)
        newRoles = new string[0];
      if (currentRoles == null)
        currentRoles = new string[0];
      return ((IEnumerable<string>) newRoles).Except<string>((IEnumerable<string>) currentRoles).Any<string>() | ((IEnumerable<string>) currentRoles).Except<string>((IEnumerable<string>) newRoles).Any<string>();
    }

    private bool GetIsUserInAllAddedRoles(
      ClaimsPrincipal contextUser,
      string[] newRoles,
      string[] currentRoles)
    {
      if (newRoles == null)
        newRoles = new string[0];
      if (currentRoles == null)
        currentRoles = new string[0];
      return ((IEnumerable<string>) newRoles).Except<string>((IEnumerable<string>) currentRoles).All<string>((Func<string, bool>) (role => contextUser.IsInRole(role)));
    }
  }
}
