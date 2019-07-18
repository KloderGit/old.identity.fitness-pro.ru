using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace identity.fitness_pro.ru.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string ExternalGuid1C { get; set; }
        public DateTime Birthday { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
    }
}
