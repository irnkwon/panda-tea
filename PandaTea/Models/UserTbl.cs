using System;
using System.Collections.Generic;

namespace PandaTea.Models
{
    public partial class UserTbl
    {
        public UserTbl()
        {
            ReviewTbl = new HashSet<ReviewTbl>();
            TransactionTbl = new HashSet<TransactionTbl>();
        }

        public decimal UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime? DateRegistered { get; set; }

        public virtual ICollection<ReviewTbl> ReviewTbl { get; set; }
        public virtual ICollection<TransactionTbl> TransactionTbl { get; set; }
    }
}
