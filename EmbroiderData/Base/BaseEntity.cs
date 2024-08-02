// Decompiled with JetBrains decompiler
// Type: EmbroideryData.BaseEntity
// Assembly: EmbroideryData, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85FC7BD1-C71A-46CF-AFDC-ECA6425FE3FE
// Assembly location: D:\RealProject\EmbroiderManagementSystem\EmbroiderManagement\obj\Release\netcoreapp3.1\PubTmp\Out\EmbroideryData.dll

using EmbroideryData.Interface;
using System;

namespace EmbroideryData
{
  public class BaseEntity : IBaseEntity, IAuditableEntity
  {
    public int Id { get; set; }

    public DateTimeOffset CreatedOn { get; set; }

    public DateTimeOffset UpdatedOn { get; set; }

    public string CreatedBy { get; set; }

    public string UpdatedBy { get; set; }
  }
}
