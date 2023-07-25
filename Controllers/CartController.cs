using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using E_Ticaret.Models;

namespace E_Ticaret.Controllers
{
    public class CartController : Controller
    {
        private Entities3 db = new Entities3();
        // GET: Cart
        public ActionResult Index(int ID)
        {
            List<SelectListItem> Address = (from i in db.Addresses.ToList()
                                            select new SelectListItem
                                            {
                                                Text = i.Name,
                                                Value = i.ID.ToString()
                                            }).ToList();
            ViewBag.Address = Address;
            ViewBag.Adress = db.Addresses.Where(x => x.UserID == ID).ToList();
            return View(GetCart());
        }


        public JsonResult AddCart(int ID)
        {
            var product = db.Products.Where(i => i.ID == ID).FirstOrDefault();

            if (product != null)
            {
                GetCart().AddProduct(product, 1);
            }

            return Json("Başarılı");
        }
        public JsonResult RemoveCart(int ID)
        {
            var product = db.Products.Where(i => i.ID == ID).FirstOrDefault();

            if (product != null)
            {
                GetCart().DeleteProduct(product);
            }
            return Json("Başarılı");
        }

        public Cart GetCart()
        {
            var cart = (Cart)Session["Cart"];
            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }

            return cart;
        }

        public PartialViewResult Summary()
        {
            return PartialView(GetCart());
        }

        public bool IsLogon()
        {
            if (Session["ID"] == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public ActionResult GetAddress(int ID)
        {
            var partial = db.Addresses.Where(x => x.ID == ID).FirstOrDefault();
            return PartialView("_AddressPartial", partial);
        }

        public ActionResult Buy(int UserID, int AddressID)
        {
            Cart crt = new Cart();
            var cart = GetCart();
            List<Cartline> _cardLines = new List<Cartline>();
            //var order = new Order();
            //db.Orders.Add(new Orders()
            //{
            //    UserID = UserID,
            //    AddressID = AddressID,
            //    AddedDate = DateTime.Now,
            //    Status = "SA",
            //});
            var order = new Orders()
            {
                UserID = UserID,
                AddressID = AddressID,
                AddedDate = DateTime.Now,
                Status = "SA",
                TotalPrice = cart.Cartlines.Sum(x => x.Product.Price * x.Quantity)
            };
            db.Orders.Add(order);
            db.SaveChanges();
            //SaveOrder(crt);
            foreach (var pr in cart.Cartlines)
            {
                db.OrderDetails.Add(new OrderDetails()
                {
                    Quantity = pr.Quantity,
                    Price = pr.Product.Price,
                    ProductID = pr.Product.ID,
                    AddedDate = DateTime.Now,
                    OrderID = order.ID
                });
                var _product = db.Products.Where(x => x.ID == pr.Product.ID).FirstOrDefault();
                _product.UnitsInStock = _product.UnitsInStock - pr.Quantity;
            }

            
            try
            {
                db.SaveChanges();
                ViewBag.Msg = "Kayıt Başarıyla Oluşturuldu.";
                cart.Clear();
                return Json("Başarılı");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

     

        public ActionResult CompleteOrder()
        {
            var Cart = new Cart();
            if (ModelState.IsValid)
            {
                //SaveOrder(Cart);
                Cart.Clear();
                return View();
            }

            return View();
        }
    }
}