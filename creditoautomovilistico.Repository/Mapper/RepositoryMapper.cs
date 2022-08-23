using AutoMapper;

namespace creditoautomovilistico.API.Mapper
{
    public class RepositoryMappingProfile : Profile
    {
        public RepositoryMappingProfile()
        {
            CreateMap<Entities.Vehiculo, Infrastructure.Models.Vehiculo>()
                .DisableCtorValidation()
                .ReverseMap();

            CreateMap<Entities.Cliente, Infrastructure.Models.Cliente>()
                .DisableCtorValidation()
                .ReverseMap();

            CreateMap<Entities.SolicitudCredito, Infrastructure.Models.SolicitudCredito>()
                .DisableCtorValidation()
                .ReverseMap();

            CreateMap<Entities.Patio, Infrastructure.Models.Patio>()
                .DisableCtorValidation()
                .ReverseMap();

            CreateMap<Entities.Ejecutivo, Infrastructure.Models.Ejecutivo>()
                .DisableCtorValidation()
                .ReverseMap();

            CreateMap<Entities.Marca, Infrastructure.Models.Marca>()
                .DisableCtorValidation()
                .ReverseMap();

            CreateMap<Entities.ClientePatio, Infrastructure.Models.ClientePatio>()
                .DisableCtorValidation()
                .ReverseMap();
        }
    }
}
