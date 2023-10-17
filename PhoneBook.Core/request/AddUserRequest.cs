using FluentValidation;

namespace PhoneBook.Core.request
{
    public class AddUserRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }
        public int IsCloseNetwork { get; set; }
        public string Email { get; set; }
        public List<Dictionary<dynamic, dynamic>> PhoneNumbers { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public List<Dictionary<dynamic, dynamic>> Notes { get; set; }
        
    }
    public class AddUserValidator : AbstractValidator<AddUserRequest>
    {
        public AddUserValidator()
        {
            RuleFor(x => x.FirstName).Length(5, 20);
            RuleFor(x => x.LastName).Length(5, 20);
            RuleFor(x => x.FirstName).Length(5, 20);
            RuleFor(x => x.Email).EmailAddress();

        }

    }


}
