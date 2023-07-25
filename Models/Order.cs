using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Ticaret.Models
{
    public class Order
    {
        public int ID { get; set; }
        public double Total  { get; set; }
        public string Status { get; set; }
        public int AddressID { get; set; }
        public int UserID { get; set; }
        public DateTime AddedDate  { get; set; }
        public virtual List<OrderLine> OrderLines  { get; set; }
    }
    public class OrderLine
    {
        public int ID { get; set; }
        public int OrderID { get; set; }
        public virtual Order Order { get; set;}

        public int Quantity { get; set; }

        public double Price { get; set; }

        public DateTime AddedDate { get; set; }

        public int ProductID { get; set; }
        public virtual Products Product { get; set; }
    }
}