using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ApartmentManagement.Models
{
    public class SampleData : DropCreateDatabaseIfModelChanges<ApartmentManagementEntities>
    {
        //DropCreateDatabaseIfModelChanges
        protected override void Seed(ApartmentManagementEntities context)
        {
            var renters = new List<Renter>
            {
                new Renter {renter_id = Guid.NewGuid(),name="Enes",email="enes25@gmail.com",password="enes2534"},
                new Renter {renter_id = Guid.NewGuid(),name="Suayb",email="suayb25@gmail.com",password="suayb2534"}
            };
            //,start_date=new DateTime(2021, 1, 1),end_date=new DateTime(2021, 12, 31)
            renters.ForEach(a => context.Renters.Add(a));
            var buildings = new List<Building>
            {
                new Building {name = "Alan Apartmani",address="Atasehir Istanbul"},

            };
            List<Owner> owners = new List<Owner>
            {
                new Owner {owner_id=Guid.NewGuid(),email="ahmet25@gmail.com", password="ahmet2534",name = "Ahmet" , buildings=buildings},

            };
            owners.ForEach(a => context.Owners.Add(a));

            var apartments = new List<Apartment>
            {
                new Apartment {apartment_id="B1AN1",apartment_number= 1,Renter=renters[0],Building=buildings[0]},
                new Apartment {apartment_id="B1AN2",apartment_number= 2,Renter=renters[1],Building=buildings[0]}

            };
            apartments.ForEach(a => context.Apartments.Add(a));
            //apartments[0].apartment_id = buildings[0].building_id.ToString() + apartments[0].apartment_number.ToString();
            //apartments[0].apartment_id = "B1" + "AN" + apartments[0].apartment_number.ToString();
            //apartments[1].apartment_id = "B1" + "AN" + apartments[1].apartment_number.ToString();



            var bills = new List<Bills>
            {
                new Bills {price=25,date=new DateTime(2021, 1, 1),apartment=apartments[0],BillName="Electric",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(2021, 2, 1),apartment=apartments[0],BillName="Electric",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(2021, 3, 1),apartment=apartments[0],BillName="Electric",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(2021, 4, 1),apartment=apartments[0],BillName="Electric",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(2021, 5, 1),apartment=apartments[0],BillName="Electric",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(2021, 6, 1),apartment=apartments[0],BillName="Electric",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(2021, 7, 1),apartment=apartments[0],BillName="Electric",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(2021, 8, 1),apartment=apartments[0],BillName="Electric",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(2021, 9, 1),apartment=apartments[0],BillName="Electric",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(2021, 10, 1),apartment=apartments[0],BillName="Electric",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(2021, 11, 1),apartment=apartments[0],BillName="Electric",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(2021, 12, 1),apartment=apartments[0],BillName="Electric",photoURL="http://placehold.it/700x400"},


                new Bills {price=25,date=new DateTime(2021, 1, 1),apartment=apartments[0],BillName="Water",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(2021, 2, 1),apartment=apartments[0],BillName="Water",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(2021, 3, 1),apartment=apartments[0],BillName="Water",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(2021, 4, 1),apartment=apartments[0],BillName="Water",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(2021, 5, 1),apartment=apartments[0],BillName="Water",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(2021, 6, 1),apartment=apartments[0],BillName="Water",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(2021, 7, 1),apartment=apartments[0],BillName="Water",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(2021, 8, 1),apartment=apartments[0],BillName="Water",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(2021, 9, 1),apartment=apartments[0],BillName="Water",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(2021, 10, 1),apartment=apartments[0],BillName="Water",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(2021, 11, 1),apartment=apartments[0],BillName="Water",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(2021, 12, 1),apartment=apartments[0],BillName="Water",photoURL="http://placehold.it/700x400"},

                new Bills {price=25,date=new DateTime(2021, 1, 1),apartment=apartments[0],BillName="Naturalgas",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(2021, 2, 1),apartment=apartments[0],BillName="NaturalGas",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(2021, 3, 1),apartment=apartments[0],BillName="NaturalGas",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(2021, 4, 1),apartment=apartments[0],BillName="NaturalGas",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(2021, 5, 1),apartment=apartments[0],BillName="NaturalGas",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(2021, 6, 1),apartment=apartments[0],BillName="NaturalGas",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(2021, 7, 1),apartment=apartments[0],BillName="NaturalGas",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(2021, 8, 1),apartment=apartments[0],BillName="NaturalGas",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(2021, 9, 1),apartment=apartments[0],BillName="NaturalGas",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(2021, 10, 1),apartment=apartments[0],BillName="NaturalGas",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(2021, 11, 1),apartment=apartments[0],BillName="NaturalGas",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(2021, 12, 1),apartment=apartments[0],BillName="NaturalGas",photoURL="http://placehold.it/700x400"},

                new Bills {price=25,date=new DateTime(2021, 1, 1),apartment=apartments[0],BillName="Rent",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(2021, 2, 1),apartment=apartments[0],BillName="Rent",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(2021, 3, 1),apartment=apartments[0],BillName="Rent",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(2021, 4, 1),apartment=apartments[0],BillName="Rent",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(2021, 5, 1),apartment=apartments[0],BillName="Rent",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(2021, 6, 1),apartment=apartments[0],BillName="Rent",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(2021, 7, 1),apartment=apartments[0],BillName="Rent",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(2021, 8, 1),apartment=apartments[0],BillName="Rent",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(2021, 9, 1),apartment=apartments[0],BillName="Rent",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(2021, 10, 1),apartment=apartments[0],BillName="Rent",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(2021, 11, 1),apartment=apartments[0],BillName="Rent",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(2021, 12, 1),apartment=apartments[0],BillName="Rent",photoURL="http://placehold.it/700x400"},

                new Bills {price=25,date=new DateTime(2021, 1, 1),apartment=apartments[1],BillName="Electric",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(2021, 2, 1),apartment=apartments[1],BillName="Electric",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(2021, 3, 1),apartment=apartments[1],BillName="Electric",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(2021, 4, 1),apartment=apartments[1],BillName="Electric",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(2021, 5, 1),apartment=apartments[1],BillName="Electric",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(2021, 6, 1),apartment=apartments[1],BillName="Electric",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(2021, 7, 1),apartment=apartments[1],BillName="Electric",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(2021, 8, 1),apartment=apartments[1],BillName="Electric",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(2021, 9, 1),apartment=apartments[1],BillName="Electric",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(2021, 10, 1),apartment=apartments[1],BillName="Electric",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(2021, 11, 1),apartment=apartments[1],BillName="Electric",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(2021, 12, 1),apartment=apartments[1],BillName="Electric",photoURL="http://placehold.it/700x400"},

                new Bills {price=25,date=new DateTime(2021, 1, 1),apartment=apartments[1],BillName="Water",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(2021, 2, 1),apartment=apartments[1],BillName="Water",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(2021, 3, 1),apartment=apartments[1],BillName="Water",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(2021, 4, 1),apartment=apartments[1],BillName="Water",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(2021, 5, 1),apartment=apartments[1],BillName="Water",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(2021, 6, 1),apartment=apartments[1],BillName="Water",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(2021, 7, 1),apartment=apartments[1],BillName="Water",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(2021, 8, 1),apartment=apartments[1],BillName="Water",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(2021, 9, 1),apartment=apartments[1],BillName="Water",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(2021, 10, 1),apartment=apartments[1],BillName="Water",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(2021, 11, 1),apartment=apartments[1],BillName="Water",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(2021, 12, 1),apartment=apartments[1],BillName="Water",photoURL="http://placehold.it/700x400"},

                new Bills {price=25,date=new DateTime(2021, 1, 1),apartment=apartments[1],BillName="NaturalGas",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(2021, 2, 1),apartment=apartments[1],BillName="NaturalGas",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(2021, 3, 1),apartment=apartments[1],BillName="NaturalGas",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(2021, 4, 1),apartment=apartments[1],BillName="NaturalGas",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(2021, 5, 1),apartment=apartments[1],BillName="NaturalGas",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(2021, 6, 1),apartment=apartments[1],BillName="NaturalGas",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(2021, 7, 1),apartment=apartments[1],BillName="NaturalGas",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(2021, 8, 1),apartment=apartments[1],BillName="NaturalGas",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(2021, 9, 1),apartment=apartments[1],BillName="NaturalGas",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(2021, 10, 1),apartment=apartments[1],BillName="NaturalGas",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(2021, 11, 1),apartment=apartments[1],BillName="NaturalGas",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(2021, 12, 1),apartment=apartments[1],BillName="NaturalGas",photoURL="http://placehold.it/700x400"},

                new Bills {price=25,date=new DateTime(2021, 1, 1),apartment=apartments[1],BillName="Rent",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(2021, 2, 1),apartment=apartments[1],BillName="Rent",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(2021, 3, 1),apartment=apartments[1],BillName="Rent",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(2021, 4, 1),apartment=apartments[1],BillName="Rent",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(2021, 5, 1),apartment=apartments[1],BillName="Rent",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(2021, 6, 1),apartment=apartments[1],BillName="Rent",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(2021, 7, 1),apartment=apartments[1],BillName="Rent",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(2021, 8, 1),apartment=apartments[1],BillName="Rent",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(2021, 9, 1),apartment=apartments[1],BillName="Rent",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(2021, 10, 1),apartment=apartments[1],BillName="Rent",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(2021, 11, 1),apartment=apartments[1],BillName="Rent",photoURL="http://placehold.it/700x400"},
                new Bills {date=new DateTime(2021, 12, 1),apartment=apartments[1],BillName="Rent",photoURL="http://placehold.it/700x400"},
            };
            bills.ForEach(a => context.Bills.Add(a));

            var complatints = new List<Complaint>
            {
                new Complaint {title="Kapi zili",text="Kapi zili calismiyor.",date=new DateTime(2021,1,15),apartment=apartments[0]},
                new Complaint {title="Merdiven",text="Basamak kayiyor.",date=new DateTime(2021,1,20),apartment=apartments[1]},
            };
            complatints.ForEach(a => context.Complaints.Add(a));
        }

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}