// Decompiled with JetBrains decompiler
// Type: EmbroideryData.Constants.ApplicationPermission
// Assembly: EmbroideryData, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85FC7BD1-C71A-46CF-AFDC-ECA6425FE3FE
// Assembly location: D:\RealProject\EmbroiderManagementSystem\EmbroiderManagement\obj\Release\netcoreapp3.1\PubTmp\Out\EmbroideryData.dll

namespace EmbroideryData.Constants
{
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
