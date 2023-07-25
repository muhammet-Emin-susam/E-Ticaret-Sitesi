using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using E_Ticaret.Models;

namespace E_Ticaret.Models
{
    public class Cart
    {
        private List<Cartline> _cardLines = new List<Cartline>();

        public List<Cartline> Cartlines
        {
            get { return _cardLines; }
        }

        public void AddProduct(Products product, int Quantity)
        {
            var line = _cardLines.Where(i => i.Product.ID == product.ID).FirstOrDefault();

            if (line == null)
            {
                _cardLines.Add(new Cartline(){Product = product, Quantity = Quantity});
            }
            else
            {
                line.Quantity += Quantity;
            }
        }   

        public void DeleteProduct(Products product)
        {
            _cardLines.RemoveAll(i => i.Product.ID == product.ID);
        }

        public double Total()
        {
            return (double)_cardLines.Sum(x => x.Product.Price * x.Quantity);
        }

        public void Clear()
        {
            _cardLines.Clear();;
        }
    }
    public class Cartline
    {
        public Products Product { get; set; }
        public int Quantity { get; set; }
    }
}