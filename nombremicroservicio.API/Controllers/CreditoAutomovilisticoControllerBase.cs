using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace creditoautomovilistico.API.Controllers
{
    [ApiController]
    [Route("autocreditos/[controller]/")]
    public class CreditoAutomovilisticoControllerBase<ServiceType, LoggerType> : ControllerBase
    {
        public ServiceType Service { get; set; }

        public LoggerType Logger { get; set; }

        public IMapper Mapper { get; set; }

        public CreditoAutomovilisticoControllerBase(ServiceType service, LoggerType logger, IMapper mapper)
        {
            Service = service;
            Logger = logger;
            Mapper = mapper;
        }
    }
}
