using AutoMapper;
using BankingCreditSystem.Application.Features.IndividualCustomers.Commands.Create;
using BankingCreditSystem.Core.Repositories;
using BankingCreditSystem.Domain.Entities;

namespace BankingCreditSystem.Application.Features.IndividualCustomers.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<IndividualCustomer, IndividualCustomerResponse>();
            CreateMap<IndividualCustomer, CreatedIndividualCustomerResponse>();
            CreateMap<IndividualCustomer, UpdatedIndividualCustomerResponse>();

            CreateMap<CreateIndividualCustomerRequest, IndividualCustomer>();
            CreateMap<UpdateIndividualCustomerRequest, IndividualCustomer>();

            CreateMap<CreateIndividualCustomerCommand, IndividualCustomer>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Request.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Request.LastName))
                .ForMember(dest => dest.NationalId, opt => opt.MapFrom(src => src.Request.NationalId))
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.Request.DateOfBirth))
                .ForMember(dest => dest.MotherName, opt => opt.MapFrom(src => src.Request.MotherName))
                .ForMember(dest => dest.FatherName, opt => opt.MapFrom(src => src.Request.FatherName))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Request.PhoneNumber))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Request.Email))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Request.Address));

            CreateMap<Paginate<IndividualCustomer>, Paginate<IndividualCustomerResponse>>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

            

        }


    }
}