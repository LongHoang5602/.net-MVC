using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Web.Models;

namespace Web.Areas.RoleAdmin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdmProductController : Controller
    {
        private readonly ApplicationDbContext _context;


        public AdmProductController()
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


        public ActionResult Create()
        {
            ViewBag.IdCategory = new SelectList(_context.Categories, "IdCategory", "Name");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,ProductName,ProductDescription,ProductPrice,ImageProduct,IdCategory")] Products products, HttpPostedFileBase ImageProduct)
        {
            if (ModelState.IsValid)
            {
                if (ImageProduct != null && ImageProduct.ContentLength > 0)
                    try
                    {  //Server.MapPath takes the absolte path of folder 'Uploads'
                        string path = Path.Combine(Server.MapPath("/Content/images/"),
                                       Path.GetFileName(ImageProduct.FileName));
                        //Save file using Path+fileName take from above string
                        ImageProduct.SaveAs(path);
                        ViewBag.Message = "File uploaded successfully";
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Message = "ERROR:" + ex.Message.ToString();
                    }
                else
                {
                    ViewBag.Message = "You have not specified a file.";
                }

                products.ImageProduct = "/Content/images/" + Path.GetFileName(ImageProduct.FileName);
                _context.Products.Add(products);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdCategory = new SelectList(_context.Products, "ProductId", "ProductName", products.IdCategory);

            return View(products);
        }



        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products product = _context.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }

            ViewBag.IdCategory = new SelectList(_context.Categories, "IdCategory", "Name", product.IdCategory);
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,ProductName,ProductDescription,ProductPrice,ImageProduct,IdCategory")] Products product, HttpPostedFileBase ProductImg)
        {
            {
                if (ProductImg != null && ProductImg.ContentLength > 0)
                {
                    try
                    {
                        string path = Path.Combine(Server.MapPath("/Content/images/"),
                                                   Path.GetFileName(ProductImg.FileName));
                        ProductImg.SaveAs(path);
                        product.ImageProduct = "/Content/images/" + Path.GetFileName(ProductImg.FileName);
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Message = "ERROR:" + ex.Message.ToString();
                    }
                }

                _context.Entry(product).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdCategory = new SelectList(_context.Categories, "CategoryId", "Name", product.IdCategory);
            return View(product);
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Products products = _context.Products.Find(id);
            if (products == null)
            {
                return HttpNotFound();
            }
            return View(products);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Products products = _context.Products.Find(id);
            _context.Products.Remove(products);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }

}