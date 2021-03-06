﻿/* Order.cs
 * 
 * PROG3050: Programming Microsoft Enterprise Applications
 * Group 7
 * 
 * Revision History
 *          Ji Hong Ahn, 2019-09-12: Created
 *          Ji Hong Ahn, 2019-10-10: Renamed model classes
 *                                   Added documentation comments and header comments
 *                                   Removed unnecessary imports
 *          Ji Hong Ahn, 2019-11-18: Applied DisplayFormat annotation for DatePurchased
 */
using System;
using System.ComponentModel.DataAnnotations;

namespace PandaTea.Models
{
    /// <summary>
    /// Model class for Order data
    /// </summary>
    public partial class Order
    {
        public decimal OrderId { get; set; }
        public decimal UserId { get; set; }
        public decimal MenuId { get; set; }
        public decimal StoreId { get; set; }
        public int Quantity { get; set; }
        [DisplayFormat(DataFormatString = "{0:MMMM dd, yyyy}")]
        public DateTime? DatePurchased { get; set; }

        public Menu Menu { get; set; }
        public Store Store { get; set; }
        public User User { get; set; }
    }
}
