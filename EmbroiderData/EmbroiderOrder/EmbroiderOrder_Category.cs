using EmbroideryData;

namespace EmbroiderData
{
    public class EmbroiderOrder_Category
    {
        public int OrderId { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual EmbroiderOrder EmbroiderOrder { get; set; }
    }
}
