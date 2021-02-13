using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ApartmentManagement.Models
{
    public class Owner
    {
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid owner_id { get; set; }
        [Required]
        [DisplayName("Email")]
        [DataType(DataType.EmailAddress)]
        [MinLength(5),MaxLength(50)]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [MinLength(8),MaxLength(16)]
        public string password { get; set; }
        [Required]
        [DisplayName("Owner Name")]
        [MinLength(2),MaxLength(25)]
        public string name { get; set; }
        [ForeignKey("owner_id")]
        public ICollection<Building> buildings { get; set; }
    }
}