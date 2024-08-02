// Decompiled with JetBrains decompiler
// Type: EmbroideryData.ProductGroup
// Assembly: EmbroideryData, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85FC7BD1-C71A-46CF-AFDC-ECA6425FE3FE
// Assembly location: D:\RealProject\EmbroiderManagementSystem\EmbroiderManagement\obj\Release\netcoreapp3.1\PubTmp\Out\EmbroideryData.dll

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmbroideryData
{
  public class ProductGroup : BaseEntity
  {
    [Required]
    public string GroupCode { get; set; }

    [Required]
    public string Name { get; set; }

    public virtual IList<Category> Categories { get; set; }
  }
}
