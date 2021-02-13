using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ApartmentManagement.Models
{
    public class Bills
    {
        [Required]
        [DisplayName("Price")]
        [Range(0.00, 1000.00)]
        [DefaultValue(0)]
        public decimal price { get; set; }
        [DefaultValue(false)]
        [DisplayName("Paid")]
        public Boolean paid { get; set; } = false;
        [DisplayName("Photo URL")]
        [DefaultValue("")]
        [DataType(DataType.Text)]
        public string photoURL { get; set; }
        [Required]
        [Key]
        [Column(Order = 2)]
        [DataType(DataType.DateTime)]
        [DisplayName("Date")]
        public DateTime date { get; set; }
        [Key]
        [Column(Order = 3)]
        [DisplayName("Apartment ID")]
        public string apartment_id { get; set; }
        [Required]
        [Key]
        [Column(Order = 4)]
        [DisplayName("Bill Type")]
        [StringLength(50)]
        [DataType(DataType.Text)]
        
        public string BillName { get; set; }
        [Key]
        [Column(Order = 5)]
        [DisplayName("Bill ID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int bill_id { get; set; }
        [ForeignKey("apartment_id")]
        public Apartment apartment { get; set; }
    }
}