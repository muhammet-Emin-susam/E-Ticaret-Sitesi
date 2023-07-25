using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using E_Ticaret.Models;

namespace E_Ticaret.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        public ActionResult Index()
        {
            return View();
        }

        private Entities3 db = new Entities3();
        public ActionResult RegisterUser(string RegisterPassword, string RegisterEmail, string RegisterUser)
        {
            db.Users.Add(new Users()
            {
                UserName = RegisterUser,
                UserMail = RegisterEmail,
                UserAddedDate = DateTime.Now,
                UserPassword = Crypto.Hash(RegisterPassword, "MD5")
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
    }
}