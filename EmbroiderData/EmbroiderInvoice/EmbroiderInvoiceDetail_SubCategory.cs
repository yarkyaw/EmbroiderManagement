using EmbroideryData;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmbroiderData
{
    public class EmbroiderInvoiceDetail_SubCategory
    {
        public int InvoiceDetailId { get; set; }
        public int SubCategoryId { get; set; }
        public virtual EmbroiderInvoiceDetail EmbroiderInvoiceDetail { get; set; }
        public virtual SubCategory SubCategory { get; set; }
    }
}
