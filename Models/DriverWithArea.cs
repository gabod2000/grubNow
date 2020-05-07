using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
        public class DriverWithArea 
        {
            public int Id { get; set; }
            public int? AreaId { get; set; }
            [ForeignKey("AreaId")]
            public virtual Area Area { get; set; }

            public int? DriverId { get; set; }
            [ForeignKey("DriverId")]
            public virtual Driver Driver { get; set; }
        }
}
