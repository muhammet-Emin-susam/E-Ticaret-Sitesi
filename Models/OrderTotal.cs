using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Ticaret.Models
{
    public class OrderTotal
    {
        public string Status { get; set; }

        public decimal TotalPrice { get; set; }

        public int UserID { get; set; }

        public int AddressID { get; set; }

        public string AddressDescription { get; set;  }

        public DateTime AddedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public string Description { get; set; }
        //public virtual OrderDetails OrderDetails { get; set; }
    }
}