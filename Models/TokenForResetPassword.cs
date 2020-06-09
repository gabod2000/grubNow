using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class TokenForResetPassword 
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Token { get; set; }
    }
}
