using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ContactBook.Models;

//ADO.net---

using System.Data;
using System.Data.SqlClient;

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
            if (string.IsNullOrEmpty(user.UserName))
            {
                return RedirectToAction("Cancel");
            }

            SqlConnection con = new SqlConnection("server=DESKTOP-UB4P45V;database=contact_db;integrated security=true;");
            con.Open();
            string cmd= "insert into tblUser values(@username,@use"
            SqlCommand cmd=new SqlCommand ()


            return Content("Record received successfully");
        }

        public ActionResult Cancel()
        {
            return View("Register");
        }

    }
}