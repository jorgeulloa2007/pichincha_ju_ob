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
    public class PatiosController : CreditoAutomovilisticoControllerBase<IPatioService, ILogger<PatiosController>>
    {
        public PatiosController(IPatioService service, ILogger<PatiosController> iLogger, IMapper mapper)
            : base(service, iLogger, mapper)
        {

        }

        #region GET METHODS

        /// <summary>
        /// Get Patio by identificacion
        /// </summary>
        /// <param name="identificacion"># of identificacion</param>  
        [HttpGet]
        [Route("{identificacion}")]
        public IActionResult GetPatio(string identificacion)
        {
            return Ok(Mapper.Map<PatioResponseModel>(
                Service.GetPatio(identificacion).Result)
                );
        }

        #endregion GET METHODS

        #region  POST METHODS
        /// <summary>
        /// Creates a new Patio
        /// </summary>
        /// <returns>A newly created item</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>   
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> PostPatio([FromBody] PatioPayloadModel payload)
        {
            try
            {
                var patio = Mapper.Map<PatioResponseModel>(
                    await Service.PostPatio(Mapper.Map<Entities.Patio>(payload))
                    );

                return CreatedAtAction(nameof(GetPatio), new { identificacion = patio.Nombre }, patio);
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
        public IActionResult PutPatio([FromBody] PatioPayloadModel payload)
        {
            return Ok(Mapper.Map<PatioResponseModel>(
                Service.PutPatio(Mapper.Map<Entities.Patio>(payload)).Result)
                );
        }

        #endregion PUT METHODS

        [HttpDelete]
        [Route("{identificacion}/delete")]
        public IActionResult DeletePatio(string identificacion)
        {
            return Ok(Service.DeletePatio(identificacion).Result);
        }

    }
}