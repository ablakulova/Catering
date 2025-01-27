﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catering.DAL.Entities.Auth
{
    public class User : IdentityUser
    {
        public string DisplayName { get; set; }

        public Address Address { get; set; }
    }
}
