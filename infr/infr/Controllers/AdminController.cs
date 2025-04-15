using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Configuration;
using infr.Models;
using System.Web.Security;
using System.Security.Claims;

namespace infr.Controllers
{
    public class AdminController : Controller
    {

        private readonly string connectionString = ConfigurationManager.ConnectionStrings["TestDbConnection"].ConnectionString;

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
            [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(user user)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM information WHERE Email = @Email AND Password = @Password AND Status = @Status";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Email", user.Email);
                    cmd.Parameters.AddWithValue("@Status", user.Status);
                    cmd.Parameters.AddWithValue("@Password", user.Password);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        Session["UserID"] = reader["Id"].ToString();
                        Session["UserName"] = reader["Name"].ToString();
                        Session["UserEmail"] = user.Email;
                        Session["UserPhone"] = reader["Phone"].ToString();

                        return RedirectToAction("Login_admin", "Admin");
                    }
                    conn.Close();
                }

                ViewBag.ErrorMessage = "Invalid email or password.";
            }
            return View(user);
        }
        public ActionResult Login_Admin()
        {
            return View();
        }
        public ActionResult User()
        {

            return View();
        }
        public ActionResult Application()
        {
            return View();
        }
    }
}