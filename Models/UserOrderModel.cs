using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Ticaret.Models
{
    public class UserOrderModel
    {
        public string OrderName { get; set; }

        public decimal TotalPrice { get; set; }

        public DateTime AddedDate { get; set; }


        public int OrderID { get; set; }
    }
}