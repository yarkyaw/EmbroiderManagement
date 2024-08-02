using EmbroiderData;
using EmbroideryData;
using System;
using System.Collections.Generic;

namespace EmbroiderManagementSystem.ViewModels
{
  public class EmbroiderOrderModel
  {
    public int Id { get; set; }

    public DateTime OrderDate { get; set; }

    public Status OrderStatus { get; set; }

    public OrderType OrderType { get; set; }

    public int GoldGradeId { get; set; }

    public int ProductWeightId { get; set; }

    public Decimal PaidGold { get; set; }

    public Decimal PaidJewel { get; set; }

    public Decimal TotalMaterial { get; set; }

    public string OrderNo { get; set; }

    public int EmbroiderId { get; set; }

    public int CategoryId { get; set; }

    public string Remark { get; set; }

    public int InvoiceId { get; set; }

    public ProductWeightModel ProductWeight { get; set; }

    public IList<EmbroiderOrderDetailModel> OrderDetails { get; set; }

    public EmbroiderModel Embroider { get; set; }

    public CategoryModel Category { get; set; }
  }
}
