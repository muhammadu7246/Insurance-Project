using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Configuration;
using insurence.Models;
using System.Web.Security;

namespace insurence.Controllers
{
    public class ProductController : Controller
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["TestDbConnection"].ConnectionString;

        // GET: Product/Index
        public ActionResult Index()
        {
            var products = new List<Product>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT Id, Name, Email, Phone, Password FROM information";  // Fetch all required fields
                SqlCommand cmd = new SqlCommand(query, conn);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    products.Add(new Product
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Name = reader["Name"].ToString(),
                        Email = reader["Email"].ToString(),
                        Phone = reader["Phone"].ToString(),
                        Password = reader["Password"].ToString()
                    });
                }
                conn.Close();
            }

            return View(products);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View();  // Load Create Product View
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        string query = "INSERT INTO information (Name, Email, Phone, Password) VALUES (@Name, @Email, @Phone, @Password)";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@Name", product.Name);
                        cmd.Parameters.AddWithValue("@Email", product.Email);
                        cmd.Parameters.AddWithValue("@Phone", product.Phone);
                        cmd.Parameters.AddWithValue("@Password", product.Password);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }

                    return RedirectToAction("Login", "Account"); // Redirect to login after successful registration
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = "Error: " + ex.Message;
                }
            }

            return View(product);
        }

        public ActionResult Login() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Product product)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM information WHERE Email = @Email AND Password = @Password";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Email", product.Email);
                    cmd.Parameters.AddWithValue("@Password", product.Password);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        string userID = reader["Id"].ToString();
                        string userName = reader["Name"].ToString();

                        // Store user details in session
                        Session["UserID"] = userID;
                        Session["UserName"] = userName;

                        return RedirectToAction("index", "User");
                    }
                    conn.Close();
                }

                ViewBag.ErrorMessage = "Invalid email or password.";
            }
            return View(product);

        }

        public ActionResult Logout()
        {
            Session.Clear(); // Clear all session data
            Session.Abandon(); // End the session
            FormsAuthentication.SignOut(); // Sign out the user (if using FormsAuthentication)

            return RedirectToAction("Login", "Account"); // Redirect to Login Page
        }

        // GET: Product/Edit/{id}
        public ActionResult Edit(int id)
        {
            Product product = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT Id, Name, Email, Phone, Password FROM information WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    product = new Product
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Name = reader["Name"].ToString(),
                        Email = reader["Email"].ToString(),
                        Phone = reader["Phone"].ToString(),
                        Password = reader["Password"].ToString()
                    };
                }
                conn.Close();
            }

            if (product == null)
            {
                return HttpNotFound();  // Return 404 if product not found
            }

            return View(product);  // Load Edit View with product details
        }

        // POST: Product/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Product product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        string query = "UPDATE information SET Name = @Name, Email = @Email, Phone = @Phone, Password = @Password WHERE Id = @Id";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@Id", id);
                        cmd.Parameters.AddWithValue("@Name", product.Name);
                        cmd.Parameters.AddWithValue("@Email", product.Email);
                        cmd.Parameters.AddWithValue("@Phone", product.Phone);
                        cmd.Parameters.AddWithValue("@Password", product.Password);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }

                    return RedirectToAction("Index");  // Redirect after successful update
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = "Error: " + ex.Message;
                    return View(product);
                }
            }

            return View(product);  // Return the view with validation errors
        }

        // GET: Product/Delete/{id}
        public ActionResult Delete(int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM information WHERE Id = @Id";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Id", id);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }

                return RedirectToAction("Index"); // Redirect to Index after deletion
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Error: " + ex.Message;
                return RedirectToAction("Index"); // Redirect to Index in case of an error
            }
        }
    }
}
