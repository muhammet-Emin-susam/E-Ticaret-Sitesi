using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using E_Ticaret.Models;

namespace E_Ticaret.Controllers
{
    public class AddProductController : Controller
    {
        private Entities3 db = new Entities3();
        // GET: AddProduct
        public ActionResult Index(int ID)
        {
            ViewBag.Products = db.Products.Where(x => x.StoreID == ID).ToList();
            return View();
        }

        public ActionResult ProductDetail()
        {
            List<SelectListItem> Categorys = (from i in db.Categories.Where(x => x.ParentID == null).ToList()
                select new SelectListItem
                {
                    Text = i.Name,
                    Value = i.ID.ToString()
                }).ToList();
            var model = db.Products.FirstOrDefault();
            ViewBag.Categorys = Categorys;
            return View(model);
        }
        public ActionResult AddProduct(Products p)
        {
            // Checking no of files injected in Request object  

            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                        //string filename = Path.GetFileName(Request.Files[i].FileName);  

                        HttpPostedFileBase file = files[i];
                        string fname = file.FileName;
                        p.ImageURL = "/Uploads/Product/" + fname;
                        // Get the complete folder path and store the file inside it.  
                        fname = Path.Combine(Server.MapPath("~/Uploads/Product/"), fname);
                        file.SaveAs(fname);

                    }
                    // Returns message that successfully uploaded  

                    //var save = db.Products.Where(xa => xa.ID == p.ID).FirstOrDefault();
                    //save.CategoryID = 1;
                    //save.Name = p.Name;
                    //save.ImageURL = p.ImageURL;
                    //save.Description = p.Description;
                    //save.Price = p.Price;
                    //save.StoreID = p.ID;
                    //save.TradeMark = p.TradeMark;
                    //save.UnitsInStock = p.UnitsInStock;
                    db.Products.Add(new Products()
                    {
                        CategoryID = p.CategoryID,
                        Name = p.Name,
                        ImageURL = p.ImageURL,
                        Description = p.Description,
                        Price = p.Price,
                        StoreID = p.StoreID,
                        TradeMark = p.TradeMark,
                        UnitsInStock = p.UnitsInStock
                    });
                    try
                    {
                        db.SaveChanges();
                        //return Json("Başarılı");
                        return RedirectToAction("/Index/" + p.StoreID);

                    }
                    catch (Exception e)
                    {
                        return Json("Hata " + e.Message);

                        //return RedirectToAction("/Index");
                    }
                    //return Json("File Uploaded Successfully!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }

            }
            else
            {
                db.Products.Add(new Products()
                {
                    ImageURL = "0",
                    CategoryID = 1,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    StoreID = p.StoreID,
                    TradeMark = p.TradeMark,
                    UnitsInStock = p.UnitsInStock
                });
                try
                {
                    db.SaveChanges();
                    //return Json("Başarılı");
                    return RedirectToAction("/Index/" + p.StoreID);

                }
                catch (Exception e)
                {
                    return Json("Hata " + e.Message);

                    //return RedirectToAction("/Index");
                }
            }
        }

        public JsonResult DeleteProduct(int ID)
        {
            db.Products.Remove(db.Products.Where(x => x.ID == ID).FirstOrDefault());
            db.SaveChanges();
            return Json("Başarılı");
        }

        public ActionResult EditProduct(int ID)
        {
            //List<SelectListItem> Categorys = (from i in db.Categories.Where(x => x.ParentID == null).ToList()
            //    select new SelectListItem
            //    {
            //        Text = i.Name,
            //        Value = i.ID.ToString()
            //    }).ToList();
            //ViewBag.Categorys = Categorys;
            var model = db.Products.Where(x => x.ID == ID).FirstOrDefault();
            return View(model);
        }
        [HttpPost]
        public ActionResult UpdateProduct(Products p)
        {
            // Checking no of files injected in Request object  

            if (Request.Files.Count > 0 && Request.Files == null)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                        //string filename = Path.GetFileName(Request.Files[i].FileName);  

                        HttpPostedFileBase file = files[i];
                        string fname = file.FileName;
                        p.ImageURL = "/Uploads/Product/" + fname;
                        // Get the complete folder path and store the file inside it.  
                        fname = Path.Combine(Server.MapPath("~/Uploads/Product/"), fname);
                        file.SaveAs(fname);

                    }
                    // Returns message that successfully uploaded  

                    var save = db.Products.Where(xa => xa.ID == p.ID).FirstOrDefault();
                    save.CategoryID = p.CategoryID;
                    save.Name = p.Name;
                    save.ImageURL = p.ImageURL;
                    save.Description = p.Description;
                    save.Price = p.Price;
                    save.StoreID = p.StoreID;
                    save.TradeMark = p.TradeMark;
                    save.UnitsInStock = p.UnitsInStock;
                    try
                    {
                        db.SaveChanges();
                        //return Json("Başarılı");
                        return RedirectToAction("/Index/" + p.StoreID);

                    }
                    catch (Exception e)
                    {
                        return Json("Hata " + e.Message);

                        //return RedirectToAction("/Index");
                    }
                    //return Json("File Uploaded Successfully!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }

            }
            else
            {
                var edit = db.Products.Where(xa => xa.ID == p.ID).FirstOrDefault();
                edit.CategoryID = p.CategoryID;
                edit.Name = p.Name;
                edit.ImageURL = "0";
                edit.Description = p.Description;
                edit.Price = p.Price;
                edit.StoreID = p.StoreID;
                edit.TradeMark = p.TradeMark;
                edit.UnitsInStock = p.UnitsInStock;
                try
                {
                    db.SaveChanges();
                    //return Json("Başarılı");
                    return RedirectToAction("/Index/" + p.StoreID);

                }
                catch (Exception e)
                {
                    return Json("Hata " + e.Message);

                    //return RedirectToAction("/Index");
                }
            }
        }
    }
}