using System;
using System.Collections.Generic;

namespace PandaTea.Models
{
    public partial class StoreTbl
    {
        public decimal StoreId { get; set; }
        public string StoreName { get; set; }
        public string Address { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
    }
}
