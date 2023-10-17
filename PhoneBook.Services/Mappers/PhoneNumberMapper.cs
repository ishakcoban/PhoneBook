using AutoMapper;
using PhoneBook.Core.DTOs;
using PhoneBook.Entities.Models;
namespace PhoneBook.Services.Mappers
{
    public class PhoneNumberMapper : Profile
    {
        public PhoneNumberMapper()
        {//                          result
            CreateMap<PhoneNumber, PhoneNumberDto>();



        }
    }
}
