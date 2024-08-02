// Decompiled with JetBrains decompiler
// Type: EmbroideryData.ApplicationRole
// Assembly: EmbroideryData, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85FC7BD1-C71A-46CF-AFDC-ECA6425FE3FE
// Assembly location: D:\RealProject\EmbroiderManagementSystem\EmbroiderManagement\obj\Release\netcoreapp3.1\PubTmp\Out\EmbroideryData.dll

using EmbroideryData.Interface;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace EmbroideryData
{
  public class ApplicationRole : IdentityRole, IAuditableEntity
  {
    public ApplicationRole()
    {
    }

    public ApplicationRole(string roleName)
      : base(roleName)
    {
    }

    public ApplicationRole(string roleName, string description)
      : base(roleName)
    {
      this.Description = description;
    }

    public string Description { get; set; }

    public string CreatedBy { get; set; }

    public string UpdatedBy { get; set; }

    public DateTimeOffset CreatedOn { get; set; }

    public DateTimeOffset UpdatedOn { get; set; }

    public virtual ICollection<IdentityUserRole<string>> Users { get; set; }

    public virtual ICollection<IdentityRoleClaim<string>> Claims { get; set; }
  }
}
