/* Store.cs
 * 
 * PROG3050: Programming Microsoft Enterprise Applications
 * Group 7
 * 
 * Revision History
 *          Ji Hong Ahn, 2019-09-12: Created
 *          Ji Hong Ahn, 2019-10-10: Renamed model classes
 *                                   Added documentation comments and header comments
 *                                   Removed unnecessary imports
 */
using System;
using System.Collections.Generic;

namespace PandaTea.Models
{
    /// <summary>
    /// Model class for Store data
    /// </summary>
    public partial class Store
    {
        public Store()
        {
            Order = new HashSet<Order>();
        }

        public decimal StoreId { get; set; }
        public string StoreName { get; set; }
        public string Address { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }

        public ICollection<Order> Order { get; set; }
    }
}
