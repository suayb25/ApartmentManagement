using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ApartmentManagement.Models
{
    public class Renter
    {
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid renter_id { get; set; }
        [Required]
        [DisplayName("Renter Name")]
        [MinLength(2),MaxLength(25)]
        public string name { get; set; }
        [Required]
        [DisplayName("Email")]
        [DataType(DataType.EmailAddress)]
        [MinLength(5),MaxLength(50)]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [MinLength(8), MaxLength(16)]
        public string password { get; set; }
        /*[Required]
        [DataType(DataType.DateTime)]
        public DateTime start_date { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime end_date { get; set; }*/

    }
}