using ApartmentManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace ApartmentManagement.Controllers
{
    public class HomeController : Controller
    {
        ApartmentManagementEntities storeDb = new ApartmentManagementEntities();
        public ActionResult Index()
        {
            var owners = storeDb.Renters.ToList();
            return View(owners);
        }

        [ChildActionOnly]
        public ActionResult HomeApartments()
        {
            var apartments = storeDb.Apartments.Include(b => b.Building).ToList();
            return PartialView(apartments);
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
    }
}