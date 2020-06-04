
using System.Collections.Generic;

namespace Models
{
    public class Cuisine
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }

        public virtual IList<VendorWithCuisine> VendorWithCuisines { get; set; }
    }
}
