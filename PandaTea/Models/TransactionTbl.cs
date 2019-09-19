using System;
using System.Collections.Generic;

namespace PandaTea.Models
{
    public partial class TransactionTbl
    {
        public decimal TransactionId { get; set; }
        public decimal UserId { get; set; }
        public decimal ProductId { get; set; }
        public decimal StoreId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime? DatePurchased { get; set; }

        public virtual ProductTbl Product { get; set; }
        public virtual StoreTbl Store { get; set; }
        public virtual UserTbl User { get; set; }
    }
}
