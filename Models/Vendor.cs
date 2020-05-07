

using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Vendor
    {
        public int Id { get; set; }
        public string StoreName { get; set; }
        public int? CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public string Website_Url { get; set; }
        public string NumberOfLocation { get; set; }
        public string Address_Location { get; set; }
        public int? AreaId { get; set; }
        public virtual Area Area { get; set; }
        public int? CuisineId { get; set; }
        public virtual Cuisine Cuisine { get; set; }
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual AppUser User { get; set; }

    }
}
