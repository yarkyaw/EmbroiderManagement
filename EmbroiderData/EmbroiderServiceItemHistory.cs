using EmbroideryData;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmbroiderData
{
    public class EmbroiderServiceItemHistory : BaseEntity
    {
        public DateTime InsertedDate { get; set; }

        public int CategoryId { get;set;}

        public decimal Rate { get; set; }

        public int ProductWeightId { get; set; }

    }
}
