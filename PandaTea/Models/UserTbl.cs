﻿using System;

namespace PandaTea.Models
{
    public partial class UserTbl
    {
        public decimal UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime? DateRegistered { get; set; }
        public string Password { get; set; }
    }
}
