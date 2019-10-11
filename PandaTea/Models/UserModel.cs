/* UserModel.cs
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
    /// Model class for User data
    /// </summary>
    public partial class UserModel
    {
        public decimal UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime? DateRegistered { get; set; }
        public string Password { get; set; }
    }
}
