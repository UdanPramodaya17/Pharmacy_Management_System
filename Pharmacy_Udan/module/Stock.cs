using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pharmacy_Udan.module
{
    public class Stock
    {
        public int StockId { get; set; }
        public string DrugName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Manufacturer { get; set; }
        public string AddedBy { get; set; }
        public DateTime AddedDate { get; set; }
    }
}