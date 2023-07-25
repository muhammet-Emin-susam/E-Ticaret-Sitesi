using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using E_Ticaret.Models;

namespace E_Ticaret.Controllers
{
    public class ProfilDetailController : Controller
    {
        // GET: ProfilDetail
        private Entities3 db = new Entities3();
        public ActionResult Index(int ID)
        {
            var partial = db.Users.Where(x => x.ID == ID).FirstOrDefault();
            ViewBag.Address = db.Addresses.Where(x => x.UserID == ID).ToList();
            
            return View(partial);
            }
        public ActionResult UserEdit(int ID)
        {
            var partialView = db.Users.Where(x => x.ID == ID).FirstOrDefault();
            return PartialView("_UserEdit", partialView);
        }

        [HttpPost]
        public ActionResult UserUpdate(Users x)
        {
            // Checking no of files injected in Request object  

            if (Request.Files.Count > 0 )
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
                        x.UserImage = "/Uploads/Users/" + fname;
                        // Get the complete folder path and store the file inside it.  
                        fname = Path.Combine(Server.MapPath("~/Uploads/Users/"), fname);
                        file.SaveAs(fname);

                    }
                    // Returns message that successfully uploaded  

                    var save = db.Users.Where(xa => xa.ID == x.ID).FirstOrDefault();
                    save.UserMail = x.UserMail;
                    save.UserPassword = x.UserPassword;
                    save.UserImage = x.UserImage;
                    Session["UserImage"] = x.UserImage;
                    try
                    {
                        db.SaveChanges();
                        //return Json("Başarılı");
                        return RedirectToAction("/Index/" + x.ID);

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
                var savee = db.Users.Where(xa => xa.ID == x.ID).FirstOrDefault();
                savee.UserMail = x.UserMail;
                savee.UserPassword = x.UserPassword;
                try
                {
                    db.SaveChanges();
                    //return Json("Başarılı");
                    return RedirectToAction("/Index/" + x.ID);

                }
                catch (Exception e)
                {
                    return Json("Hata " + e.Message);

                    //return RedirectToAction("/Index");
                }
            }


        }

        public ActionResult AddAddress(string Name, string Description, string Country, string City, int HomeNo, int ApartmentNo, int UserID)
        {
            db.Addresses.Add(new Addresses()
            {
                Description = Description,
                Name = Name,
                UserID = UserID,
                ApartmentNo = ApartmentNo,
                City = City,
                Country = Country,
                HomeNo = HomeNo
            });

            try
            {
                db.SaveChanges();
                return Json("Başarılı");

            }
            catch (Exception e)
            {
                return Json("Hata " + e.Message);
            }
        }
        public JsonResult DeleteAddress(int ID)
        {
            db.Addresses.Remove(db.Addresses.Where(x => x.ID == ID).FirstOrDefault());
            db.SaveChanges();
            return Json("Başarılı");
        }
        public ActionResult EditAddress(int ID)
        {
            var partial = db.Addresses.Where(x => x.ID == ID).FirstOrDefault();
            return PartialView("_AddressEdit", partial);
        }
        public ActionResult UpdateAddress(string AddressDescription, string AddressName, int ID, int AddressHomeNo, int AddressApartNo, string AddressCountry,string AddressCity)
        {
            var ComingID = db.Addresses.Where(x => x.ID == ID).FirstOrDefault();
            ComingID.Name = AddressName;
            ComingID.Description = AddressDescription;
            ComingID.ApartmentNo = AddressApartNo;
            ComingID.City = AddressCity;
            ComingID.Country = AddressCountry;
            ComingID.HomeNo = AddressHomeNo;
            db.SaveChanges();
            return Json("Başarılı");

        }

    }
}