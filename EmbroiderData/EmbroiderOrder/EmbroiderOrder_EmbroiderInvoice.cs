using EmbroideryData;

namespace EmbroiderData
{
    public class EmbroiderOrder_EmbroiderInvoice
    {
        public int OrderId { get; set; }

        public int InvoiceId { get; set; }

        public virtual EmbroiderOrder EmbroiderOrder { get; set; }

        public virtual EmbroiderInvoice EmbroiderInvoice { get; set; }
    }
}
