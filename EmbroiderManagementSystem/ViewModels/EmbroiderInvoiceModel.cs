using EmbroiderData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmbroiderManagementSystem.ViewModels
{
    public class EmbroiderInvoiceModel
    {
        #region Properties


        [Required]
        public int CategoryId { get; set; }

        [Required]
        public decimal DiposalGold { get; set; }

        [Required]
        public decimal ExcessOrLack { get; set; }

        [Required]
        public int EmbroiderId { get; set; }

        [Required]
        public int GoldGradeId { get; set; }

        [Required]
        public bool HasBalance { get; set; }

        public int Id { get; set; }

        [Required]
        public DateTime InvoiceDate { get; set; }

        public List<EmbroiderInvoiceDetailModel> InvoiceDetails { get; set; } = new List<EmbroiderInvoiceDetailModel>();

        public string InvoiceNo { get; set; }

        [Required]
        public Status InvoiceStatus { get; set; }

        [Required]
        public int OrderId { get; set; }

        [Required]
        public int ProductWeightId { get; set; }

        [Required]
        public decimal ReceivedGold { get; set; }

        public string Remark { get; set; }

        [Required]
        public decimal ServiceFee { get; set; }

        [Required]
        public decimal ServiceFeePerItem { get; set; }

        [Required]
        public decimal Total { get; set; }

        public EmbroiderOrderModel Order { get; set; }

        #endregion
    }
}
