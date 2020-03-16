using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Test22.Models;
using static Test22.Models.Item;

namespace Test22.Controllers
{
    public class ItemsController : Controller
    {
        private Test22Entities db = new Test22Entities();

        // GET: Items
        public ActionResult Index()
        {
            String userName = Convert.ToString(Session["userName"]);
            var items = db.Items.Include(i => i.Category).Include(i => i.User).Where(c => c.User.UserName == userName);
            return View(items.ToList());
        }
        // GET: Items/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // GET: Items/Create
        public ActionResult Create()
        {
            List<SelectListItem> li = new List<SelectListItem>();
            li.Add(new SelectListItem() { Text = "Active", Value = "1" });
            li.Add(new SelectListItem() { Text = "In-Active", Value = "0" });
            ViewBag.abc = new SelectList(li, "Value", "Text");

            string userName = Convert.ToString(Session["userName"]);

            ViewBag.CategoryID = new SelectList(db.Categories.Where(c => c.User.UserName == userName), "CategoryID", "Name");
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName");
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ItemID,Name,Price,Quantity,Status,CategoryID,DateAdded,ExperationDate,Total,UserID")] Item item)
        {
           

            if (ModelState.IsValid)
            {
               
                    db.Items.Add(item);
                    db.SaveChanges();
                
                    return RedirectToAction("Index");
                
            }

            string userName = Convert.ToString(Session["userName"]);


            ViewBag.CategoryID = new SelectList(db.Categories.Where(c => c.User.UserName == userName), "CategoryID", "Name", item.CategoryID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", item.UserID);
            return View(item);
        }

        // GET: Items/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            List<SelectListItem> li = new List<SelectListItem>();
            li.Add(new SelectListItem() { Text = "Active", Value = "1" });
            li.Add(new SelectListItem() { Text = "In-Active", Value = "0" });
            ViewBag.abc = new SelectList(li, "Value", "Text");

            string userName = Convert.ToString(Session["userName"]);



            ViewBag.CategoryID = new SelectList(db.Categories.Where(c => c.User.UserName == userName), "CategoryID", "Name", item.CategoryID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", item.UserID);
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ItemID,Name,Price,Quantity,Status,CategoryID,DateAdded,ExperationDate,Total,UserID")] Item item)
        {
            if (ModelState.IsValid)
            {
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            string userName = Convert.ToString(Session["userName"]);

            ViewBag.CategoryID = new SelectList(db.Categories.Where(c => c.User.UserName == userName), "CategoryID", "Name", item.CategoryID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", item.UserID);
            return View(item);
        }

        // GET: Items/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Item item = db.Items.Find(id);
            db.Items.Remove(item);
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
