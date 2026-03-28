using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pharmacy_Udan.module
{
    public class Pharmacy
    {
        public int PharmacyID { get; set; }
        public string PharmacyName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string VerifyPassword { get; set; }

    }
}