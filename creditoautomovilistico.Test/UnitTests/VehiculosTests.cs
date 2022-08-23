using System.Threading.Tasks;
using creditoautomovilistico.API.Controllers;
using NUnit.Framework;
using NSubstitute;
using Microsoft.Extensions.Logging;
using creditoautomovilistico.Test.Mocks;
using Microsoft.AspNetCore.Mvc;
using creditoautomovilistico.Domain.Interfaces;
using creditoautomovilistico.Domain.Services;
using creditoautomovilistico.API.Models;
using System.Collections.Generic;
using creditoautomovilistico.Test.UnitTests.Base;

namespace creditoautomovilistico.Test.UnitTests
{
    [TestFixture]
    internal class VehiculosTests: UnitTestBase
    {
        private VehiculosController _vehiculosController;
        private IVehiculoRepository _repo;
        private IVehiculoService _service;
        private ILogger<VehiculosController> _logger;

        public VehiculosTests() : base()
        {

        }

        [SetUp]
        public void SetUp()
        {
            _logger =  Substitute.For<ILogger<VehiculosController>>();
            _repo = Substitute.For<IVehiculoRepository>();
            _service = new VehiculoService(_repo);
            _vehiculosController = new VehiculosController(_service, _logger, Mapper);
        }

        [Test]
        public void Vehiculos_GetVehiculo_ByPlaca_Succeed()
        {
            string placaVehiculo = "12345678";
            
            _repo.GetVehiculoByPlaca(placaVehiculo)
                .Returns(Mapper.Map<Entities.Vehiculo>(VehiculosMocks.GetMockedVehiculoDbo(placaVehiculo)));

            var result = (OkObjectResult)_vehiculosController.GetVehiculo(placaVehiculo);

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);

            var responseModel = (VehiculoResponseModel)result.Value;
            Assert.AreEqual(placaVehiculo, responseModel.Placa);
        }

        [Test]
        public async Task Vehiculos_PostVehiculo_Succeed()
        {
            string placaVehiculo = "12345678";
            string marca = "KIA";

            var response = VehiculosMocks.GetMockedVehiculoDbo(placaVehiculo);
            var marcaResponse = VehiculosMocks.GetMockedMarca(marca);

            _repo.GetVehiculoByPlaca(Arg.Any<string>())
                .Returns((Entities.Vehiculo)null);

            _repo.AddVehiculo(Arg.Any<Entities.Vehiculo>(), Arg.Any<Entities.Marca>())
                .Returns(Mapper.Map<Entities.Vehiculo>(response));

            var model = VehiculosMocks.GetMockedVehiculoPayload(placaVehiculo);

            _repo.GetMarcaVehByName(marca)
                .Returns(Mapper.Map<Entities.Marca>(marcaResponse));

            var result = await _vehiculosController.PostVehiculo(model) as CreatedAtActionResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(201, result.StatusCode);

            var responseModel = (VehiculoResponseModel)result.Value;
            Assert.AreEqual(placaVehiculo, responseModel.Placa);
            Assert.AreEqual(response.Id, responseModel.Id);
        }

        [Test]
        public void Vehiculos_PutVehiculo_Succeed()
        {
            string placaVehiculo = "12345678";
            string marca = "KIA";

            var vehResponse = VehiculosMocks.GetMockedVehiculoDbo(placaVehiculo);
            var marcaResponse = VehiculosMocks.GetMockedMarca(marca);


            _repo.EditVehiculo(Arg.Any<Entities.Vehiculo>(), Arg.Any<Entities.Marca>())
                .Returns(Mapper.Map<Entities.Vehiculo>(vehResponse));

            _repo.GetVehiculoByPlaca(placaVehiculo)
                .Returns(Mapper.Map<Entities.Vehiculo>(vehResponse));

            _repo.GetMarcaVehByName(marca)
                .Returns(Mapper.Map<Entities.Marca>(marcaResponse));

            var model = VehiculosMocks.GetMockedVehiculoPayload(placaVehiculo);

            var result = (OkObjectResult)_vehiculosController.PutVehiculo(model);

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);

            var responseModel = (VehiculoResponseModel)result.Value;
            Assert.AreEqual(placaVehiculo, responseModel.Placa);
        }

        [Test]
        public void Vehiculos_DeleteVehiculo_Succeed()
        {
            string placa = "12345678";

            _repo.RemoveVehiculo(placa).Returns(true);

            var result = _vehiculosController.DeleteVehiculo(placa) as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, 200);
            Assert.AreEqual(result.Value, true);
        }

        [Test]
        public void Vehiculos_DeleteVehiculo_Fails_AssociatedInfo()
        {
            string placa = "12345678";

            _repo.RemoveVehiculo(placa).Returns(true);
            _repo.HaveSolicitudesAsociadas(placa).Returns(true);

            var result = _vehiculosController.DeleteVehiculo(placa) as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, 200);
            Assert.AreEqual(result.Value, false);
        }

        [Test]
        public async Task Vehiculos_PostVehiculo_Fails_DuplicatedPlaca()
        {
            string placaVehiculo = "12345678";

            var model = VehiculosMocks.GetMockedVehiculoPayload(placaVehiculo);

            var response = VehiculosMocks.GetMockedVehiculoDbo(placaVehiculo);

            _repo.GetVehiculoByPlaca(Arg.Any<string>())
                .Returns(Mapper.Map<Entities.Vehiculo>(response));

            _repo.AddVehiculo(Arg.Any<Entities.Vehiculo>(), Arg.Any<Entities.Marca>())
                .Returns(Mapper.Map<Entities.Vehiculo>(response));


            var result = await _vehiculosController.PostVehiculo(model);

            Assert.IsNotNull(result);
        }

        [Test]
        public void Vehiculos_GetVehiculo_ByMultipleFields_Succeed()
        {
            string placaVehiculo = "12345678";
            var searchFields = VehiculosMocks.GetMockedSearchModel();
            var response = new List<Infrastructure.Models.Vehiculo>() { VehiculosMocks.GetMockedVehiculoDbo(placaVehiculo) };

            _repo.GetVehiculosBySearchParams(Arg.Any<Domain.Models.VehiculoSearchModel>())
                .Returns(Mapper.Map<List<Entities.Vehiculo>>(response));

            var result = _vehiculosController.GetVehiculoByMutipleFields(searchFields) as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, 200);
            var results = (List<VehiculoResponseModel>)result.Value;

            Assert.AreEqual(results[0].Placa, placaVehiculo);
        }

    }
}
