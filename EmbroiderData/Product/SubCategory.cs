using EmbroiderData;
using System.Collections.Generic;

namespace EmbroideryData
{
    public class SubCategory : BaseEntity
    {
        public string Name { get; set; }

        public string SubCategoryCode { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual IList<EmbroiderInvoiceDetail_SubCategory> EmbroiderInvoiceDetail_SubCategories { get; set; }

        public virtual IList<EmbroiderOrderDetail_SubCategory> EmbroiderOrderDetail_SubCategories { get; set; }
    }
}
