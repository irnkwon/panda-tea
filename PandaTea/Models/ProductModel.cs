/* ProductModel.cs
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
namespace PandaTea.Models
{
    /// <summary>
    /// Model class for Product data
    /// </summary>
    public partial class ProductModel
    {
        public decimal ProductId { get; set; }
        public string ProductName { get; set; }
    }
}
