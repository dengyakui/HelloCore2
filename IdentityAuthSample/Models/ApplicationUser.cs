﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace IdentityAuthSample.Models
{
    public class ApplicationUser:IdentityUser<Guid>
    {
        public string CustomTag { get; set; }
    }
}
