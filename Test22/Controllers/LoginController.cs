using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Test22.Models;

namespace Test22.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Autherize(Test22.Models.User userModel)
        {
            using (Test22Entities db = new Test22Entities())
            {
                var userDetails = db.Users.Where(x => x.UserName == userModel.UserName && x.Password == userModel.Password).FirstOrDefault();
                if (userDetails == null)
                {

                    return View("Index", userModel);
                }
                else
                {
                    Session["usedID"] = userDetails.UserID;
                    Session["userName"] = userDetails.UserName;
                    return RedirectToAction("Index", "Dashboard");
                }
            }

        }
        public ActionResult LogOut()
        {
            int userID = (int)Session["usedID"];
            Session.Abandon();
            return RedirectToAction("Index", "Login");

        }


    }
}