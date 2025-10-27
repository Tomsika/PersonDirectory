using AutoMapper;
using PersonDirectory.API.Models;
using PersonDirectory.Application.Commands;
using PersonDirectory.Domain.Entities;

namespace PersonDirectory.API.MappingProfile
{
    public class PersonMappingProfile : Profile
    {
        public PersonMappingProfile()
        {
            CreateMap<AddPersonModel, AddPersonCommand>()
            .ForMember(dest => dest.PhoneNumbers, opt => opt.MapFrom(src => src.PhoneNumbers));

            CreateMap<AddPersonPhoneNumberModel, PhoneNumber>();
            CreateMap<UpdatePersonModel, UpdatePersonCommand>();
            CreateMap<DeletePersonModel, DeletePersonCommand>();
        }
    }
}