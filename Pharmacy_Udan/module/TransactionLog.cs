using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pharmacy_Udan.module
{
    public class TransactionLog
    {
        public int stockid { get; set; }
        public string action { get; set; }
        public int quantity { get; set; }

        public string actionBy { get; set; }
        public DateTime DateTime { get; set; }


    }
}