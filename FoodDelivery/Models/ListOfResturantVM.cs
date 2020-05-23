using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodDelivery.Models
{
    public class ListOfResturantVM
    {
        public int Id { get; set; }
        public string StoreName { get; set; }
        public virtual string Category { get; set; }
        public string Website_Url { get; set; }
        public string Address_Location { get; set; }
        public string UniqueFileName { get; set; }
    }
}
