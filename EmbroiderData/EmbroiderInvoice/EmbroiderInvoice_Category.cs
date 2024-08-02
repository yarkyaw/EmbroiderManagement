using EmbroideryData;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmbroiderData
{
    public class EmbroiderInvoice_Category
    {
        public int InvoiceId { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public virtual EmbroiderInvoice  EmbroiderInvoice { get; set; }
    }
}
