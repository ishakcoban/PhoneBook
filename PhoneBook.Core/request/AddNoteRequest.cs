using FluentValidation;

namespace PhoneBook.Core.request
{
    public class AddNoteRequest
    {
        public string Header { get; set; }
        public string Description { get; set; }

    }

    public class AddNoteValidator : AbstractValidator<AddNoteRequest>
    {
        public AddNoteValidator()
        {
            RuleFor(x => x.Header).NotEmpty();
            RuleFor(x => x.Description).NotEmpty().Length(5, 200);

        }
    }
}
