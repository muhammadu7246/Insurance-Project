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
using Claim = infr.Models.Claim;

namespace infr.Controllers
{
    public class UserController : Controller
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["TestDbConnection"].ConnectionString;

        // GET: Registration Page
        public ActionResult Registration()
        {
            return View();
        }

        // POST: User Registration
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration(user user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        string query = "INSERT INTO information (Name, Email, Phone, Password) VALUES (@Name, @Email, @Phone, @Password)";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@Name", user.Name);
                            cmd.Parameters.AddWithValue("@Email", user.Email);
                            cmd.Parameters.AddWithValue("@Phone", user.Phone);
                            cmd.Parameters.AddWithValue("@Password", user.Password);

                            conn.Open();
                            cmd.ExecuteNonQuery();
                        }
                    }

                    return RedirectToAction("Login", "User");
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = "Error: " + ex.Message;
                }
            }

            return View(user);
        }

        // GET: Login Page
        public ActionResult Login()
        {
            return View();
        }

        // POST: User Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(user user)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT Id, Name, Phone FROM information WHERE Email = @Email AND Password = @Password";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Email", user.Email);
                    cmd.Parameters.AddWithValue("@Password", user.Password);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        Session["UserID"] = reader["Id"].ToString();
                        Session["UserName"] = reader["Name"].ToString();
                        Session["UserEmail"] = user.Email;
                        Session["UserPhone"] = reader["Phone"].ToString();

                        return RedirectToAction("Index", "User");
                    }
                    conn.Close();
                }

                ViewBag.ErrorMessage = "Invalid email or password.";
            }
            return View(user);
        }

        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Login", "User");
        }

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult FAQS()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult insurance()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult life_insurance()
        {

            var policies = new List<Policy>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Policies WHERE PolicyType = 'Life'";
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

        public ActionResult Medical_insurance()
        {
            var policies = new List<Policy>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Policies WHERE PolicyType = 'medical'";
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
        public ActionResult moter_insurance()
        {
            var policies = new List<Policy>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Policies WHERE PolicyType = 'motor'";
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
        public ActionResult Home_insurance()
        {
            var policies = new List<Policy>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Policies WHERE PolicyType = 'home'";
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
        /*public ActionResult Activated()
        {
            var claims = new List<Models.Claim>(); // Create list to store claims

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Claims"; // Fetch all claims

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            claims.Add(new Claim
                            {
                                ClaimId = reader["ClaimId"] != DBNull.Value ? Convert.ToInt32(reader["ClaimId"]) : 0,
                                HolderId = reader["HolderId"] != DBNull.Value ? Convert.ToInt32(reader["HolderId"]) : 0,
                                UserId = reader["UserId"] != DBNull.Value ? Convert.ToInt32(reader["UserId"]) : 0,
                                ClaimDate = reader["ClaimDate"] != DBNull.Value ? Convert.ToDateTime(reader["ClaimDate"]) : DateTime.MinValue,
                                ClaimAmount = reader["ClaimAmount"] != DBNull.Value ? Convert.ToDecimal(reader["ClaimAmount"]) : 0,
                                Status = reader["Status"] != DBNull.Value ? reader["Status"].ToString() : "Unknown"
                            });
                        }
                    }
                }
            }

            return View(claims); // Pass the claims list to the view
        }*/

        public ActionResult Activated()
        {
            var claims = new List<Claim>(); // Create list to store claims
            int userId = Convert.ToInt32(Session["UserID"]); // Get logged-in user ID

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Claims WHERE UserId = @UserId"; // Filter claims by UserId

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId); // Use parameterized query to prevent SQL injection
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            claims.Add(new Claim
                            {
                                ClaimId = reader["ClaimId"] != DBNull.Value ? Convert.ToInt32(reader["ClaimId"]) : 0,
                                HolderId = reader["HolderId"] != DBNull.Value ? Convert.ToInt32(reader["HolderId"]) : 0,
                                UserId = reader["UserId"] != DBNull.Value ? Convert.ToInt32(reader["UserId"]) : 0,
                                ClaimDate = reader["ClaimDate"] != DBNull.Value ? Convert.ToDateTime(reader["ClaimDate"]) : DateTime.MinValue,
                                ClaimAmount = reader["ClaimAmount"] != DBNull.Value ? Convert.ToDecimal(reader["ClaimAmount"]) : 0,
                                Status = reader["Status"] != DBNull.Value ? reader["Status"].ToString() : "Unknown"
                            });
                        }
                    }
                }
            }

            return View(claims); // Pass the filtered claims list to the view
        }




        public ActionResult portfolio()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult price()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        public ActionResult PolicyProposal()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PolicyProposal(PolicyHolder policyHolder)
        {
            if (policyHolder.EndDate <= policyHolder.StartDate)
            {
                ModelState.AddModelError("", "End date must be greater than start date.");
                return View();
            }

            decimal premiumAmount = 0;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Fetch policy details
                string query = "SELECT CoverageAmount, DurationInYears, PolicyType FROM Policies WHERE PolicyId = @PolicyId";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PolicyId", policyHolder.PolicyId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            decimal coverageAmount = Convert.ToDecimal(reader["CoverageAmount"]);
                            int duration = Convert.ToInt32(reader["DurationInYears"]);
                            string policyType = reader["PolicyType"].ToString();

                            decimal premiumRate = policyType == "Term Plan" ? 0.05m : 0.07m;
                            premiumAmount = (coverageAmount / duration) * premiumRate;
                        }
                        else
                        {
                            ModelState.AddModelError("", "Invalid policy selected.");
                            return View();
                        }
                    }
                }

                // Insert policy holder details
                string insertQuery = @"INSERT INTO PolicyHolders (UserId, PolicyId, StartDate, EndDate, PremiumAmount) 
                           VALUES (@UserId, @PolicyId, @StartDate, @EndDate, @PremiumAmount)";

                using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", policyHolder.UserId);
                    cmd.Parameters.AddWithValue("@PolicyId", policyHolder.PolicyId);
                    cmd.Parameters.AddWithValue("@StartDate", policyHolder.StartDate);
                    cmd.Parameters.AddWithValue("@EndDate", policyHolder.EndDate);
                    cmd.Parameters.AddWithValue("@PremiumAmount", premiumAmount);

                    cmd.ExecuteNonQuery();
                }
            }

            // Set success message in TempData
            TempData["SuccessMessage"] = "Policy Holder added successfully!";

            return RedirectToAction("Index");
        }
        }
    }
