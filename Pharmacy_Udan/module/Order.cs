using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pharmacy_Udan.module
{
    public class Order
    {
        public int OrderId { get; set; }
        public int StockId { get; set; }
        public string DrugName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }

    }
}