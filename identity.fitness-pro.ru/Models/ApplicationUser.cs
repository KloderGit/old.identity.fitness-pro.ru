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
        public DateTime Birthdate { get; set; }
        public string Address { get; set; }
        public string Picture { get; set; }
    }
}

// Claims
// Messanger.Skype
// Messanger.Whatsup
// Messanger.Other

// Social.Instagram
// Social.Facebook
// Social.VContakte

// Status ???
