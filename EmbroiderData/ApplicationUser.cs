// Decompiled with JetBrains decompiler
// Type: EmbroideryData.ApplicationUser
// Assembly: EmbroideryData, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85FC7BD1-C71A-46CF-AFDC-ECA6425FE3FE
// Assembly location: D:\RealProject\EmbroiderManagementSystem\EmbroiderManagement\obj\Release\netcoreapp3.1\PubTmp\Out\EmbroideryData.dll

using EmbroideryData.Interface;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace EmbroideryData
{
  public class ApplicationUser : IdentityUser, IAuditableEntity
  {
    public virtual string FriendlyName
    {
      get
      {
        string str = string.IsNullOrWhiteSpace(this.FullName) ? this.UserName : this.FullName;
        if (!string.IsNullOrWhiteSpace(this.JobTitle))
          str = this.JobTitle + " " + str;
        return str;
      }
    }

    public string JobTitle { get; set; }

    public string FullName { get; set; }

    public string Configuration { get; set; }

    public bool IsEnabled { get; set; }

    public bool IsLockedOut
    {
      get
      {
        if (!this.LockoutEnabled)
          return false;
        DateTimeOffset? lockoutEnd = this.LockoutEnd;
        DateTimeOffset utcNow = DateTimeOffset.UtcNow;
        return lockoutEnd.HasValue && lockoutEnd.GetValueOrDefault() >= utcNow;
      }
    }

    public string CreatedBy { get; set; }

    public string UpdatedBy { get; set; }

    public DateTimeOffset CreatedOn { get; set; }

    public DateTimeOffset UpdatedOn { get; set; }

    public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

    public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }
  }
}
