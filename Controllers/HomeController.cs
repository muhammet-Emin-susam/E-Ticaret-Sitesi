using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Mvc.Routing.Constraints;
using E_Ticaret.Models;

namespace E_Ticaret.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        private Entities3 db = new Entities3();
        public ActionResult Index(int id = 0)
        {
            ViewBag.Products = db.Products.ToList();
            ViewBag.Categories = db.Categories.ToList();
            return View();
        }



        public ActionResult GetProduct(int CategoryID)
        {
            var partial = db.Products.Where(x => x.CategoryID == CategoryID).ToList();
            return PartialView("GridView", partial);
        }

        public ActionResult PartialViewSample()
        {
            using (Entities3 db = new Entities3())
            {
                List<Categories> categories = new List<Categories>();
            }

            List<Categories> c = new List<Categories>();
            var partial = db.Categories;

            return PartialView("_GetMenuItem", partial);
        }

        public ActionResult ProductDetail(int ID)
        {
            var partial = db.Products.Where(x => x.ID == ID).FirstOrDefault();
            var sm = partial.CategoryID;
            var Sameproduct = db.Products.Where(x => x.CategoryID == sm).ToList();
            Sameproduct.Remove(partial);
            ViewBag.SamePro = Sameproduct;
            ViewBag.Comments = db.Comments.Where(c => c.ProductID == ID).OrderBy(x => x.AddedDate).ToList();
            return View(partial);
        }

        public ActionResult GetComment(string CommentTextArea,int UserID,int ProductID )
        {
            db.Comments.Add(new Comments()
            {
                Text = CommentTextArea,
                ProductID = ProductID,
                UserID = UserID,
                AddedDate = DateTime.Now
            });
            try
            {
                db.SaveChanges();
                ViewBag.Msg = "Kayıt Başarıyla Oluşturuldu.";
                return Json("Başarılı");

            }
            catch (Exception e)
            {
                return Json("Hata " + e.Message);
            }
        }

        public ActionResult CategoriesView(int ID)
        {
            var model = db.Products.Where(x => x.CategoryID == ID).ToList();
            //var cat = db.Categories.Where(x => x.ParentID == model.ParentID).ToList();
            return View(model);
        }

        public ActionResult SearchProduct(string SearchText , int CatID)
        {
            var model = db.Products.Where(x => x.CategoryID == CatID).ToList();
            //var Search = db.Products.Where(x => x.Name.Contains(SearchText)).ToList();
            var Search = model.Where(x => x.Name.Contains(SearchText)).ToList();
            return PartialView("GridView", Search);
        }

    }
}