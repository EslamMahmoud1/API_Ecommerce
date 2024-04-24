using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.User
{
    public class ApplicationUser : IdentityUser
    {
        public string DisplayName { get; set; }
        public Address UserAddress { get; set; }
    }
}
