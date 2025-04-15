using System;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Configuration;
using insurence.Models;
using System.Linq;
using System.Collections.Generic;

namespace insurence.Controllers
{
    public class AccountController : Controller
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["TestDbConnection"].ConnectionString;

        // GET: Login Page
        public ActionResult Login()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Product product)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT Id, Name, Phone FROM information WHERE Email = @Email AND Password = @Password";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Email", product.Email);
                    cmd.Parameters.AddWithValue("@Password", product.Password);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())  // If user exists
                    {
                        Session["UserID"] = reader["Id"].ToString();
                        Session["UserName"] = reader["Name"].ToString();
                        Session["UserEmail"] = product.Email;
                        Session["UserPhone"] = reader["Phone"].ToString();

                        return RedirectToAction("Index", "Account");  // Redirect to dashboard
                    }
                    conn.Close();
                }

                ViewBag.ErrorMessage = "Invalid email or password.";
            }
            return View(product);
        }

        // GET: Logout
        public ActionResult Logout()
        {
            Session.Clear();  // Clear all session data
            Session.Abandon(); // End the session

            return RedirectToAction("Login", "Account"); // Redirect to Login Page
        }
            /*ViewBag.UserName = Session["UserName"];
            ViewBag.UserEmail = Session["UserEmail"];
            ViewBag.UserPhone = Session["UserPhone"];*/

        // List all policies
        public ActionResult Index()
        {
            var policies = new List<Policy>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Policies";
                SqlCommand cmd = new SqlCommand(query, conn);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    policies.Add(new Policy
                    {
                        PolicyId = Convert.ToInt32(reader["PolicyId"]),
                        PolicyName = reader["PolicyName"].ToString(),
                        PolicyType = reader["PolicyType"].ToString(),
                        Description = reader["Description"].ToString(),
                        PremiumAmount = Convert.ToDecimal(reader["PremiumAmount"]),
                        CoverageAmount = Convert.ToDecimal(reader["CoverageAmount"]),
                        DurationInYears = Convert.ToInt32(reader["DurationInYears"])
                    });
                }
                conn.Close();
            }

            return View(policies);
        }

        // Create Policy (GET)
        public ActionResult Create()
        {
            return View();
        }

        // Create Policy (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Policy policy)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO Policies (PolicyName, PolicyType, Description, PremiumAmount, CoverageAmount, DurationInYears) VALUES (@PolicyName, @PolicyType, @Description, @PremiumAmount, @CoverageAmount, @DurationInYears)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@PolicyName", policy.PolicyName);
                    cmd.Parameters.AddWithValue("@PolicyType", policy.PolicyType);
                    cmd.Parameters.AddWithValue("@Description", policy.Description);
                    cmd.Parameters.AddWithValue("@PremiumAmount", policy.PremiumAmount);
                    cmd.Parameters.AddWithValue("@CoverageAmount", policy.CoverageAmount);
                    cmd.Parameters.AddWithValue("@DurationInYears", policy.DurationInYears);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }

                return RedirectToAction("Index");
            }

            return View(policy);
        }

        // Edit Policy (GET)
        public ActionResult Edit(int id)
        {
            Policy policy = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Policies WHERE PolicyId = @PolicyId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@PolicyId", id);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    policy = new Policy
                    {
                        PolicyId = Convert.ToInt32(reader["PolicyId"]),
                        PolicyName = reader["PolicyName"].ToString(),
                        PolicyType = reader["PolicyType"].ToString(),
                        Description = reader["Description"].ToString(),
                        PremiumAmount = Convert.ToDecimal(reader["PremiumAmount"]),
                        CoverageAmount = Convert.ToDecimal(reader["CoverageAmount"]),
                        DurationInYears = Convert.ToInt32(reader["DurationInYears"])
                    };
                }
                conn.Close();
            }

            if (policy == null)
            {
                return HttpNotFound();
            }

            return View(policy);
        }

        // Edit Policy (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Policy policy)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        string query = "UPDATE Policies SET PolicyName = @PolicyName, PolicyType = @PolicyType, Description = @Description, PremiumAmount = @PremiumAmount, CoverageAmount = @CoverageAmount, DurationInYears = @DurationInYears WHERE PolicyId = @PolicyId";

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@PolicyId", policy.PolicyId);
                            cmd.Parameters.AddWithValue("@PolicyName", policy.PolicyName ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@PolicyType", policy.PolicyType ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@Description", policy.Description ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@PremiumAmount", policy.PremiumAmount);
                            cmd.Parameters.AddWithValue("@CoverageAmount", policy.CoverageAmount);
                            cmd.Parameters.AddWithValue("@DurationInYears", policy.DurationInYears);

                            conn.Open();
                            int rowsAffected = cmd.ExecuteNonQuery();
                            conn.Close();

                            if (rowsAffected > 0)
                            {
                                TempData["SuccessMessage"] = "Policy updated successfully!";
                            }
                            else
                            {
                                TempData["ErrorMessage"] = "No record updated. PolicyId may not exist.";
                            }
                        }
                    }
                    return RedirectToAction("Index");
                }
                catch (SqlException ex)
                {
                    TempData["ErrorMessage"] = "SQL Error: " + ex.Message;
                    return View(policy);
                }
            }

            return View(policy);
        }


        // Delete Policy
        public ActionResult Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Policies WHERE PolicyId = @PolicyId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@PolicyId", id);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }

            return RedirectToAction("Index");
        }
        
    }
}
