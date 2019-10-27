/* Product.cs
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
    /// Model class for Product data
    /// </summary>
    public partial class Product
    {
        public Product()
        {
            Menu = new HashSet<Menu>();
            Review = new HashSet<Review>();
        }

        public decimal ProductId { get; set; }
        public string ProductName { get; set; }
        public string ImagePath { get; set; }

        public ICollection<Menu> Menu { get; set; }
        public ICollection<Review> Review { get; set; }
    }
}
