// Decompiled with JetBrains decompiler
// Type: EmbroiderManagementSystem.Authorization.AccountManagementOperations
// Assembly: EmbroiderManagement, Version=3.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6C04B8B6-47A6-4678-8DD0-C49AAE9CC5B4
// Assembly location: D:\RealProject\EmbroiderManagementSystem\EmbroiderManagement\obj\Debug\netcoreapp3.1\EmbroiderManagementSystem.dll

namespace EmbroiderManagementSystem.Authorization
{
  public static class AccountManagementOperations
  {
    public const string CreateOperationName = "Create";
    public const string ReadOperationName = "Read";
    public const string UpdateOperationName = "Update";
    public const string DeleteOperationName = "Delete";
    public static UserAccountAuthorizationRequirement Create = new UserAccountAuthorizationRequirement(nameof (Create));
    public static UserAccountAuthorizationRequirement Read = new UserAccountAuthorizationRequirement(nameof (Read));
    public static UserAccountAuthorizationRequirement Update = new UserAccountAuthorizationRequirement(nameof (Update));
    public static UserAccountAuthorizationRequirement Delete = new UserAccountAuthorizationRequirement(nameof (Delete));
  }
}
