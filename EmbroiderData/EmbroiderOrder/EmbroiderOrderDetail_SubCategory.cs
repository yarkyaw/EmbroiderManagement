using EmbroideryData;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmbroiderData
{
    public class EmbroiderOrderDetail_SubCategory
    {
        public int OrderDetailId { get; set; }
        public int SubCategoryId { get; set; }
        public virtual EmbroiderOrderDetail EmbroiderOrderDetail { get; set; }
        public virtual SubCategory SubCategory { get; set; }
    }
}
