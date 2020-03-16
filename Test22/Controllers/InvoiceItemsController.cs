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
    public class InvoiceItemsController : Controller
    {
        private Test22Entities db = new Test22Entities();

        // GET: InvoiceItems
        public ActionResult Index()
        {
            String userName = Convert.ToString(Session["userName"]);
            var invoiceItems = db.InvoiceItems.Include(i => i.Invoice).Include(i => i.Item).Include(i => i.User).Where(c => c.User.UserName == userName);
            return View(invoiceItems.ToList());
        }

        // GET: InvoiceItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvoiceItem invoiceItem = db.InvoiceItems.Find(id);
            if (invoiceItem == null)
            {
                return HttpNotFound();
            }
            return View(invoiceItem);
        }

        // GET: InvoiceItems/Create
        public ActionResult Create()
        {
            ViewBag.InvoiceID = new SelectList(db.Invoices, "InvoiceID", "InvoiceID");
            ViewBag.ItemID = new SelectList(db.Items, "ItemID", "Name");
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName");
            return View();
        }

        // POST: InvoiceItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InvoiceItemID,InvoiceID,ItemID,Quantity,Cost,ExtendedCost,UserID")] InvoiceItem invoiceItem)
        {
            if (ModelState.IsValid)
            {
                db.InvoiceItems.Add(invoiceItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.InvoiceID = new SelectList(db.Invoices, "InvoiceID", "InvoiceID", invoiceItem.InvoiceID);
            ViewBag.ItemID = new SelectList(db.Items, "ItemID", "Name", invoiceItem.ItemID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", invoiceItem.UserID);
            return View(invoiceItem);
        }

        // GET: InvoiceItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvoiceItem invoiceItem = db.InvoiceItems.Find(id);
            if (invoiceItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.InvoiceID = new SelectList(db.Invoices, "InvoiceID", "InvoiceID", invoiceItem.InvoiceID);
            ViewBag.ItemID = new SelectList(db.Items, "ItemID", "Name", invoiceItem.ItemID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", invoiceItem.UserID);
            return View(invoiceItem);
        }

        // POST: InvoiceItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InvoiceItemID,InvoiceID,ItemID,Quantity,Cost,ExtendedCost,UserID")] InvoiceItem invoiceItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(invoiceItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.InvoiceID = new SelectList(db.Invoices, "InvoiceID", "InvoiceID", invoiceItem.InvoiceID);
            ViewBag.ItemID = new SelectList(db.Items, "ItemID", "Name", invoiceItem.ItemID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", invoiceItem.UserID);
            return View(invoiceItem);
        }

        // GET: InvoiceItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvoiceItem invoiceItem = db.InvoiceItems.Find(id);
            if (invoiceItem == null)
            {
                return HttpNotFound();
            }
            return View(invoiceItem);
        }

        // POST: InvoiceItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            InvoiceItem invoiceItem = db.InvoiceItems.Find(id);
            db.InvoiceItems.Remove(invoiceItem);
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
