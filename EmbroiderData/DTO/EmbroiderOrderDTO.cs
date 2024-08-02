using EmbroiderData;
using EmbroiderData.DTO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EmbroideryData.DTO
{
    public class EmbroiderOrderDTO
    {
        public int Id { get; set; }

        public string OrderNo { get; set; }

        public DateTime OrderDate { get; set; }

        public OrderType OrderType { get; set; }

        public string EmbroiderName { get; set; }
        public string EmbroiderCode { get; set; }
        public int EmbroiderId {get;set;}
        public Status OrderStatus { get; set; }

        public string CategoryName { get; set; }

        public string ProductWeightName { get; set; }

        public IEnumerable<EmbroiderOrderDetailDTO> OrderDetails { get; set; }= new List<EmbroiderOrderDetailDTO>();
    }
}
