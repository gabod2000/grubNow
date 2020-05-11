using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    public  class VendorWithArea
    {
        public int Id { get; set; }
        public int? AreaId { get; set; }

        [ForeignKey("AreaId")]
        public virtual Area Area { get; set; }

        public int? VendorId { get; set; }
        [ForeignKey("VendorId")]
        public virtual Vendor Vendor { get; set; }

       
    }
}
