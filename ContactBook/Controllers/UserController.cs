using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContactBook.Models;

namespace ContactBook.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Register()
        {
            return View();
        }
        public ActionResult SaveUser(User user)
        {
            return Content("Record received successfully");
        }

        public ActionResult Cancel()
        {
            return View("Register");
        }

    }
}