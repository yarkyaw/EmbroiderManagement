using System;
using System.Collections.Generic;

namespace EmbroiderData.DTO
{
    public class EmbroiderInvoiceDTO
    {
        public int Id { get; set; }

        public DateTime InvoiceDate { get; set; }

        public string InvoiceNo { get; set; }

        public Decimal ReceivedGold { get; set; }

        public Decimal DiposalGold { get; set; }

        public Decimal PaidToEmbroider { get; set; }

        public Decimal Total { get; set; }

        public Status InvoiceStatus { get; set; }

        public string Remark { get; set; }

        public bool HasBalance { get; set; }

        public Decimal Balance { get; set; }

        public int GoldGradeId { get; set; }

        public decimal ServiceFee { get; set; }

        public decimal ServiceFeePerItem { get; set; }

        public decimal ExcessOrLack { get; set; }

        public string OrderNo { get; set; }

        public int OrderId { get; set; }

        public int EmbroiderId { get; set; }

        public string EmbroiderName { get; set; }

        public string EmbroiderCode { get; set; }

        public string CategoryName { get; set; }

        public string ProductWeightName { get; set; }

        public OrderType OrderType { get; set; }

        public IEnumerable<EmbroiderInvoiceDetailDTO> InvoiceDetails { get; set; } = new List<EmbroiderInvoiceDetailDTO>();
    }
}
