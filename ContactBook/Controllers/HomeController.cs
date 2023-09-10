using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContactBook.Controllers
{
    public class HomeController : Controller
    {


        public ActionResult ContactHome()
        {
            return View();
        }


        //domain name/Home/Index


        public ActionResult Index()
        {
            TempData["myData"] = "I took the value from Index and sending to the about us";
            //return View("FirstPage");
            return RedirectToAction("About");
        }

        public ActionResult About()
        {

            // ViewData["my-info"] = "This is my about us page....content will be added soon ..thank you!!";
            //ViewBag.MyNewInfo = "This is my about us page....content will be added soon ..thank you!!";
            ViewBag.MyNewInfo = TempData["myData"];
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}