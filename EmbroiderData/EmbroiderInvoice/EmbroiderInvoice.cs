using EmbroiderData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmbroideryData
{
    public class EmbroiderInvoice : BaseEntity
    {
        [Required]
        public int OrderId { get; set; }

        [Required]
        public DateTime InvoiceDate { get; set; }

        [Required]
        public string InvoiceNo { get; set; }

        

        [Required]
        public Decimal ReceivedGold { get; set; }

        [Required]
        public Decimal DiposalGold { get; set; }

        [Required]
        public Decimal PaidToEmbroider { get; set; }

        [Required]
        public Decimal Total { get; set; }

        [Required]
        public Status InvoiceStatus { get; set; }

        public string Remark { get; set; }

        [Required]
        public bool HasBalance { get; set; }

        [Required]
        public Decimal Balance { get; set; }

        [Required]
        public int GoldGradeId { get; set; }

        [Required]
        public decimal ServiceFee { get; set; }

        [Required]
        public decimal ServiceFeePerItem { get; set; }

        [Required]
        public decimal ExcessOrLack { get; set; }

        public virtual EmbroiderInvoice_Embroider EmbroiderInvoice_Embroider { get; set; }

        public virtual IList<EmbroiderInvoiceDetail> InvoiceDetails { get; set; }

        public virtual EmbroiderOrder_EmbroiderInvoice EmbroiderOrder_EmbroiderInvoice { get; set; }

        public EmbroiderInvoice_ProductWeight EmbroiderInvoice_ProductWeight { get; set; }

        public virtual EmbroiderInvoice_Category EmbroiderInvoice_Category { get; set; }
    }
}
