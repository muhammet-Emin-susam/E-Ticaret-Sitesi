using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using E_Ticaret.Models;

namespace E_Ticaret.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        private Entities3 db = new Entities3();

        public ActionResult Login(string loginEmail, string loginPassword)
        {
            string md5pass = Crypto.Hash(loginPassword, "MD5").ToString();
            if (loginEmail != string.Empty && loginPassword != string.Empty)
            {
                //string sifre = Sifre.MD5Olustur(yourPassword);
                var p = db.Users.Where(x => x.UserMail == loginEmail && x.UserPassword == md5pass).FirstOrDefault();
                if (p != null)
                {
                    Session["ID"] = p.ID;
                    Session["UserMail"] = p.UserMail;
                    Session["UserName"] = p.UserName;   
                    Session["UserRole"] = p.UserRole;
                    Session["UserImage"] = p.UserImage;
                    //return RedirectToAction("Index", "Admin");
                    return Json("Başarılı");
                }
                else
                {
                    return Json("Hata");
                }

            }
            else
            {
                return Json("Bos");
            }
        }
    }
}