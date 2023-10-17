using System.ComponentModel.DataAnnotations;

namespace PhoneBook.Entities.Models
{
    public class Login
    {
        [Key]
        public int LoginID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public int IsDeleted { get; set; } = 0;
        public int IsActive { get; set; } = 1;
    }
}
