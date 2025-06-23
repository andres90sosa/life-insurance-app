using AutoMapper;
using LifeInsurance.API.DTOs;
using LifeInsurance.Application.Persons.Commands;
using LifeInsurance.Domain.Entities;

namespace LifeInsurance.API.Mappings
{
    public class PersonMappingProfile : Profile
    {
        public PersonMappingProfile()
        {
            CreateMap<CreatePersonDto, CreatePersonCommand>();
            CreateMap<UpdatePersonDto, UpdatePersonCommand>();
            CreateMap<Person, PersonDto>();
        }
    }
}
