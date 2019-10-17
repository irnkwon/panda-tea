/* OrderModel.cs
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

namespace PandaTea.Models
{
    /// <summary>
    /// Model class for Order data
    /// </summary>
    public partial class OrderModel
    {
        public decimal OrderId { get; set; }
        public decimal UserId { get; set; }
        public decimal MenutId { get; set; }
        public decimal StoreId { get; set; }
        public int Quantity { get; set; }
        public DateTime? DatePurchased { get; set; }
    }
}
