
using System.Collections.Generic;

namespace Models
{
    public class Cuisine
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual IList<VendorWithCuisine> VendorWithCuisines { get; set; }
    }
}
