using AutoMapper;
using PhoneBook.Core.DTOs;
using PhoneBook.Core.request;
using PhoneBook.Entities.Models;

namespace PhoneBook.Services.Mappers
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {//                          result
            CreateMap<AddUserRequest, User>()
                .ForSourceMember(dest => dest.Notes, act => act.DoNotValidate())
                .ForSourceMember(dest => dest.PhoneNumbers, act => act.DoNotValidate())
                .ForMember(dest => dest.Notes, act => act.Ignore())
                .ForMember(dest => dest.PhoneNumbers, act => act.Ignore());
            CreateMap<User, UserDto>();

            //.ForMember(x => x.PhoneNumbers, opt => opt.Ignore());
            //CreateMap<User, UserDto>();
            //CreateMap<List<Note>,UserDto>();

        }
    }

    /*public static class MappingExpressionExtensions
    {
        public static IMappingExpression<TSource, TDest> IgnoreAllUnmapped<TSource, TDest>(this IMappingExpression<TSource, TDest> expression)
        {
            expression.ForAllMembers(opt => opt.Ignore());
            return expression;
        }
    }*/
}
