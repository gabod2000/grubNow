

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Vendor
    {
        public virtual int Id { get; set; }
        public virtual string StoreName { get; set; }
        public virtual int? CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public string Website_Url { get; set; }
        public virtual string NumberOfLocation { get; set; }
        public virtual string Address_Location { get; set; }

        public virtual IList<VendorWithArea> VendorWithAreas { get; set; }
        public virtual IList<VendorWithCuisine> VendorWithCuisines { get; set; }
        public virtual string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual AppUser User { get; set; }
        public virtual string UniqueFileName { get; set; }

    }
}
