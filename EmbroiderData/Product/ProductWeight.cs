using EmbroiderData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmbroideryData
{
    public class ProductWeight : BaseEntity
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Gram { get; set; }

        [Required]
        public string LocalizeName { get; set; }

        public virtual IList<EmbroiderOrder_ProductWeight> EmbroiderOrder_ProductWeights { get; set; }

        public IList<EmbroiderInvoice_ProductWeight> EmbroiderInvoice_ProductWeights { get; set; }

        [NotMapped]
        public string FullName => this.Name + "(" + this.LocalizeName + ")";
    }
}
