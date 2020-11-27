using System.ComponentModel.DataAnnotations;

namespace home_blazor.Data.Model
{
    public class Income
    {
        [Key]
        public int IncomeId { get; set; }
        public string IncomeName { get; set; }
        public int IncomeValue { get; set; }
        public string IncomeDate { get; set; }
        public int UserId { get; set; }
    }
}
