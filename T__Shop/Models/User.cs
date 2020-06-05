using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace T__Shop.Models
{
    public class User: IdentityUser
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Year { get; set; }
    }
}
