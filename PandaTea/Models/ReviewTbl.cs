using System;
using System.Collections.Generic;

namespace PandaTea.Models
{
    public partial class ReviewTbl
    {
        public decimal ReviewId { get; set; }
        public decimal UserId { get; set; }
        public decimal ProductId { get; set; }
        public decimal? Score { get; set; }
        public string Text { get; set; }
        public DateTime? DateReviewed { get; set; }

        public virtual ProductTbl Product { get; set; }
        public virtual UserTbl User { get; set; }
    }
}
