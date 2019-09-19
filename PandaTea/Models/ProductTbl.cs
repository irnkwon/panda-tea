using System;
using System.Collections.Generic;

namespace PandaTea.Models
{
    public partial class ProductTbl
    {
        public ProductTbl()
        {
            ReviewTbl = new HashSet<ReviewTbl>();
            TransactionTbl = new HashSet<TransactionTbl>();
        }

        public decimal ProductId { get; set; }
        public string ProductName { get; set; }
        public string Size { get; set; }
        public decimal? Price { get; set; }

        public virtual ICollection<ReviewTbl> ReviewTbl { get; set; }
        public virtual ICollection<TransactionTbl> TransactionTbl { get; set; }
    }
}
