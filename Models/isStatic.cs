using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_Ticaret.Models
{
    public class isStatic
    {
        public static List<Categories> getCategories()
        {
            using (Entities3 db = new Entities3())
            {
                return db.Categories.ToList();
            }
        }
    }
}