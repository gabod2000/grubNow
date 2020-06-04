
using Microsoft.AspNetCore.Identity;
using System;

namespace Models
{
    public class AppRole : IdentityRole
    {
        public virtual string Discription { get; set; }
        public virtual DateTime Created { get; set; }
    }
}
