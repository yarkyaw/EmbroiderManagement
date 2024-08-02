// Decompiled with JetBrains decompiler
// Type: EmbroiderManagementSystem.Authorization.ManageUserAuthorizationHandler
// Assembly: EmbroiderManagement, Version=3.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6C04B8B6-47A6-4678-8DD0-C49AAE9CC5B4
// Assembly location: D:\RealProject\EmbroiderManagementSystem\EmbroiderManagement\obj\Debug\netcoreapp3.1\EmbroiderManagementSystem.dll

using EmbroiderManagementSystem.Helpers;
using EmbroideryData;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EmbroiderManagementSystem.Authorization
{
  public class ManageUserAuthorizationHandler : 
    AuthorizationHandler<UserAccountAuthorizationRequirement, string>
  {
    protected override Task HandleRequirementAsync(
      AuthorizationHandlerContext context,
      UserAccountAuthorizationRequirement requirement,
      string targetUserId)
    {
      if (context.User == null || requirement.OperationName != "Create" && requirement.OperationName != "Update" && requirement.OperationName != "Delete" || !context.User.HasClaim("permission", (string) ApplicationPermissions.ManageUsers) && !this.GetIsSameUser(context.User, targetUserId))
        return Task.CompletedTask;
      context.Succeed((IAuthorizationRequirement) requirement);
      return Task.CompletedTask;
    }

    private bool GetIsSameUser(ClaimsPrincipal user, string targetUserId) => !string.IsNullOrWhiteSpace(targetUserId) && Utilities.GetUserId(user) == targetUserId;
  }
}
