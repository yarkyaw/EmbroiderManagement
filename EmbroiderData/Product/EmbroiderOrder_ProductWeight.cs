using EmbroideryData;

namespace EmbroiderData
{
    public class EmbroiderOrder_ProductWeight
    {
        public int ProductWeightId { get; set; }

        public int OrderId { get; set; }

        public virtual EmbroiderOrder EmbroiderOrder { get; set; }

        public virtual ProductWeight ProductWeight { get; set; }
    }
}
