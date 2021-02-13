using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ApartmentManagement.Models
{
    public class Building
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int building_id { get; set; }
        [Required]
        [DisplayName("Building Name")]
        [MinLength(2),MaxLength(25)]
        public string name { get; set; }
        [Required]
        [DisplayName("Adress")]
        [MinLength(2), MaxLength(50)]
        public string address { get; set; }
        [Required]
        [DisplayName("Owner ID")]
        public Guid owner_id { get; set; }
        /*[ForeignKey("building_id")]
        public virtual ICollection<Apartment> apartments{get;set; }*/
    }
}