using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ApartmentManagement.Models
{
    public class Apartment
    {
        [Required]
        [DisplayName("Apartment Number")]
        [Range(1,1000)]
        public int apartment_number { get; set; }
        [DisplayName("Renter ID")]
        public Guid renter_id { get; set; }
        [ForeignKey("renter_id")]
        [DisplayName("Renter")]
        public Renter Renter { get; set; }       
        [DisplayName("Building ID")]
        public int building_id { get; set; }
        [ForeignKey("building_id")]
        [DisplayName("Building")]
        public Building Building { get; set; }

        [Key]
        [DisplayName("Apartment ID")]
        [StringLength(50)]
        public string apartment_id { get; set; } // building_id.ToString() + apartment_number.ToString()

        /*[ForeignKey("apartment_id")]
        public ICollection<ElectricBill> electric_bills { get; set; }
        [ForeignKey("apartment_id")]
        public ICollection<NaturalGasBill> naturalgas_bills { get; set; }
        [ForeignKey("apartment_id")]
        public ICollection<WaterBill> water_bills { get; set; }*/
    }
}