// Decompiled with JetBrains decompiler
// Type: EmbroideryData.Category
// Assembly: EmbroideryData, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 85FC7BD1-C71A-46CF-AFDC-ECA6425FE3FE
// Assembly location: D:\RealProject\EmbroiderManagementSystem\EmbroiderManagement\obj\Release\netcoreapp3.1\PubTmp\Out\EmbroideryData.dll

using EmbroiderData;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmbroideryData
{
  public class Category : BaseEntity
  {
    [Required]
    public string CategoryCode { get; set; }

    public int GroupId { get; set; }

    [Required]
    public string Name { get; set; }

    public virtual ProductGroup Group { get; set; }

    public virtual IList<SubCategory> SubCategories { get; set; }

    public virtual IList<EmbroiderInvoice_Category> EmbroiderInvoice_Categories { get; set; }
    public virtual IList<EmbroiderOrder_Category> EmbroiderOrder_Categories { get; set; }
        
    }
}
