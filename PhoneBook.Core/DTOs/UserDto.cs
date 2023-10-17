namespace PhoneBook.Core.DTOs
{
    public class UserDto
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }
        public int IsCloseNetwork { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public List<PhoneNumberDto> PhoneNumbers { get; set; }
        public List<NoteDto> Notes { get; set; }
    }
}