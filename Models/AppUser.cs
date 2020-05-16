using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class AppUser : IdentityUser
    {
        public AppUser()
        {
            Vendors = new Vendor();
        }

        [MaxLength(50)]
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string LastName { get; set; }

        public string ProfilePic { get; set; }
        public string DriverCar { get; set; }
        public DateTime Created { get; set; }
        [NotMapped]
        public int? VendorId { get; set; }

        [NotMapped]
        [ForeignKey("VendorId")]
        public virtual Vendor Vendors { get; set; }
        [NotMapped]
        public int? DriverId { get; set; }

        [ForeignKey("DriverId")]
        [NotMapped]
        public virtual Driver Driver { get; set; }
    }
}
