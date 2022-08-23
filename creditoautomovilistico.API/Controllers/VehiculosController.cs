using AutoMapper;
using creditoautomovilistico.API.Models;
using creditoautomovilistico.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace creditoautomovilistico.API.Controllers
{
    [ApiVersion("1")]
    [Route("[controller]")]
    public class VehiculosController : CreditoAutomovilisticoControllerBase<IVehiculoService, ILogger<VehiculosController>>
    {
        public VehiculosController(IVehiculoService service, ILogger<VehiculosController> iLogger, IMapper mapper)
            : base(service, iLogger, mapper)
        {
        }

        #region GET METHODS

        /// <summary>
        /// Get Vehiculo by placa
        /// </summary>
        /// <param name="placa"># of placa</param>  
        [HttpGet]
        [Route("{placa}")]
        public IActionResult GetVehiculo(string placa)
        {
            return Ok(Mapper.Map<VehiculoResponseModel>(
                Service.GetVehiculo(placa).Result)
                );
        }

        /// <summary>
        /// Get Vehiculo by multiple fields search
        /// </summary>
        /// <param name="payload">search parameters</param>  
        [HttpGet]
        [Route("get")]
        public IActionResult GetVehiculoByMutipleFields([FromQuery] VehiculoSearchModel payload)
        {
            try
            {
                return Ok(Mapper.Map<List<VehiculoResponseModel>>(
                    Service.GetVehiculoByMutipleFields(Mapper.Map<Domain.Models.VehiculoSearchModel>(payload)).Result)
                    );
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        #endregion GET METHODS

        #region  POST METHODS
        /// <summary>
        /// Creates a new vehicle
        /// </summary>
        /// <returns>A newly created item</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the item is null</response>   
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> PostVehiculo([FromBody] VehiculoPayloadModel payload)
        {
            try
            {
                var vehiculo = Mapper.Map<VehiculoResponseModel>(
                    await Service.PostVehiculo(Mapper.Map<Entities.Vehiculo>(payload))
                    );

                return CreatedAtAction(nameof(GetVehiculo), new { placa = vehiculo.Placa }, vehiculo);
            }
            catch (Exception ex)
            {
                if (ex.GetType().Name == "InvalidOperationException")
                    return Conflict(ex.Message);

                if (ex.GetType().Name == "ArgumentOutOfRangeException")
                    return BadRequest(ex.Message);

                return Problem(ex.Message);
            }
        }

        #endregion POST METHODS

        #region  PUT METHODS

        /// <summary>
        /// Updates vehicle data in DB
        /// </summary>
        /// <param name="payload">model with vehicle data to update</param>
        /// <returns></returns>
        [HttpPut]
        [Route("update")]
        public IActionResult PutVehiculo([FromBody] VehiculoPayloadModel payload)
        {
            try
            {
                return Ok(Mapper.Map<VehiculoResponseModel>(
                    Service.PutVehiculo(Mapper.Map<Entities.Vehiculo>(payload)).Result)
                    );
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null && ex.InnerException.GetType().Name == "InvalidOperationException")
                    return BadRequest(ex.Message);

                 return Problem(ex.Message);
            }
        }

        #endregion PUT METHODS

        /// <summary>
        /// Removes vehicle from DB
        /// </summary>
        /// <param name="placa">vehicle´s placa</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{placa}/delete")]
        public IActionResult DeleteVehiculo(string placa)
        {
            return Ok(Service.DeleteVehiculo(placa).Result);
        }
    }
}