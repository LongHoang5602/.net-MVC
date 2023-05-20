using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;



        public ProductController()
        {
            _context = new ApplicationDbContext();
        }


        public ActionResult Index()
        {
            List<Products> products = _context.Products.ToList();
            return View(products);

        }

        public ActionResult Details(int? id)
        {
            var product = _context.Products.Find(id);

            if (product == null)
            {
                return HttpNotFound();
            }

            return View(product);
        }


        private int isExist(int id)
        {
            List<Item> cart = (List<Item>)Session["cart"];
            for (int i = 0; i < cart.Count; i++)
                if (cart[i].products.ProductId.Equals(id))
                    return i;
            return -1;
        }

        public ActionResult UpdateCart()
        {
            int IdProduct = int.Parse(Request.Form["ProductId"]);
            int Quantity = int.Parse(Request.Form["Quantity"]);

            List<Item> cart = (List<Item>)Session["cart"];
            int index = isExist(IdProduct);
            if (index != -1)
            {
                cart[index].Quantity = Quantity;
            }
            Session["cart"] = cart;
            return RedirectToAction("ViewCart");
        }

        public ActionResult AddCart(int id)
        {
            Products products = _context.Products.Find(id);
            if (Session["cart"] == null)
            {
                List<Item> cart = new List<Item>();
                cart.Add(new Item { products = products, Quantity = 1 });
                Session["cart"] = cart;
            }
            else
            {
                List<Item> cart = (List<Item>)Session["cart"];
                int index = isExist(id);
                if (index != -1)
                {
                    cart[index].Quantity++;
                }
                else
                {
                    cart.Add(new Item { products = products, Quantity = 1 });
                }
                Session["cart"] = cart;
            }
            return RedirectToAction("ViewCart");
        }

        public ActionResult ViewCart()
        {
            return View();
        }
        public ActionResult RemoveCart(int id)
        {
            List<Item> cart = (List<Item>)Session["cart"];
            int index = isExist(id);

            if (cart[index].Quantity >= 2)
            {
                cart[index].Quantity--;
            }
            else
            {
                cart.RemoveAt(index);
            }
            Session["cart"] = cart;
            return RedirectToAction("ViewCart");
        }

        public ActionResult RemoveAll()
        {
            List<Item> cart = (List<Item>)Session["cart"];
            if (cart.Count > 0)
            {
                cart.Clear();
            }
            Session["cart"] = cart;
            return RedirectToAction("ViewCart");
        }



        public ActionResult Search(string productName)
        {
            var products = from p in _context.Products
                           select p;

            if (!string.IsNullOrEmpty(productName))
            {
                products = products.Where(p => p.ProductName.Contains(productName));
            }

            return View(products);
        }


    }
}
