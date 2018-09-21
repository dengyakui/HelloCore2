using Microsoft.AspNetCore.Identity;
using System;

namespace IdentitySample.Data
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string CustomTag { get; set; }
    }
    
}
