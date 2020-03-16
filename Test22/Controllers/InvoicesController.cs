using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Test22.Models;

namespace Test22.Controllers
{
    public class InvoicesController : Controller
    {
        private Test22Entities db = new Test22Entities();

        // GET: Invoices
        public ActionResult Index()
        {

            String userName = Convert.ToString(Session["userName"]);
            var invoices = db.Invoices.Include(i => i.Distributer).Include(i => i.Payment).Include(i => i.User).Where(c => c.User.UserName == userName);
            return View(invoices.ToList());
        }
      

        // GET: Invoices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        // GET: Invoices/Create
        public ActionResult Create()
        {
            List<SelectListItem> li = new List<SelectListItem>();
            li.Add(new SelectListItem() { Text = "Active", Value = "1" });
            li.Add(new SelectListItem() { Text = "In-Active", Value = "0" });
            ViewBag.abc = new SelectList(li, "Value", "Text");

            ViewBag.DistributerId = new SelectList(db.Distributers, "DistributerId", "Name");
            ViewBag.PaymentID = new SelectList(db.Payments, "PaymentID", "PaymentType");
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName");
            return View();
        }

        // POST: Invoices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InvoiceID,InvoiceCreated,AmountPaid,AmountDue,Total,DistributerId,PaymentID,UserID,PaymentStatus,ItemsReceived")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                db.Invoices.Add(invoice);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DistributerId = new SelectList(db.Distributers, "DistributerId", "Name", invoice.DistributerId);
            ViewBag.PaymentID = new SelectList(db.Payments, "PaymentID", "PaymentType", invoice.PaymentID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", invoice.UserID);
            return View(invoice);
        }

        // GET: Invoices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            List<SelectListItem> li = new List<SelectListItem>();
            li.Add(new SelectListItem() { Text = "Active", Value = "1" });
            li.Add(new SelectListItem() { Text = "In-Active", Value = "0" });
            ViewBag.abc = new SelectList(li, "Value", "Text");
            ViewBag.DistributerId = new SelectList(db.Distributers, "DistributerId", "Name", invoice.DistributerId);
            ViewBag.PaymentID = new SelectList(db.Payments, "PaymentID", "PaymentType", invoice.PaymentID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", invoice.UserID);
            return View(invoice);
        }

        // POST: Invoices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InvoiceID,InvoiceCreated,AmountPaid,AmountDue,Total,DistributerId,PaymentID,UserID,PaymentStatus,ItemsReceived")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(invoice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DistributerId = new SelectList(db.Distributers, "DistributerId", "Name", invoice.DistributerId);
            ViewBag.PaymentID = new SelectList(db.Payments, "PaymentID", "PaymentType", invoice.PaymentID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", invoice.UserID);
            return View(invoice);
        }

        // GET: Invoices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        // POST: Invoices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Invoice invoice = db.Invoices.Find(id);
            db.Invoices.Remove(invoice);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
