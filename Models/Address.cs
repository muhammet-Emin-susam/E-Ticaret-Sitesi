using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_Ticaret.Models
{
    public class Address
    {
        public IEnumerable<SelectListItem> GetAddress { get; set; }
    }
}