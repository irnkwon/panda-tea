/* Review.cs
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
    /// Model class for Review data
    /// </summary>
    public partial class Review
    {
        public decimal ReviewId { get; set; }
        public decimal UserId { get; set; }
        public decimal ProductId { get; set; }
        public decimal? Score { get; set; }
        public string Text { get; set; }
        public DateTime? DateReviewed { get; set; }

        public Product Product { get; set; }
        public User User { get; set; }
    }
}
