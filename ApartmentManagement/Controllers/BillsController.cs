using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ApartmentManagement.Models;

namespace ApartmentManagement.Controllers
{
    public class BillsController : Controller
    {
        private ApartmentManagementEntities db = new ApartmentManagementEntities();
        public static int year1=2022;
        public static int month1 = 1;
        public static string apartmentId;

        // GET: Bills
        [Authorize(Roles = "Administrator")]
        public ActionResult Index()
        {
            var bills = db.Bills.Include(b => b.apartment);
            return View(bills.ToList());
        }

        // GET: Bills/Details/5
        [Authorize]
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bills bills = db.Bills.Find(id);
            if (bills == null)
            {
                return HttpNotFound();
            }
            return View(bills);
        }
        [Authorize]
        public ActionResult Pay(String ApartmentID, String BillName,DateTime date)//When we click bill we will see pay page
        {
            if (ApartmentID == null || date == null || BillName == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bills bills = db.Bills.Include(b=>b.apartment).Where(a=> a.apartment_id == ApartmentID && a.BillName==BillName && a.date==date).SingleOrDefault();
            if (bills == null)
            {
                return HttpNotFound();
            }
            ViewData["photoURL"] = "https://learndigital.withgoogle.com/dijitalatolye";
            return View(bills);
        }
        [Authorize]
        public ActionResult PaySuccess(int BillId,String photoURL)//When we click bill we will see pay page
        {
            if (photoURL == null || BillId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bills bills = db.Bills.Include(b => b.apartment).Where(a => a.bill_id == BillId).SingleOrDefault();
            if (bills == null)
            {
                return HttpNotFound();
            }
            bills.paid = true;
            bills.photoURL = photoURL;
            db.Entry(bills).State = EntityState.Modified;
            db.SaveChanges();
            return View(bills);
        }

        [ChildActionOnly]
        public ActionResult ApartmentBillList(String ApartmentID,int? year)
        {
            if (year == null)
            {
                year =  int.Parse(DateTime.Now.Year.ToString());
                //year = 2021;
            }
            DateTime dateTime = new DateTime((int)year, 12, 31);
            DateTime dateTime2 = new DateTime((int)year, 1, 1);
            if (ApartmentID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var bills = db.Bills.Include(b => b.apartment).Where(a=>a.apartment_id==ApartmentID && (a.date<dateTime && a.date>=dateTime2)).ToList();
            if (bills == null)
            {
                return HttpNotFound();
            }
            String[] months =  { "January","February", "March","April", "May","June","July", "August" , "September", "October", "November", "December" };
            ViewData["Months"] = months;
            ViewData["ApartmentID"] = ApartmentID;
            ViewData["year"] = year;
            ViewData["year1"] = year;
            return PartialView(bills);
        }

        // GET: Bills/Create
        [Authorize(Roles = "Owner")]
        public ActionResult Create(string id,string month,int year)
        {
            ViewBag.apartment_id = new SelectList(db.Apartments.Where(a=>a.apartment_id==id), "apartment_id", "apartment_id");
            if (month == "January")
            {
                month = "01";
                month1 = 1;
            }
            else if (month == "February")
            {
                month = "02";
                month1 = 2;
            }
            else if (month == "March")
            {
                month = "03";
                month1 = 3;
            }
            else if (month == "April")
            {
                month = "04";
                month1 = 4;
            }
            else if (month == "May")
            {
                month = "05";
                month1 = 5;
            }
            else if (month == "June")
            {
                month = "06";
                month1 = 6;
            }
            else if (month == "July")
            {
                month = "07";
                month1 = 7;
            }
            else if (month == "August")
            {
                month = "08";
                month1 = 8;
            }
            else if (month == "September")
            {
                month = "09";
                month1 = 9;
            }
            else if (month == "October")
            {
                month = "10";
                month1 = 10;
            }
            else if (month == "November")
            {
                month = "11";
                month1 = 11; 
            }
            else if (month == "December")
            {
                month = "12";
                month1 = 12;
            }
            DateTime dateTime = new DateTime(year, month1, 28);
            DateTime dateTime2 = new DateTime(year, month1, 1);
            var billnames = db.Bills.Where(a => a.apartment_id == id && (a.date < dateTime && a.date >= dateTime2)).Select(a => a.BillName).ToList();
            List<string> billNameList = new List<string>();
            if (!billnames.Contains("Electric"))
            {
                billNameList.Add("Electric");
            }
            if (!billnames.Contains("Naturalgas"))
            {
                billNameList.Add("Naturalgas");
            }
            if (!billnames.Contains("Water"))
            {
                billNameList.Add("Water");
            }
            if (!billnames.Contains("Rent"))
            {
                billNameList.Add("Rent");
            }
            SelectList billNameList1 = new SelectList(billNameList);
            //ViewBag.BillName = new SelectList(db.Bills.Take(4).ToList(), "BillName", "BillName");
            ViewBag.BillName = billNameList1;
            ViewBag.Month = month;
            ViewBag.Year = year;
            year1 = year;
            apartmentId = db.Apartments.Where(a => a.apartment_id == id).Select(a=>a.apartment_id).SingleOrDefault();
            return View();
        }

        // POST: Bills/Create
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Owner")]
        public ActionResult Create([Bind(Include = "date,apartment_id,BillName,price,paid")] Bills bills)
        {
            //var apartment = db.Apartments.Where(a => a.apartment_id == apartmentId).SingleOrDefault();
            //bills.apartment = apartment;
            bills.apartment_id = apartmentId;
            bills.photoURL = "http://placehold.it/700x400";
            bills.date = new DateTime(year1, month1, 1);
            if (ModelState.IsValid)
            {
                db.Bills.Add(bills);
                db.SaveChanges();
                return RedirectToAction("Index","Home");
            }

            ViewBag.apartment_id = new SelectList(db.Apartments, "apartment_id", "apartment_id", bills.apartment_id);
            return View(bills);
        }

        // GET: Bills/Edit/5
        [Authorize(Roles = "Owner")]
        public ActionResult Edit(String ApartmentID, String BillName, DateTime date)
        {
            if (ApartmentID==null || BillName==null || date == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bills bills = db.Bills.Include(b => b.apartment).Where(a => a.apartment_id == ApartmentID && a.BillName == BillName && a.date == date).SingleOrDefault();
            //Bills bills = db.Bills.Find(id);
            if (bills == null)
            {
                return HttpNotFound();
            }
            ViewBag.apartment_id = new SelectList(db.Apartments, "apartment_id", "apartment_id", bills.apartment_id);
            return View(bills);
        }

        // POST: Bills/Edit/5
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Owner")]
        public ActionResult Edit([Bind(Include = "date,apartment_id,BillName,bill_id,price,paid,photoURL")] Bills bills)
        {
            var apartment = db.Apartments.Where(a => a.apartment_id == bills.apartment_id).SingleOrDefault();
            bills.apartment = apartment;
            if (ModelState.IsValid)
            {
                db.Entry(bills).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index","Home");
            }
            ViewBag.apartment_id = new SelectList(db.Apartments, "apartment_id", "apartment_id", bills.apartment_id);
            return View(bills);
        }

        // GET: Bills/Delete/5
        [Authorize(Roles = "Owner")]
        public ActionResult Delete(DateTime id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bills bills = db.Bills.Find(id);
            if (bills == null)
            {
                return HttpNotFound();
            }
            return View(bills);
        }

        // POST: Bills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Owner")]
        public ActionResult DeleteConfirmed(DateTime id)
        {
            Bills bills = db.Bills.Find(id);
            db.Bills.Remove(bills);
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
