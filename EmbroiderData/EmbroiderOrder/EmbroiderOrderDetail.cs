using EmbroiderData;
using System.ComponentModel.DataAnnotations;

namespace EmbroideryData
{
    public class EmbroiderOrderDetail : BaseEntity
    {
        [Required]
        public int OrderId { get; set; }

        public string Description { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public int Ratio { get; set; }

        [Required]
        public MaterialType MaterialType { get; set; }

        public EmbroiderOrderDetail_SubCategory EmbroiderOrderDetail_SubCategory { get; set; } = new EmbroiderOrderDetail_SubCategory();

        public EmbroiderOrder Order { get; set; }
    }
}
