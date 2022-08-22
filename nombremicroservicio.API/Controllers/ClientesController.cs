using AutoMapper;
using creditoautomovilistico.API.Models;
using creditoautomovilistico.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace creditoautomovilistico.API.Controllers
{
    [ApiVersion("1")]
    [Route("[controller]")]
    public class ClientesController : CreditoAutomovilisticoControllerBase<IClienteService, ILogger<ClientesController>>
    {
        public ClientesController(IClienteService service, ILogger<ClientesController> iLogger, IMapper mapper)
            : base(service, iLogger, mapper)
        {

        }

        #region GET METHODS

        /// <summary>
        /// Get Cliente by identificacion
        /// </summary>
        /// <param name="identificacion"># of identificacion</param>  
        [HttpGet]
        [Route("{identificacion}")]
        public IActionResult GetCliente(string identificacion)
        {
            return Ok(Mapper.Map<ClienteResponseModel>(
                Service.GetCliente(identificacion).Result)
                );
        }

        #endregion GET METHODS

        #region  POST METHODS
        /// <summary>
        /// Creates a new Cliente
        /// </summary>
        /// <returns>A newly created item</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>   
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> PostCliente([FromBody] ClientePayloadModel payload)
        {
            try
            {
                var cliente = Mapper.Map<ClienteResponseModel>(
                    await Service.PostCliente(Mapper.Map<Entities.Cliente>(payload))
                    );

                return CreatedAtAction(nameof(GetCliente), new { identificacion = cliente.Identificacion }, cliente);
            }
            catch (ApplicationException)
            {
                return Conflict();
            }
        }

        #endregion POST METHODS

        #region  PUT METHODS

        [HttpPut]
        [Route("update")]
        public IActionResult PutCliente([FromBody] ClientePayloadModel payload)
        {
            return Ok(Mapper.Map<ClienteResponseModel>(
                Service.PutCliente(Mapper.Map<Entities.Cliente>(payload)).Result)
                );
        }

        #endregion PUT METHODS

        [HttpDelete]
        [Route("{identificacion}/delete")]
        public IActionResult DeleteCliente(string identificacion)
        {
            return Ok(Service.DeleteCliente(identificacion).Result);
        }

        [HttpPost]
        [Route("solicitud")]
        public async Task<IActionResult> GenerateSolicitudCredito([FromBody] SolicitudCreditoPayloadModel payload)
        {
            try
            {
                var solicitud = Mapper.Map<SolicitudCreditoResponseModel>(
                    await Service.GenerateSolicitudCredito(Mapper.Map<Entities.SolicitudCredito>(payload))
                    );

                return CreatedAtAction(nameof(GetCliente), new { id = solicitud.Cliente.Identificacion }, solicitud);
            }
            catch (ApplicationException ex)
            {
                return Conflict(ex);
            }
        }
    }
}