using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Ticaret.Controllers
{
    public class FirstPageController : Controller
    {
        // GET: FirstPage
        public ActionResult Index()
        {
            return View();
        }
    }
}