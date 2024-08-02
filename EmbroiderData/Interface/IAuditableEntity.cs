// Decompiled with JetBrains decompiler
// Type: EmbroideryData.Interface.IAuditableEntity
// Assembly: EmbroideryData, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85FC7BD1-C71A-46CF-AFDC-ECA6425FE3FE
// Assembly location: D:\RealProject\EmbroiderManagementSystem\EmbroiderManagement\obj\Release\netcoreapp3.1\PubTmp\Out\EmbroideryData.dll

using System;

namespace EmbroideryData.Interface
{
  public interface IAuditableEntity
  {
    DateTimeOffset CreatedOn { get; set; }

    DateTimeOffset UpdatedOn { get; set; }

    string CreatedBy { get; set; }

    string UpdatedBy { get; set; }
  }
}
