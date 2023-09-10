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
            string cmdStr = "insert into tblUser values(@username,@Password,@gender,@contactnumber,@emailid)";
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
            cmd.Parameters.AddWithValue("@contactnumber", user.MobileNumber);
            cmd.Parameters.AddWithValue("@emailid", user.EmailId);

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
                    user.MobileNumber = dr["contactnumber"].ToString();
                    user.EmailId = dr["emailid"].ToString();
                }
            }

            dr.Close();

            return user;

        }

        public ActionResult Search() {

            SqlConnection con = new SqlConnection("server=DESKTOP-UB4P45V;database=contact_db;integrated security=true;");
            con.Open();
            string cmdStr = "select * from tblUser";
            SqlCommand cmd = new SqlCommand(cmdStr, con);
            cmd.CommandType = CommandType.Text;
                      

            SqlDataReader dr = cmd.ExecuteReader();
            List<User> lstofUsers = new List<User>();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    User user = new User();
                    user.UserName = dr["username"].ToString();
                    user.Password = dr["password"].ToString();
                    string gender = dr["gender"].ToString();
                    if (gender == "F")
                    {
                        user.genderF = gender[0];
                    }

                    if (gender == "M")
                    {
                        user.genderM = gender[0];
                    }
                    user.MobileNumber = dr["contactnumber"].ToString();
                    user.EmailId = dr["emailid"].ToString();

                    lstofUsers.Add(user);
                }
            }

            dr.Close();

            ViewBag.Users = lstofUsers;
            return View();

        }

        public ActionResult Delete()
        {

            return View();
        }
        public ActionResult DeleteUser(User user)
        {
            SqlConnection con = new SqlConnection("server=DESKTOP-UB4P45V;database=contact_db;integrated security=true;");
            con.Open();
            string cmdStr = "delete from tblUser where username=@username";
            SqlCommand cmd = new SqlCommand(cmdStr, con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@username", user.UserName);
           
            int rowSucceeded = cmd.ExecuteNonQuery();

            cmd.Dispose();
            con.Close();

            return Content("Record Deleted successfully");

       

        }

        public ActionResult Edit()
        {
            return View();

        }
        public ActionResult EditUser(User user)
        {
            SqlConnection con = new SqlConnection("server=DESKTOP-UB4P45V;database=contact_db;integrated security=true;");
            con.Open();
            string cmdStr = "update tbluser set contactnumber=@contactnumber,emailid=@emailid where username=@username";
            SqlCommand cmd = new SqlCommand(cmdStr, con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@username", user.UserName);
            cmd.Parameters.AddWithValue("@contactnumber", user.MobileNumber);
            cmd.Parameters.AddWithValue("@emailid", user.EmailId);
            int rowSucceeded = cmd.ExecuteNonQuery();

            cmd.Dispose();
            con.Close();

            return Content("Record modified successfully");


        }

        public ActionResult SearchUser(User user)
        {
            user = FetchRecord(user);

            ViewBag.User = user;
            return View("Edit");
        }
        public ActionResult Cancel()
        {
            return View("Register");
        }

    }
}