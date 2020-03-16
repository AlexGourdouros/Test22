using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Test22.Models;
using WebMatrix.Data;

namespace Test22.Controllers
{
    public class DashboardController : Controller
    {
        private Test22Entities db = new Test22Entities();

        // GET: Dashboard
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult BubbleBreak()
        {
            var db = new Test22Entities();
            ArrayList xValue = new ArrayList();
            ArrayList yValue = new ArrayList();

            String userName = Convert.ToString(Session["userName"]);
            var results = (from c in db.Items select c).Where(c => c.User.UserName == userName);
            results.ToList().ForEach(rs => xValue.Add(rs.Name));
            results.ToList().ForEach(rs => yValue.Add(rs.Quantity));

            new Chart(width: 600, height: 400, theme: ChartTheme.Blue)
                .AddTitle("Current Stock For Items")
                .AddSeries("Default", chartType: "Pie", xValue: xValue, yValues: yValue)
                .Write("bmp");

            return null;


        }

    }
}