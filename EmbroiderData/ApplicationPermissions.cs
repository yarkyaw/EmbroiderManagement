// Decompiled with JetBrains decompiler
// Type: EmbroideryData.ApplicationPermissions
// Assembly: EmbroideryData, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85FC7BD1-C71A-46CF-AFDC-ECA6425FE3FE
// Assembly location: D:\RealProject\EmbroiderManagementSystem\EmbroiderManagement\obj\Release\netcoreapp3.1\PubTmp\Out\EmbroideryData.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace EmbroideryData
{
  public static class ApplicationPermissions
  {
    public static ReadOnlyCollection<ApplicationPermission> AllPermissions;
    public const string UsersPermissionGroupName = "User Permissions";
    public static ApplicationPermission ViewUsers = new ApplicationPermission("View Users", "users.view", "User Permissions", "Permission to view other users account details");
    public static ApplicationPermission ManageUsers = new ApplicationPermission("Manage Users", "users.manage", "User Permissions", "Permission to create, delete and modify other users account details");
    public const string RolesPermissionGroupName = "Role Permissions";
    public static ApplicationPermission ViewRoles = new ApplicationPermission("View Roles", "roles.view", "Role Permissions", "Permission to view available roles");
    public static ApplicationPermission ManageRoles = new ApplicationPermission("Manage Roles", "roles.manage", "Role Permissions", "Permission to create, delete and modify roles");
    public static ApplicationPermission AssignRoles = new ApplicationPermission("Assign Roles", "roles.assign", "Role Permissions", "Permission to assign roles to users");

    static ApplicationPermissions() => ApplicationPermissions.AllPermissions = new List<ApplicationPermission>()
    {
      ApplicationPermissions.ViewUsers,
      ApplicationPermissions.ManageUsers,
      ApplicationPermissions.ViewRoles,
      ApplicationPermissions.ManageRoles,
      ApplicationPermissions.AssignRoles
    }.AsReadOnly();

    public static ApplicationPermission GetPermissionByName(
      string permissionName)
    {
      return ApplicationPermissions.AllPermissions.Where<ApplicationPermission>((Func<ApplicationPermission, bool>) (p => p.Name == permissionName)).SingleOrDefault<ApplicationPermission>();
    }

    public static ApplicationPermission GetPermissionByValue(
      string permissionValue)
    {
      return ApplicationPermissions.AllPermissions.Where<ApplicationPermission>((Func<ApplicationPermission, bool>) (p => p.Value == permissionValue)).SingleOrDefault<ApplicationPermission>();
    }

    public static string[] GetAllPermissionValues() => ApplicationPermissions.AllPermissions.Select<ApplicationPermission, string>((Func<ApplicationPermission, string>) (p => p.Value)).ToArray<string>();

    public static string[] GetAdministrativePermissionValues() => new string[3]
    {
      (string) ApplicationPermissions.ManageUsers,
      (string) ApplicationPermissions.ManageRoles,
      (string) ApplicationPermissions.AssignRoles
    };
  }

    public class ApplicationPermission
    {
        public ApplicationPermission()
        {
        }

        public ApplicationPermission(string name, string value, string groupName, string description = null)
        {
            this.Name = name;
            this.Value = value;
            this.GroupName = groupName;
            this.Description = description;
        }

        public string Name { get; set; }

        public string Value { get; set; }

        public string GroupName { get; set; }

        public string Description { get; set; }

        public override string ToString() => this.Value;

        public static implicit operator string(ApplicationPermission permission) => permission.Value;
    }
}
