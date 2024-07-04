using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStock.Models.Entity;

namespace MvcStock.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        MvcDbStockEntities db = new MvcDbStockEntities();
        public ActionResult Index()
        {
            var values = db.TBLPRODUCTS.ToList();
            return View(values);
        }
        [HttpGet]
        public ActionResult AddProduct()
        {
            List<SelectListItem> values = (from i in db.TBLCATEGORY.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.CATEGORYNAME,
                                               Value = i.CATEGORYID.ToString()
                                           }).ToList();
            ViewBag.vls = values;
            return View();
        }
        [HttpPost]
        public ActionResult AddProduct(TBLPRODUCTS p1)
        {
            var ctg = db.TBLCATEGORY.Where(m=>m.CATEGORYID==p1.TBLCATEGORY.CATEGORYID).FirstOrDefault();
            p1.TBLCATEGORY = ctg;
            db.TBLPRODUCTS.Add(p1);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DELETE(int id)
        {
            var products = db.TBLPRODUCTS.Find(id);
            db.TBLPRODUCTS.Remove(products);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult GetProduct(int id)
        {
            var product = db.TBLPRODUCTS.Find(id);
            List<SelectListItem> values = (from i in db.TBLCATEGORY.ToList()
                                           select new SelectListItem
                                           {
                                               Text = i.CATEGORYNAME,
                                               Value = i.CATEGORYID.ToString()
                                           }).ToList();
            ViewBag.vls = values;

            return View("GetProduct",product);
        }
        public ActionResult Update(TBLPRODUCTS p1)
        {
            var product = db.TBLPRODUCTS.Find(p1.PRODUCTID);
            product.PRODUCTNAME=p1.PRODUCTNAME;
            product.BRAND = p1.BRAND;
            product.STOCK = p1.STOCK;
            product.PRICE = p1.PRICE;
            //product.PRODUCTCATEGORY = p1.PRODUCTCATEGORY;
            var ctg = db.TBLCATEGORY.Where(m => m.CATEGORYID == p1.TBLCATEGORY.CATEGORYID).FirstOrDefault();
            product.PRODUCTCATEGORY = ctg.CATEGORYID;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}