using AutoMapper;
using PhoneBook.Core.DTOs;
using PhoneBook.Core.request;
using PhoneBook.Entities.Models;

namespace PhoneBook.Services.Mappers
{
    public class NoteMapper : Profile
    {
        public NoteMapper()
        {
            CreateMap<Note, NoteDto>();
            /*CreateMap<AddNoteRequest, Note>()
            .ForSourceMember(x => x.Notes, opt => opt.DoNotValidate())
            .ForMember(x => x.Description, opt => opt.Ignore());
            */
        }
    }
}
