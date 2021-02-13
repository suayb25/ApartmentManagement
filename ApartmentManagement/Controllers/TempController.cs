using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ApartmentManagement.Controllers
{
    public class TempController : Controller
    {
        // GET: Temp
        // To create roles
        public ActionResult Index()
        {
            Roles.CreateRole("Administrator");
            Roles.CreateRole("Owner");
            Roles.CreateRole("Renter");
            Membership.CreateUser("admin", "admin123", "admin@talha.net");
            Roles.AddUserToRole("admin", "Administrator");
            return View();
        }
    }
}