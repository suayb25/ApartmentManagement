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
    public class ApartmentsController : Controller
    {
        private ApartmentManagementEntities db = new ApartmentManagementEntities();

        // GET: Apartments
        [Authorize]
        public ActionResult Index()
        {
            var apartments = db.Apartments.Include(a => a.Building).Include(a => a.Renter);
            return View(apartments.ToList());
        }

        // GET: Apartments/Details/5
        [Authorize]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Apartment apartment = db.Apartments.Include(a => a.Building).Include(a => a.Renter).SingleOrDefault(x => x.apartment_id == id);
            if (apartment == null)
            {
                return HttpNotFound();
            }
            return View(apartment);
        }

        [Authorize]
        public ActionResult MyApartments()//For owner and renter, both can use this method
        {
            var OwnerOrRenterId = (Guid)Membership.GetUser(User.Identity.Name).ProviderUserKey;
            if (OwnerOrRenterId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var apartments = db.Apartments.Include(a => a.Building).Include(a => a.Renter).Where(x => x.Building.owner_id== OwnerOrRenterId || x.renter_id== OwnerOrRenterId).ToList();
            //if user role owner add if else statement
            //var apartments = db.Apartments.Include(a => a.Building).Include(a => a.Renter).Where(x => x.Building.owner_id == RenterId).ToList();
            var roles=Roles.GetRolesForUser(Membership.GetUser(User.Identity.Name).UserName);
            var buildings = db.Buildings.Where(x => x.owner_id == OwnerOrRenterId ).ToList();
            if (roles[0] == "Owner" && buildings.Count()==0)
            {
                ViewBag.Message = "You don't have any buildings. First, you need to have a building to create a new apartment.";
                ViewBag.Count = 0;
            }
            else
            {
                ViewBag.Count = 1;
            }

            if (roles[0] == "Renter" && apartments.Count() == 0)
            {
                ViewBag.Message1 = "You are not a renter in any building. Please contact your building owner.";
                ViewBag.Count1 = 0;
                ViewBag.ComplaintCount = 0;
            }
            else
            {
                ViewBag.Count1 = 1;
                
                foreach (var item in apartments)
                {
                    if (db.Complaints.Where(a => a.apartment_id == item.apartment_id).Any())
                    {
                        ViewBag.ComplaintCount = 1;
                        break;
                    }
                    else
                    {
                        ViewBag.ComplaintCount = 0;
                    }
                }
                
            }
            
            if (apartments == null)
            {
                return HttpNotFound();
            }
            return View(apartments);
        }

        [Authorize]
        public ActionResult ApartmentBillings(string id,int? year)
        {
            if (year == null)
            {
                year =  int.Parse(DateTime.Now.Year.ToString());
                //year = 2021;
            }
            var OwnerOrRenterId = (Guid)Membership.GetUser(User.Identity.Name).ProviderUserKey;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
             Apartment apartment = db.Apartments.Include(a => a.Building).Include(a => a.Renter).SingleOrDefault(x => x.apartment_id == id && (x.renter_id == OwnerOrRenterId 
             || x.Building.owner_id==OwnerOrRenterId));
            if (apartment == null)
            {
                return HttpNotFound();
            }
            ViewData["year"] = year;
            return View(apartment);
        }

        // GET: Apartments/Create
        [Authorize(Roles ="Owner")]
        public ActionResult Create()
        {
            var ownerId = (Guid)Membership.GetUser(User.Identity.Name).ProviderUserKey;
            //Buildings.Where(x => x.owner_id == ownerId).SingleOrDefault();
            ViewBag.building_id = new SelectList(db.Buildings.Where(x=>x.owner_id==ownerId), "building_id", "name");
            ViewBag.renter_id = new SelectList(db.Renters, "renter_id", "name");
            return View();
        }

        // POST: Apartments/Create
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Owner")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "apartment_number,renter_id,building_id")] Apartment apartment)
        {
            apartment.apartment_id = "B" + apartment.building_id.ToString() + "AN" + apartment.apartment_number.ToString();
            if (ModelState.IsValid)
            {
                db.Apartments.Add(apartment);
                db.SaveChanges();
                var bills = new List<Bills>
            {
                new Bills {date=new DateTime(DateTime.Now.Year, 1, 1),apartment=apartment,BillName="Electric",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(DateTime.Now.Year, 2, 1),apartment=apartment,BillName="Electric",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(DateTime.Now.Year, 3, 1),apartment=apartment,BillName="Electric",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(DateTime.Now.Year, 4, 1),apartment=apartment,BillName="Electric",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(DateTime.Now.Year, 5, 1),apartment=apartment,BillName="Electric",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(DateTime.Now.Year, 6, 1),apartment=apartment,BillName="Electric",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(DateTime.Now.Year, 7, 1),apartment=apartment,BillName="Electric",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(DateTime.Now.Year, 8, 1),apartment=apartment,BillName="Electric",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(DateTime.Now.Year, 9, 1),apartment=apartment,BillName="Electric",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(DateTime.Now.Year, 10, 1),apartment=apartment,BillName="Electric",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(DateTime.Now.Year, 11, 1),apartment=apartment,BillName="Electric",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(DateTime.Now.Year, 12, 1),apartment=apartment,BillName="Electric",photoURL="http://placehold.it/700x400"},


                new Bills {date=new DateTime(DateTime.Now.Year, 1, 1),apartment=apartment,BillName="Water",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(DateTime.Now.Year, 2, 1),apartment=apartment,BillName="Water",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(DateTime.Now.Year, 3, 1),apartment=apartment,BillName="Water",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(DateTime.Now.Year, 4, 1),apartment=apartment,BillName="Water",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(DateTime.Now.Year, 5, 1),apartment=apartment,BillName="Water",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(DateTime.Now.Year, 6, 1),apartment=apartment,BillName="Water",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(DateTime.Now.Year, 7, 1),apartment=apartment,BillName="Water",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(DateTime.Now.Year, 8, 1),apartment=apartment,BillName="Water",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(DateTime.Now.Year, 9, 1),apartment=apartment,BillName="Water",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(DateTime.Now.Year, 10, 1),apartment=apartment,BillName="Water",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(DateTime.Now.Year, 11, 1),apartment=apartment,BillName="Water",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(DateTime.Now.Year, 12, 1),apartment=apartment,BillName="Water",photoURL="http://placehold.it/700x400"},

                new Bills {date=new DateTime(DateTime.Now.Year, 1, 1),apartment=apartment,BillName="Naturalgas",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(DateTime.Now.Year, 2, 1),apartment=apartment,BillName="NaturalGas",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(DateTime.Now.Year, 3, 1),apartment=apartment,BillName="NaturalGas",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(DateTime.Now.Year, 4, 1),apartment=apartment,BillName="NaturalGas",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(DateTime.Now.Year, 5, 1),apartment=apartment,BillName="NaturalGas",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(DateTime.Now.Year, 6, 1),apartment=apartment,BillName="NaturalGas",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(DateTime.Now.Year, 7, 1),apartment=apartment,BillName="NaturalGas",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(DateTime.Now.Year, 8, 1),apartment=apartment,BillName="NaturalGas",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(DateTime.Now.Year, 9, 1),apartment=apartment,BillName="NaturalGas",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(DateTime.Now.Year, 10, 1),apartment=apartment,BillName="NaturalGas",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(DateTime.Now.Year, 11, 1),apartment=apartment,BillName="NaturalGas",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(DateTime.Now.Year, 12, 1),apartment=apartment,BillName="NaturalGas",photoURL="http://placehold.it/700x400"},

                new Bills {date=new DateTime(DateTime.Now.Year, 1, 1),apartment=apartment,BillName="Rent",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(DateTime.Now.Year, 2, 1),apartment=apartment,BillName="Rent",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(DateTime.Now.Year, 3, 1),apartment=apartment,BillName="Rent",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(DateTime.Now.Year, 4, 1),apartment=apartment,BillName="Rent",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(DateTime.Now.Year, 5, 1),apartment=apartment,BillName="Rent",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(DateTime.Now.Year, 6, 1),apartment=apartment,BillName="Rent",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(DateTime.Now.Year, 7, 1),apartment=apartment,BillName="Rent",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(DateTime.Now.Year, 8, 1),apartment=apartment,BillName="Rent",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(DateTime.Now.Year, 9, 1),apartment=apartment,BillName="Rent",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(DateTime.Now.Year, 10, 1),apartment=apartment,BillName="Rent",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(DateTime.Now.Year, 11, 1),apartment=apartment,BillName="Rent",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(DateTime.Now.Year, 12, 1),apartment=apartment,BillName="Rent",photoURL="http://placehold.it/700x400"},
            };
                bills.ForEach(a => db.Bills.Add(a));
                db.SaveChanges();
                return RedirectToAction("Index","Home");
            }
            var ownerId = (Guid)Membership.GetUser(User.Identity.Name).ProviderUserKey;
            ViewBag.building_id = new SelectList(db.Buildings.Where(x => x.owner_id == ownerId), "building_id", "name", apartment.building_id);
            ViewBag.renter_id = new SelectList(db.Renters, "renter_id", "name", apartment.renter_id);
            return View(apartment);
        }

        // GET: Apartments/Edit/5
        [Authorize(Roles = "Owner")]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Apartment apartment = db.Apartments.Find(id);
            if (apartment == null)
            {
                return HttpNotFound();
            }
            ViewBag.building_id = new SelectList(db.Buildings, "building_id", "name", apartment.building_id);
            ViewBag.renter_id = new SelectList(db.Renters, "renter_id", "name", apartment.renter_id);
            return View(apartment);
        }

        // POST: Apartments/Edit/5
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Owner")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "apartment_id,apartment_number,renter_id,building_id")] Apartment apartment)
        {
            apartment.apartment_id = "B" + apartment.building_id.ToString() + "AN" + apartment.apartment_number.ToString();
            if (ModelState.IsValid)
            {
                db.Entry(apartment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.building_id = new SelectList(db.Buildings, "building_id", "name", apartment.building_id);
            ViewBag.renter_id = new SelectList(db.Renters, "renter_id", "name", apartment.renter_id);
            return View(apartment);
        }

        [ChildActionOnly]
        public ActionResult RenterApartmentsList()
        {
            var RenterId = (Guid)Membership.GetUser(User.Identity.Name).ProviderUserKey;
            var buildings = db.Apartments.Include(a=>a.Building).Where(x => x.renter_id == RenterId).ToList();
            return PartialView(buildings);
        }

        // GET: Apartments/Delete/5
        [Authorize(Roles = "Owner")]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Apartment apartment = db.Apartments.Include(a=>a.Building).Include(a=>a.Renter).Where(a=>a.apartment_id==id).SingleOrDefault();
            if (apartment == null)
            {
                return HttpNotFound();
            }
            return View(apartment);
        }

        // POST: Apartments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Owner")]
        public ActionResult DeleteConfirmed(string id)
        {
            Apartment apartment = db.Apartments.Find(id);
            var complaints = db.Complaints.Where(a => a.apartment_id == id).ToList();
            complaints.ForEach(a => db.Complaints.Remove(a));
            var bills = db.Bills.Where(a => a.apartment_id == id).ToList();
            bills.ForEach(a => db.Bills.Remove(a));
            db.Apartments.Remove(apartment);
            db.SaveChanges();
            return RedirectToAction("Index","Home");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        [Authorize(Roles = "Owner")]
        public ActionResult BuildingApartments(int? id)//Looking corresponding building's apartments for owner
        {
            var OwnerOrRenterId = (Guid)Membership.GetUser(User.Identity.Name).ProviderUserKey;
            if (OwnerOrRenterId == null || id ==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var apartments = db.Apartments.Include(a => a.Building).Include(a => a.Renter).Where(x => x.Building.owner_id == OwnerOrRenterId && x.Building.building_id==id).ToList();
            var buildingName = db.Buildings.Where(a => a.building_id == id).Select(a => a.name).SingleOrDefault();
            ViewBag.Building = buildingName;
            if (apartments == null)
            {
                return HttpNotFound();
            }
            return View(apartments);
        }

        // GET: Apartments/Edit/5
        [Authorize(Roles = "Owner")]
        public ActionResult OwnerApartmentEdit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Apartment apartment = db.Apartments.Find(id);
            if (apartment == null)
            {
                return HttpNotFound();
            }
            var ownerId = (Guid)Membership.GetUser(User.Identity.Name).ProviderUserKey;
            ViewBag.building_id = new SelectList(db.Buildings.Where(a=>a.owner_id==ownerId), "building_id", "name", apartment.building_id);
            ViewBag.renter_id = new SelectList(db.Renters, "renter_id", "name", apartment.renter_id);
            return View(apartment);
        }

        // POST: Apartments/Edit/5
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Owner")]
        public ActionResult OwnerApartmentEdit([Bind(Include = "apartment_id,apartment_number,renter_id,building_id")] Apartment apartment)
        {
            apartment.apartment_id = "B" + apartment.building_id.ToString() + "AN" + apartment.apartment_number.ToString();
            if (ModelState.IsValid)
            {
                db.Entry(apartment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index","Home");
            }
            var ownerId = (Guid)Membership.GetUser(User.Identity.Name).ProviderUserKey;
            ViewBag.building_id = new SelectList(db.Buildings.Where(a => a.owner_id == ownerId), "building_id", "name", apartment.building_id);
            ViewBag.renter_id = new SelectList(db.Renters, "renter_id", "name", apartment.renter_id);
            return View(apartment);
        }
        [Authorize(Roles = "Renter")]
        public ActionResult RenterApartmentDetails(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Apartment apartment = db.Apartments.Include(a => a.Building).Include(a => a.Renter).SingleOrDefault(x => x.apartment_id == id);
            if (apartment == null)
            {
                return HttpNotFound();
            }
            return View(apartment);
        }
    }
}
