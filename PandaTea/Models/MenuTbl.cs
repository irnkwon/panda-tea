namespace PandaTea.Models
{
    public partial class MenuTbl
    {
        public decimal MenuId { get; set; }
        public decimal ProductId { get; set; }
        public string Size { get; set; }
        public decimal? Price { get; set; }
        public int? Calories { get; set; }
    }
}
