using System;
using System.Collections.Generic;

namespace PandaTea.Models
{
    public partial class ProductTbl
    {
        public decimal ProductId { get; set; }
        public string ProductName { get; set; }
        public string Size { get; set; }
        public decimal? Price { get; set; }
    }
}
