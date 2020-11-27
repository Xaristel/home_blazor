using System.ComponentModel.DataAnnotations;

namespace home_blazor.Data.Model
{
    public class Users
    {
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
    }
}
