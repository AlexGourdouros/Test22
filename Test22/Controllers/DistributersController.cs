﻿using System;
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
    public class DistributersController : Controller
    {
        private Test22Entities db = new Test22Entities();

        // GET: Distributers
        public ActionResult Index()
        {
            String userName = Convert.ToString(Session["userName"]);
            var distributers = db.Distributers.Include(d => d.User).Where(c => c.User.UserName == userName);
            return View(distributers.ToList());
        }

        // GET: Distributers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Distributer distributer = db.Distributers.Find(id);
            if (distributer == null)
            {
                return HttpNotFound();
            }
            return View(distributer);
        }

        // GET: Distributers/Create
        public ActionResult Create()
        {
            List<SelectListItem> li = new List<SelectListItem>();
            li.Add(new SelectListItem() { Text = "Active", Value = "1" });
            li.Add(new SelectListItem() { Text = "In-Active", Value = "0" });
            ViewBag.abc = new SelectList(li, "Value", "Text");


            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName");
            return View();
        }

        // POST: Distributers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DistributerId,Name,Status,UserID")] Distributer distributer)
        {
            if (ModelState.IsValid)
            {
                db.Distributers.Add(distributer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", distributer.UserID);
            return View(distributer);
        }

        // GET: Distributers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Distributer distributer = db.Distributers.Find(id);
            if (distributer == null)
            {
                return HttpNotFound();
            }
            List<SelectListItem> li = new List<SelectListItem>();
            li.Add(new SelectListItem() { Text = "Active", Value = "1" });
            li.Add(new SelectListItem() { Text = "In-Active", Value = "0" });
            ViewBag.abc = new SelectList(li, "Value", "Text");

           


            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", distributer.UserID);
            return View(distributer);
        }

        // POST: Distributers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DistributerId,Name,Status,UserID")] Distributer distributer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(distributer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", distributer.UserID);
            return View(distributer);
        }

        // GET: Distributers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Distributer distributer = db.Distributers.Find(id);
            if (distributer == null)
            {
                return HttpNotFound();
            }
            return View(distributer);
        }

        // POST: Distributers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Distributer distributer = db.Distributers.Find(id);
            db.Distributers.Remove(distributer);
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
