﻿using Microsoft.AspNetCore.Identity;

namespace e_commece_API_React.Entities
{
    public class User : IdentityUser<int>
    {
        public UserAddress Address { get; set; }
    }
}
