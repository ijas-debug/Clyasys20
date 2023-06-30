using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Reg_User.Models;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Security.Cryptography;
using System.Web.Security;
using System.IO;

namespace MVC_Reg_User.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        // GET: Login
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Index(LoginClass lc)
        {

            string mainconn = ConfigurationManager.ConnectionStrings["Myconnection"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);
            SqlCommand sqlcomm = new SqlCommand("sp_Login", sqlconn);

            sqlcomm.CommandType = CommandType.StoredProcedure;
            sqlcomm.Parameters.AddWithValue("@EmailAddress", lc.EmailAddress);
            sqlcomm.Parameters.AddWithValue("@Password", lc.Password);

            sqlconn.Open();
            SqlDataReader sqr = sqlcomm.ExecuteReader();

            if (sqr.Read())
            {
                FormsAuthentication.SetAuthCookie(lc.EmailAddress, true);
                Session["emailid"] = lc.EmailAddress.ToString();
                return RedirectToAction("welcome", "Login");
            }
            else
            {
                ViewData["message"] = "Username & Password are wrong !";
            }
            sqlconn.Close();
            return View();
        }
        public ActionResult welcome()
        {
            string displayimg = (string)Session["emailid"];
            string mainconn = ConfigurationManager.ConnectionStrings["Myconnection"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);
            string sqlquery = "select * from [dbo].[MVCregUser] where EmailAddress='" + displayimg + "'";
            sqlconn.Open();
            SqlCommand sqlcomm = new SqlCommand(sqlquery, sqlconn);
            sqlcomm.Parameters.AddWithValue("EmailAddress", Session["emailid"].ToString());
            SqlDataReader sdr = sqlcomm.ExecuteReader();

            UserClass user = new UserClass();
            if (sdr.Read())
            {
                string s = sdr["Uimg"].ToString();
                ViewData["Img"] = s;
                TempData["Oldimg"] = s;


                user.FirstName = sdr["FirstName"].ToString();
                user.LastName = sdr["LastName"].ToString();
                user.DateOfBirth = (DateTime)sdr["DateOfBirth"];
                user.Gender = sdr["Gender"].ToString();
                user.PhoneNumber = sdr["PhoneNumber"].ToString();
                user.EmailAddress = sdr["EmailAddress"].ToString();
                user.Address = sdr["Address"].ToString();
                user.State = sdr["State"].ToString();
                user.City = sdr["City"].ToString();
                user.Username = sdr["Username"].ToString();
                user.Password = sdr["Password"].ToString();

            }
            sqlconn.Close();
            return View(user);
        }
        public ActionResult userimgchange(HttpPostedFileBase file)
        {
            var emailId = (string)Session["emailid"];

            string imgpath = Server.MapPath((string)TempData["Oldimg"]);
            string fileimgpath = imgpath;
            FileInfo fi = new FileInfo(fileimgpath);
            if (fi.Exists)
            {
                fi.Delete();
            }

            if (file != null && file.ContentLength > 0)
            {
                string filename = Path.GetFileName(file.FileName);
                string filepath = Path.Combine(Server.MapPath("/User-Images/"), filename);
                file.SaveAs(filepath);

                string mainconn = ConfigurationManager.ConnectionStrings["Myconnection"].ConnectionString;
                using (SqlConnection sqlconn = new SqlConnection(mainconn))
                {
                    sqlconn.Open();
                    string sqlquery = "UPDATE [dbo].[MVCregUser] SET  Uimg = @Uimg WHERE EmailAddress = @EmailAddress";
                    SqlCommand sqlcomm = new SqlCommand(sqlquery, sqlconn);
                    sqlcomm.Parameters.AddWithValue("@Uimg", "/User-Images/" + filename);
                    sqlcomm.Parameters.AddWithValue("@EmailAddress", emailId);
                    sqlcomm.ExecuteNonQuery();
                   
                }
            }

            return RedirectToAction("welcome", "Login");
        }

      
    }
}
