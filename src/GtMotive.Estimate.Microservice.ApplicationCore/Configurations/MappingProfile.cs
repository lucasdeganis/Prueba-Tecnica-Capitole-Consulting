using AutoMapper;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Rent;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicle;
using GtMotive.Estimate.Microservice.Domain.Dto;
using GtMotive.Estimate.Microservice.Domain.Entities;

namespace GtMotive.Estimate.Microservice.ApplicationCore.Configurations
{
    /// <summary>
    /// Esta clase define el perfil de mapeo para AutoMapper, configurando las conversiones entre entidades y DTOs.
    /// </summary>
    public class MappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MappingProfile"/> class.
        /// Este constructor inicializa el perfil de mapeo, definiendo las conversiones entre las entidades del dominio y sus respectivos DTOs.
        /// </summary>
        public MappingProfile()
        {
            CreateMap<Fleet, FleetDto>();
            CreateMap<Client, ClientDto>();
            CreateMap<Vehicle, VehicleDto>();
            CreateMap<Rent, RentDto>();

            CreateMap<VehicleDto, Vehicle>();
            CreateMap<RentDto, Rent>();
            CreateMap<ClientDto, Client>();
            CreateMap<FleetDto, Fleet>();

            CreateMap<RentVehicleUseCaseInput, Rent>()
            .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.RentStartDate))
            .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.RentEndDate));
            CreateMap<CreateVehicleUseCaseInput, Vehicle>();
        }
    }
}
