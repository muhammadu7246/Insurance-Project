using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Configuration;
using infr.Models;


namespace infr.Controllers
{
    public class HomeController : Controller
    {
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
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Medical_insurance()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult moter_insurance()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Home_insurance()
        {
            ViewBag.Message = "Your contact page.";

            return View();
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
        public ActionResult registration()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult policy_Proposal()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        // Create Policy (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Policy policy)
        {
            /*if (ModelState.IsValid)
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
*/
            return View(policy);
        }


    }
}