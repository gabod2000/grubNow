using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
        public class DriverWithArea 
        {
            public virtual int Id { get; set; }
            public virtual int? AreaId { get; set; }
            [ForeignKey("AreaId")]
            public virtual Area Area { get; set; }

            public virtual int? DriverId { get; set; }
            [ForeignKey("DriverId")]
            public virtual Driver Driver { get; set; }
        }
}
