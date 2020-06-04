using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    public  class VendorWithArea
    {
        public virtual int Id { get; set; }
        public virtual int? AreaId { get; set; }

        [ForeignKey("AreaId")]
        public virtual Area Area { get; set; }

        public virtual int? VendorId { get; set; }
        [ForeignKey("VendorId")]
        public virtual Vendor Vendor { get; set; }

       
    }
}
