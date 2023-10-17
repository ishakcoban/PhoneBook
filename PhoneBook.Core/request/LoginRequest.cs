using FluentValidation;

namespace PhoneBook.Core.request
{
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }

    }

    public class LoginValidator : AbstractValidator<LoginRequest>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.Password).NotNull().Length(5, 20);
        }
    }
}
