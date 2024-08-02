using EmbroiderData;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace EmbroideryData
{
    public class Embroider : BaseEntity
    {
       

        #region Properties

        [Required]
        public decimal OpeningBalance { get; set; }


        [Required]
        public string Address { get; set; }

        
        [NotMapped]
        public decimal Balance { get; set; }

        public string EmbroiderCode { get; set; }

        public virtual IList<EmbroiderInvoice_Embroider> EmbroiderInvoice_Embroideries { get; set; } = new List<EmbroiderInvoice_Embroider>();

        public virtual IList<EmbroiderOrder_Embroider> EmbroiderOrder_Embroideries { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Phone { get; set; }

        #endregion
    }
}
