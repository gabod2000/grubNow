using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Area
    {
        public int Id { get; set; }
        public string AreaName { get; set; }
        public virtual IList<DriverWithArea> DriverWithAreas { get; set; }
        public virtual IList<VendorWithArea> VendorWithAreas { get; set; }
    }
}
