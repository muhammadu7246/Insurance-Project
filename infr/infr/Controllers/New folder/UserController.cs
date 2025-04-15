using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace insurence.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        [Authorize]
        public ActionResult Index()
        {
            ViewBag.UserName = Session["UserName"];
            ViewBag.UserEmail = Session["UserEmail"];
            ViewBag.UserPhone = Session["UserPhone"];
            return View();
        }
    }
}