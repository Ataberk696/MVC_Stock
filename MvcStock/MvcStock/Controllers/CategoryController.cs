using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStock.Models.Entity;
using PagedList;
using PagedList.Mvc;

namespace MvcStock.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        MvcDbStockEntities db = new MvcDbStockEntities();
        public ActionResult Index(int page=1)
        {
            //var values = db.TBLCATEGORY.ToList();
            var values = db.TBLCATEGORY.ToList().ToPagedList(page, 4);
            return View(values);
        }
        [HttpGet]
        public ActionResult NewCategory() 
        {
            return View();
        }
        [HttpPost]
        public ActionResult NewCategory(TBLCATEGORY p1)
        {
            if (!ModelState.IsValid)
            {
                return View("NewCategory");
            }
            db.TBLCATEGORY.Add(p1);
            db.SaveChanges();
            return View();
        }
        
        public ActionResult DELETE(int id)
        {
            var category=db.TBLCATEGORY.Find(id);
            db.TBLCATEGORY.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult GetCategory(int id) 
        {
            var ctgry = db.TBLCATEGORY.Find(id);
            return View("GetCategory", ctgry);
        }
        public ActionResult Update(TBLCATEGORY p1) 
        {
            var ctg = db.TBLCATEGORY.Find(p1.CATEGORYID);
            ctg.CATEGORYNAME = p1.CATEGORYNAME;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}