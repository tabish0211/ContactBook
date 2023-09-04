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
            string cmdStr = "insert into tblUser values(@username,@Password,@gender)";
            SqlCommand cmd=new SqlCommand (cmdStr,con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@username", user.UserName);
            cmd.Parameters.AddWithValue("@Password", user.Password);

            if (user.genderF.ToString()=="F")
            {
                cmd.Parameters.AddWithValue("@gender", user.genderF);
            }


            if (user.genderM.ToString()=="M")
            {
                cmd.Parameters.AddWithValue("@gender", user.genderM);
            }

            int rowSucceeded = cmd.ExecuteNonQuery();

            cmd.Dispose();
            con.Close();

            User userDtl=FetchRecord(user);


            return View("UserDetail",userDtl);
        }

        private User FetchRecord(User user)
        {
            SqlConnection con = new SqlConnection("server=DESKTOP-UB4P45V;database=contact_db;integrated security=true;");
            con.Open();
            string cmdStr = "select * from tblUser where username=@username";
            SqlCommand cmd = new SqlCommand(cmdStr, con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@username", user.UserName);

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    user.UserName = dr["username"].ToString();
                    user.Password = dr["password"].ToString();
                    string gender = dr["gender"].ToString();
                    if (gender=="F")
                    {
                        user.genderF = gender[0];
                    }

                    if (gender == "M")
                    {
                        user.genderM = gender[0];
                    }
                }
            }

            dr.Close();

            return user;

        }

        public ActionResult Cancel()
        {
            return View("Register");
        }

    }
}