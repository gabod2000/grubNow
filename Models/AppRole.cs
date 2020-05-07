
using Microsoft.AspNetCore.Identity;
using System;

namespace Models
{
    public class AppRole : IdentityRole
    {
        public string Discription { get; set; }
        public DateTime Created { get; set; }
    }
}
