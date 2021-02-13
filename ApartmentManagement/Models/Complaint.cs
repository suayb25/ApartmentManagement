using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ApartmentManagement.Models
{
    public class Complaint
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int complaint_id { get; set; }
        [Required]
        [MinLength(2), MaxLength(20)]
        [DataType(DataType.Text)]
        public string title { get; set; }
        [Required]
        [MinLength(10), MaxLength(50)]
        [DataType(DataType.Text)]
        public string text { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime date { get; set; }
        public string apartment_id { get; set; }
        [ForeignKey("apartment_id")]
        public Apartment apartment { get; set; }
    }
}