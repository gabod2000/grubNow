using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    public class VendorWithCuisine
    {
        public int Id { get; set; }
        public int? CuisineId { get; set; }
        [ForeignKey("VendorId")]
        public virtual Cuisine Cuisine { get; set; }
        public int? VendorId { get; set; }
        [ForeignKey("VendorId")]
        public virtual Vendor Vendor { get; set; }
    }
}
