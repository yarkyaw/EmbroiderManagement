// Decompiled with JetBrains decompiler
// Type: EmbroiderManagementSystem.Authorization.ViewRoleAuthorizationHandler
// Assembly: EmbroiderManagement, Version=3.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6C04B8B6-47A6-4678-8DD0-C49AAE9CC5B4
// Assembly location: D:\RealProject\EmbroiderManagementSystem\EmbroiderManagement\obj\Debug\netcoreapp3.1\EmbroiderManagementSystem.dll

using EmbroideryData;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace EmbroiderManagementSystem.Authorization
{
  public class ViewRoleAuthorizationHandler : 
    AuthorizationHandler<ViewRoleAuthorizationRequirement, string>
  {
    protected override Task HandleRequirementAsync(
      AuthorizationHandlerContext context,
      ViewRoleAuthorizationRequirement requirement,
      string roleName)
    {
      if (context.User == null || !context.User.HasClaim("permission", (string) ApplicationPermissions.ViewRoles) && !context.User.IsInRole(roleName))
        return Task.CompletedTask;
      context.Succeed((IAuthorizationRequirement) requirement);
      return Task.CompletedTask;
    }
  }
}
