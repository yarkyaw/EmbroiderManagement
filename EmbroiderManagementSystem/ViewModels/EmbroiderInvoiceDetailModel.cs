// Decompiled with JetBrains decompiler
// Type: EmbroiderManagementSystem.ViewModels.EmbroiderInvoiceDetailModel
// Assembly: EmbroiderManagement, Version=3.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6C04B8B6-47A6-4678-8DD0-C49AAE9CC5B4
// Assembly location: D:\RealProject\EmbroiderManagementSystem\EmbroiderManagement\obj\Debug\netcoreapp3.1\EmbroiderManagementSystem.dll

using EmbroiderData;
using EmbroideryData;
using System.ComponentModel.DataAnnotations;

namespace EmbroiderManagementSystem.ViewModels
{
  public class EmbroiderInvoiceDetailModel
  {
    public int Id { get; set; }

    [Required]
    public int InvoiceId { get; set; }

    [Required]
    public int SubCategoryId { get; set; }

    [Required]
    public int ActualQuantity { get; set; }

    public string Description { get; set; }

    [Required]
    public int Quantity { get; set; }

    [Required]
    public EmbroiderInvoiceDetailType DetailType { get; set; }

    public ActiveStatus ActiveStatus { get; set; }
  }
}
