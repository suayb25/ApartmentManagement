using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ApartmentManagement.Models;

namespace ApartmentManagement.Controllers
{
    public class BuildingsController : Controller
    {
        private ApartmentManagementEntities db = new ApartmentManagementEntities();

        // GET: Buildings
        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            return View(db.Buildings.ToList());
        }

        // GET: Buildings/Details/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Building building = db.Buildings.Find(id);
            if (building == null)
            {
                return HttpNotFound();
            }
            return View(building);
        }

        // GET: Buildings/Details/5
        [Authorize(Roles = "Owner")]
        public ActionResult OwnerBuildingDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Building building = db.Buildings.Find(id);
            if (building == null)
            {
                return HttpNotFound();
            }
            return View(building);
        }

        // GET: Buildings/Edit/5
        [Authorize(Roles = "Owner")]
        public ActionResult OwnerBuildingEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Building building = db.Buildings.Find(id);
            if (building == null)
            {
                return HttpNotFound();
            }
            return View(building);
        }

        // POST: Buildings/Edit/5
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Owner")]
        public ActionResult OwnerBuildingEdit([Bind(Include = "building_id,name,address,owner_id")] Building building)
        {
            if (ModelState.IsValid)
            {
                db.Entry(building).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index","Home");
            }
            return View(building);
        }


        // GET: Buildings/OwnerCreate
        [Authorize(Roles = "Owner")]
        public ActionResult OwnerCreate()
        {
            return View();
        }

        // POST: Buildings/OwnerCreate
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Owner")]
        public ActionResult OwnerCreate([Bind(Include = "building_id,name,address")] Building building)//For creating new building for Owners
        {
            if (ModelState.IsValid)
            {

                building.owner_id = (Guid)Membership.GetUser(User.Identity.Name).ProviderUserKey;
                db.Buildings.Add(building);
                db.SaveChanges();
                return RedirectToAction("Index","Home");
            }

            return View(building);
        }

        // GET: Buildings/Create
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Buildings/Create
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create([Bind(Include = "building_id,name,address,owner_id")] Building building)
        {
            if (ModelState.IsValid)
            {
                db.Buildings.Add(building);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(building);
        }

        // GET: Buildings/Edit/5
        [Authorize(Roles = "Administrator,Owner")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Building building = db.Buildings.Find(id);
            if (building == null)
            {
                return HttpNotFound();
            }
            return View(building);
        }

        // POST: Buildings/Edit/5
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,Owner")]
        public ActionResult Edit([Bind(Include = "building_id,name,address,owner_id")] Building building)
        {
            if (ModelState.IsValid)
            {
                db.Entry(building).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(building);
        }

        // GET: Buildings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Building building = db.Buildings.Find(id);
            if (building == null)
            {
                return HttpNotFound();
            }
            return View(building);
        }

        // POST: Buildings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Building building = db.Buildings.Find(id);
            var apartments = db.Apartments.Where(a => a.building_id == id).ToList();
            var complaints = db.Complaints.ToList();
            var bills = db.Bills.ToList();
            foreach (var item in apartments)
            {
                foreach(var item2 in complaints)
                {
                    if (item.apartment_id == item2.apartment_id)
                    {
                        db.Complaints.Remove(item2);
                    }
                }
                foreach (var item3 in bills)
                {
                    if (item.apartment_id == item3.apartment_id)
                    {
                        db.Bills.Remove(item3);
                    }
                }
            }
            apartments.ForEach(a => db.Apartments.Remove(a));
            db.Buildings.Remove(building);
            db.SaveChanges();
            return RedirectToAction("Index","Home");
        }

        [ChildActionOnly]
        public ActionResult BuildingList()
        {
            var buildings = db.Buildings.ToList();
            return PartialView(buildings);
        }

        [ChildActionOnly]
        public ActionResult OwnerBuildingList()
        {
            var OwnerId = (Guid)Membership.GetUser(User.Identity.Name).ProviderUserKey;
            var buildings = db.Buildings.Where(x=>x.owner_id==OwnerId).ToList();
            return PartialView(buildings);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
