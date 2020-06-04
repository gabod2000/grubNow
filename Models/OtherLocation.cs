using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    public class OtherLocation
    {
        public virtual int Id { get; set; }
        [Required]
        public virtual string LocationName { get; set; }
        [Required]
        public virtual string LocationAddress { get; set; }
        public virtual int VendorID { get; set; }
        [ForeignKey("VendorID")]
        public virtual Vendor Vendor { get; set; }
    }
}
