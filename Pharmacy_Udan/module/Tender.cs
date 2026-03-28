using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Pharmacy_Udan.module
{
    public class Tender
    {
        public int TenderId { get; set; }
        public int SupplierId { get; set; }
        public string TenderDrugName { get; set; }  // <-- New property
        public decimal TenderAmount { get; set; }
        public int TenderQuantity { get; set; }
        public DateTime TenderDate { get; set; }
        public string TenderStatus { get; set; }
    }
}