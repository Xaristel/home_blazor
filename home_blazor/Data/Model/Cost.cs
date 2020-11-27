using System.ComponentModel.DataAnnotations;

namespace home_blazor.Data.Model
{
    public class Cost
    {
        [Key]
        public int CostId { get; set; }
        public string CostName { get; set; }
        public int CostValue { get; set; }
        public string CostDate { get; set; }
        public int UserId { get; set; }
    }
}
