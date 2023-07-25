using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using E_Ticaret.Models;

namespace E_Ticaret.Controllers
{
    public class OrderListController : Controller
    {
        // GET: OrderList
        private Entities3 db = new Entities3();
        public ActionResult Index(int ID)
        {
            var model = db.Orders.Where(x => x.ID == ID).ToList();
            ViewBag.DetailProduct = db.OrderDetails.Where(x => x.OrderID == ID).ToList();
            var OrderTotal = new OrderTotal();
            List<Orders> OrderTot = new List<Orders>();

            //OrderTotal.Price = model.Price * model.Quantity;
            //OrderTotal.Quantity = model.Quantity;
            //OrderTotal.AddedDate = model.AddedDate;
            //OrderTotal.OrderID = model.OrderID;
            //OrderTotal.ProductID = model.ProductID;
            foreach (var item in model)
            {
                var Address = db.Addresses.Where(x => x.ID == item.AddressID).FirstOrDefault();
                OrderTotal = new OrderTotal();
                OrderTotal.TotalPrice = item.TotalPrice;
                OrderTotal.AddedDate = item.AddedDate;
                OrderTotal.AddressID = item.AddressID;
                OrderTotal.Description = item.Description;
                OrderTotal.AddressDescription = item.Addresses.Description;
                OrderTotal.Status = "SA";
                OrderTotal.UserID = item.UserID;
                OrderTotal.ModifiedDate = DateTime.Now;
            }
            OrderTot.AddRange(model.ToList());
            return View(OrderTotal);
        }

        public ActionResult OrderDetail()
        {
            var username = Convert.ToInt32(Session["ID"]);
            var order = db.Orders.Where(x => x.UserID == username).ToList();
            List<Models.UserOrderModel> model = new List<UserOrderModel>();
            foreach (var item in order)
            {
                var bymodel = new UserOrderModel();
                bymodel.TotalPrice = item.OrderDetails.Sum(y => y.Quantity * y.Price);
                bymodel.OrderName = String.Join(", ", item.OrderDetails.Select(y => y.Products.Name + "(" + y.Quantity + ")"));
                bymodel.AddedDate = item.AddedDate;
                bymodel.OrderID = item.ID;
                model.Add(bymodel);
            }

            return View(model);
        }

          
    }
}