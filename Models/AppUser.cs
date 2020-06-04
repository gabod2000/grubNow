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
        public virtual string FirstName { get; set; }
        [MaxLength(50)]
        public virtual string LastName { get; set; }

        public virtual string ProfilePic { get; set; }
        public virtual string DriverCar { get; set; }
        public virtual DateTime Created { get; set; }
        [NotMapped]
        public virtual int? VendorId { get; set; }

        [NotMapped]
        [ForeignKey("VendorId")]
        public virtual Vendor Vendors { get; set; }
        [NotMapped]
        public virtual int? DriverId { get; set; }

        [ForeignKey("DriverId")]
        [NotMapped]
        public virtual Driver Driver { get; set; }
    }
}
