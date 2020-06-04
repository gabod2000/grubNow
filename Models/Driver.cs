using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public  class Driver
    {
        public virtual int Id { get; set; }
        public virtual string Address_Location { get; set; }
        public virtual string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual AppUser User { get; set; }
        public virtual IList<DriverWithArea> DriverWithAreas { get; set; }
    }
}
