using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStock.Models.Entity;

namespace MvcStock.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        MvcDbStockEntities db = new MvcDbStockEntities();
        public ActionResult Index(string p)
        {
            var values = from d in db.TBLCUSTOMER select d;
            if (!string.IsNullOrEmpty(p))
            {
                values = values.Where(m => m.CUSTOMERNAME.Contains(p));
            }
            return View(values.ToList());
            //var values = db.TBLCUSTOMER.ToList();
            //return View(values);
        }
        [HttpGet]
        public ActionResult NewCustomer()
        {
            return View();
        }
        [HttpPost]
        public ActionResult NewCustomer(TBLCUSTOMER p1)
        {
            if (!ModelState.IsValid)
            {
                return View("NewCustomer");
            }
            db.TBLCUSTOMER.Add(p1);
            db.SaveChanges();
            return View();
        }

        public ActionResult DELETE(int id)
        {
            var customer = db.TBLCUSTOMER.Find(id);
            db.TBLCUSTOMER.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult GetCustomer(int id) 
        {
            var cus = db.TBLCUSTOMER.Find(id);  
            return View("GetCustomer",cus);
        }

        public ActionResult Update(TBLCUSTOMER p1)
        {
            var cus = db.TBLCUSTOMER.Find(p1.CUSTOMERID);
            cus.CUSTOMERNAME = p1.CUSTOMERNAME;
            cus.CUSTOMERSURNAME = p1.CUSTOMERSURNAME;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}