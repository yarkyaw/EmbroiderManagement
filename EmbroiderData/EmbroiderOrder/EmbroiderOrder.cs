using EmbroiderData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmbroideryData
{
    public class EmbroiderOrder : BaseEntity
    {
       
        [NotMapped]
        private Decimal totalMaterial;

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public Status OrderStatus { get; set; }

        [Required]
        public OrderType OrderType { get; set; }

        [Required]
        public string OrderNo { get; set; }

        [Required]
        public Decimal PaidGold { get; set; }

        [Required]
        public Decimal PaidJewel { get; set; }

        [NotMapped]
        public Decimal TotalMaterial { get => this.totalMaterial; set => this.totalMaterial = this.PaidGold + this.PaidJewel; }

        [Required]
        public int GoldGradeId { get; set; }

        public string Remark { get; set; }

        public EmbroiderOrder_ProductWeight EmbroiderOrder_ProductWeight { get; set; }

        public virtual IList<EmbroiderOrderDetail> OrderDetails { get; set; }

        public virtual EmbroiderOrder_Embroider EmbroiderOrder_Embroider { get; set; }

        public virtual EmbroiderOrder_EmbroiderInvoice EmbroiderOrder_EmbroiderInvoice { get; set; }

        public virtual EmbroiderOrder_Category EmbroiderOrder_Category { get; set; }
    }
}
