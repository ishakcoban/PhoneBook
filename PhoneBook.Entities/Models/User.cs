using System.ComponentModel.DataAnnotations;

namespace PhoneBook.Entities.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public int IsDeleted { get; set; } = 0;
        public int IsActive { get; set; } = 1;
        public ICollection<Note> Notes { get; set; }
        public ICollection<PhoneNumber> PhoneNumbers { get; set; }
    }

 
}
