using AutoMapper;
using creditoautomovilistico.API.Models;
using creditoautomovilistico.Entities;

namespace creditoautomovilistico.API.Mapper
{
    public class APIMappingProfile : Profile
    {
        public APIMappingProfile()
        {
            CreateMap<VehiculoPayloadModel, Vehiculo>()
                .ForMember(dest => dest.Id, o => o.Ignore())
                .DisableCtorValidation();

            CreateMap<Vehiculo, VehiculoResponseModel>()
                .DisableCtorValidation(); 

            CreateMap<VehiculoSearchModel, Domain.Models.VehiculoSearchModel>()
                .ReverseMap();

            CreateMap<ClientePayloadModel, Cliente>()
                .ForMember(dest => dest.Id, o => o.Ignore())
                .ForMember(dest => dest.Solicitudes, o => o.Ignore())
                .DisableCtorValidation();

            CreateMap<Cliente, ClienteResponseModel>()
                .DisableCtorValidation();

            CreateMap<Patio, PatioResponseModel>()
                .DisableCtorValidation();

            CreateMap<PatioPayloadModel, Patio>()
                .DisableCtorValidation()
                .ForMember(dest => dest.Id, o => o.Ignore())
                .ForMember(dest => dest.Clientes, o => o.Ignore())
                .ForMember(dest => dest.Ejecutivos, o => o.Ignore());

            CreateMap<SolicitudCreditoPayloadModel, SolicitudCredito>()
                .DisableCtorValidation()
                .ForMember(dest => dest.Id, o => o.Ignore())
                .ForMember(dest => dest.Patio, o => o.MapFrom(u=> new PatioPayloadModel { Nombre = u.Patio}))
                .ForMember(dest => dest.Cliente, o => o.MapFrom(u => new ClientePayloadModel { Identificacion = u.IdCliente }))
                .ForMember(dest => dest.Ejecutivo, o => o.MapFrom(u => new Ejecutivo { Identificacion = u.IdEjecutivo }))
                .ForMember(dest => dest.Vehiculo, o => o.MapFrom(u => new Vehiculo { Placa = u.PlacaVehiculo }));

            CreateMap<SolicitudCredito, SolicitudCreditoResponseModel>()
                .DisableCtorValidation();

            CreateMap<Ejecutivo, EjecutivoResponseModel>()
                .DisableCtorValidation();

            CreateMap<ClientePatio, ClientePatioResponseModel>()
                 .DisableCtorValidation();
        }
    }
}
