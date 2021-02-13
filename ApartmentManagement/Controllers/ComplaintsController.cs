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
    public class ComplaintsController : Controller
    {
        private ApartmentManagementEntities db = new ApartmentManagementEntities();

        // GET: Complaints
        [Authorize(Roles= "Administrator")]
        public ActionResult Index()
        {
            var complaints = db.Complaints.Include(c => c.apartment);
            return View(complaints.ToList());
        }

        // GET: Complaints/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Complaint complaint = db.Complaints.Find(id);
            if (complaint == null)
            {
                return HttpNotFound();
            }
            return View(complaint);
        }
        [Authorize(Roles="Owner")]
        public ActionResult ApartmentComplaintList(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var complaints = db.Complaints.Include(c => c.apartment).Where(a => a.apartment_id == id).ToList();
            var aparment = db.Apartments.Include(a => a.Building).Where(a => a.apartment_id == id);
            var apartmentNumber = aparment.Select(a => a.apartment_number).SingleOrDefault();
            var BuildingName = aparment.Select(a => a.Building.name).SingleOrDefault();
            if (complaints == null)
            {
                return HttpNotFound();
            }
            ViewBag.apartmentNumber = apartmentNumber;
            ViewBag.BuildingName = BuildingName;
            return View(complaints);
        }
        [Authorize(Roles = "Renter")]
        public ActionResult RenterComplaints()
        {
            var RenterId = (Guid)Membership.GetUser(User.Identity.Name).ProviderUserKey;
            var complaints = db.Complaints.Include(a => a.apartment).Include(a=>a.apartment.Building).Where(a => a.apartment.renter_id == RenterId).ToList();
            return View(complaints);
        }

        // GET: Complaints/Create
        [Authorize(Roles = "Renter")]
        public ActionResult Create(string id)
        {
            ViewBag.apartment_id = new SelectList(db.Apartments.Where(a=>a.apartment_id==id), "apartment_id", "apartment_id");
            var apartment = db.Apartments.Where(a => a.apartment_id == id);
            var apartmentNumber = apartment.Select(a => a.apartment_number).SingleOrDefault();
            var buildingName = db.Apartments.Include(a=>a.Building).Where(a => a.apartment_id == id).Select(a => a.Building.name).SingleOrDefault();
            ViewBag.apartmentNumber = apartmentNumber;
            ViewBag.buildingName = buildingName;
            return View();
        }

        // POST: Complaints/Create
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Renter")]
        public ActionResult Create([Bind(Include = "complaint_id,title,text,date,apartment_id")] Complaint complaint)
        {
            complaint.date = new DateTime(DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day);
            if (ModelState.IsValid)
            {
                db.Complaints.Add(complaint);
                db.SaveChanges();
                return RedirectToAction("Index","Home");
            }

            ViewBag.apartment_id = new SelectList(db.Apartments, "apartment_id", "apartment_id", complaint.apartment_id);
            return View(complaint);
        }

        // GET: Complaints/Edit/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Complaint complaint = db.Complaints.Find(id);
            if (complaint == null)
            {
                return HttpNotFound();
            }
            ViewBag.apartment_id = new SelectList(db.Apartments, "apartment_id", "apartment_id", complaint.apartment_id);
            return View(complaint);
        }

        // POST: Complaints/Edit/5
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit([Bind(Include = "complaint_id,title,text,date,apartment_id")] Complaint complaint)
        {
            if (ModelState.IsValid)
            {
                db.Entry(complaint).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.apartment_id = new SelectList(db.Apartments, "apartment_id", "apartment_id", complaint.apartment_id);
            return View(complaint);
        }

        // GET: Complaints/Delete/5
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Complaint complaint = db.Complaints.Find(id);
            if (complaint == null)
            {
                return HttpNotFound();
            }
            return View(complaint);
        }

        // POST: Complaints/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public ActionResult DeleteConfirmed(int id)
        {
            Complaint complaint = db.Complaints.Find(id);
            db.Complaints.Remove(complaint);
            db.SaveChanges();
            return RedirectToAction("Index");
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
