using EmbroideryData;

namespace EmbroiderData
{
    public class EmbroiderInvoice_ProductWeight
    {
        public int ProductWeightId { get; set; }

        public int InvoiceId { get; set; }

        public virtual EmbroiderInvoice EmbroiderInvoice { get; set; }

        public virtual ProductWeight ProductWeight { get; set; }
    }
}
