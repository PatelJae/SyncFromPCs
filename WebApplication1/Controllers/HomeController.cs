using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Table t)
        {
            // this action is for handle post login
            if (ModelState.IsValid)
            {
                using (MyTestDatabaseEntities dc = new MyTestDatabaseEntities())
                {
                    var v = dc.Tables.Where(a => a.UserName.Equals(t.UserName) && a.Password.Equals(t.Password)).FirstOrDefault();
                    if (v != null)
                    {
                        Session["LogedUserID"] = t.UserID.ToString();
                        Session["LoggedUserFullName"] = t.FullName.ToString();
                        return RedirectToAction("AfterLogin");
                    }

                }
            }

            return View(t);
        }

        public ActionResult AfterLogin()
        {
            if (Session ["LogedUserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

    }
}
