using EmbroiderData;
using System.ComponentModel.DataAnnotations;

namespace EmbroideryData
{
    public class EmbroiderInvoiceDetail : BaseEntity
    {
        [Required]
        public int InvoiceId { get; set; }

        [Required]
        public int Quantity { get; set; }

        public string Description { get; set; }

        public EmbroiderInvoiceDetailType DetailType {get;set;}

        public int ActualQuantity { get; set; }

        public ActiveStatus ActiveStatus { get; set; }

        public virtual EmbroiderInvoiceDetail_SubCategory EmbroiderInvoiceDetail_SubCategory { get; set; }

    public virtual EmbroiderInvoice Invoice { get; set; }
  }
}
