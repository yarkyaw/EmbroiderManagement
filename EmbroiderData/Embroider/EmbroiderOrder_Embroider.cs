using EmbroideryData;

namespace EmbroiderData
{
    public class EmbroiderOrder_Embroider
    {
        public int OrderId { get; set; }

        public int EmbroiderId { get; set; }

        public virtual EmbroiderOrder EmbroiderOrder { get; set; }

        public virtual Embroider Embroider { get; set; }
    }
}
