using System.ComponentModel.DataAnnotations;

namespace PhoneBook.Entities.Models
{
    public class Note
    {
        [Key]
        public int NoteID { get; set; }
        public string Description { get; set; }
        public User User { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public int IsDeleted { get; set; } = 0;
        public int IsActive { get; set; } = 1;
    }
}
