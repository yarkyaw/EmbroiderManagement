using EmbroideryData;

namespace EmbroiderData
{
    public class EmbroiderInvoice_Embroider
    {
        public int InvoiceId { get; set; }

        public int EmbroiderId { get; set; }

        public virtual EmbroiderInvoice EmbroiderInvoice { get; set; }

        public virtual Embroider Embroider { get; set; }
    }
}
