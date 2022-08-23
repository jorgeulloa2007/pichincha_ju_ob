using creditoautomovilistico.API.Controllers;
using creditoautomovilistico.API.Models;
using creditoautomovilistico.Domain.Interfaces;
using creditoautomovilistico.Domain.Services;
using creditoautomovilistico.Entities;
using creditoautomovilistico.Test.Mocks;
using creditoautomovilistico.Test.UnitTests.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using nombremicroservicio.Domain.Interfaces;
using NSubstitute;
using NUnit.Framework;
using System.Threading.Tasks;

namespace creditoautomovilistico.Test.UnitTests
{
    [TestFixture]
    internal class ClientesTests : UnitTestBase
    {
        private ClientesController _ClientesController;
        private IClienteRepository _repo;
        private IPatioRepository _repoPatio;
        private IVehiculoRepository _repoVeh;
        private IClienteService _service;
        private ILogger<ClientesController> _logger;

        public ClientesTests() : base()
        {

        }

        [SetUp]
        public void SetUp()
        {
            _logger = Substitute.For<ILogger<ClientesController>>();
            _repo = Substitute.For<IClienteRepository>();
            _repoPatio = Substitute.For<IPatioRepository>();
            _repoVeh = Substitute.For<IVehiculoRepository>();
            _service = new ClienteService(_repo, _repoPatio, _repoVeh);
            _ClientesController = new ClientesController(_service, _logger, Mapper);
        }

        [Test]
        public void Clientes_GetCliente_ByPlaca_Succeed()
        {
            string idCliente = "12345678901";

            _repo.GetClienteByIdentificacion(idCliente)
                .Returns(Mapper.Map<Cliente>(ClientesMocks.GetMockedClienteDbo(idCliente)));

            var result = (OkObjectResult)_ClientesController.GetCliente(idCliente);

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);

            var responseModel = (ClienteResponseModel)result.Value;
            Assert.AreEqual(idCliente, responseModel.Identificacion);
        }

        [Test]
        public async Task Clientes_PostCliente_Succeed()
        {
            string idCliente = "12345678901";

            _repo.GetClienteByIdentificacion(Arg.Any<string>())
                .Returns((Entities.Cliente)null);

            var response = ClientesMocks.GetMockedClienteDbo(idCliente);
            _repo.AddCliente(Arg.Any<Entities.Cliente>())
                .Returns(Mapper.Map<Entities.Cliente>(response));

            var model = ClientesMocks.GetMockedClientePayload(idCliente);
            var result = await _ClientesController.PostCliente(model) as CreatedAtActionResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(201, result.StatusCode);

            var responseModel = (ClienteResponseModel)result.Value;
            Assert.AreEqual(idCliente, responseModel.Identificacion);
            Assert.AreEqual(response.Id, responseModel.Id);
        }

        [Test]
        public void Clientes_PutCliente_Succeed()
        {
            string idCliente = "12345678901";

            var response = ClientesMocks.GetMockedClienteDbo(idCliente);

            _repo.EditCliente(Arg.Any<Cliente>())
                .Returns(Mapper.Map<Cliente>(response));

            _repo.GetClienteByIdentificacion(idCliente)
                .Returns(Mapper.Map<Cliente>(response));

            var model = ClientesMocks.GetMockedClientePayload(idCliente);

            var result = (OkObjectResult)_ClientesController.PutCliente(model);

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);

            var responseModel = (ClienteResponseModel)result.Value;
            Assert.AreEqual(idCliente, responseModel.Identificacion);
        }

        [Test]
        public void Clientes_DeleteCliente_Succeed()
        {
            string placa = "12345678901";

            _repo.RemoveCliente(placa).Returns(true);

            var result = _ClientesController.DeleteCliente(placa) as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, 200);
            Assert.AreEqual(result.Value, true);
        }

        [Test]
        public void Clientes_DeleteCliente_Fails_AssociatedInfo()
        {
            string placa = "12345678901";

            _repo.RemoveCliente(placa).Returns(true);
            _repo.HaveSolicitudesAsociadas(placa).Returns(true);

            var result = _ClientesController.DeleteCliente(placa) as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, 200);
            Assert.AreEqual(result.Value, false);
        }

        [Test]
        public async Task Clientes_PostCliente_Fails_DuplicatedId()
        {
            string idCliente = "12345678901";

            var model = ClientesMocks.GetMockedClientePayload(idCliente);

            var response = ClientesMocks.GetMockedClienteDbo(idCliente);

            _repo.GetClienteByIdentificacion(Arg.Any<string>())
                .Returns(Mapper.Map<Entities.Cliente>(response));

            _repo.AddCliente(Arg.Any<Entities.Cliente>())
                .Returns(Mapper.Map<Entities.Cliente>(response));


            var result = await _ClientesController.PostCliente(model);

            Assert.IsNotNull(result);
        }
 
        [Test]
        public async Task Clientes_GenerarSolicitudCredito_Succeed()
        {
            string idCliente = "12345678901";
            string idPatio = "Patio1";
            string idVeh = "123456";
            string idEjecutivo = "1234567890";

            _repo.GetClienteByIdentificacion(idCliente)
                .Returns(Mapper.Map<Cliente>(ClientesMocks.GetMockedClienteDbo(idCliente)));

            _repoPatio.GetPatioByIdentificacion(Arg.Any<string>())
                .Returns(Mapper.Map<Patio>(PatiosMocks.GetMockedPatioDbo(idPatio, idEjecutivo)));

            _repoVeh.GetVehiculoByPlaca(Arg.Any<string>())
                .Returns(Mapper.Map<Vehiculo>(VehiculosMocks.GetMockedVehiculoDbo(idVeh))); ;

            var payload = ClientesMocks.GetMockedSolicitudCreditoPayload(idCliente, idEjecutivo, idPatio, idVeh);
            var response = ClientesMocks.GetMockedSolicitudCreditoResponse(idCliente, idPatio, idVeh, idEjecutivo);

            _repo.GenerarSolicitud(Arg.Any<SolicitudCredito>())
                .Returns(Mapper.Map<SolicitudCredito>(ClientesMocks.GetMockedSolicitudCreditoDbo(idCliente, idPatio, idVeh, idEjecutivo)));

            var result = await _ClientesController.GenerateSolicitudCredito(payload) as CreatedAtActionResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(201, result.StatusCode);

            var responseModel = (SolicitudCreditoResponseModel)result.Value;
            Assert.AreEqual(idCliente, responseModel.Cliente.Identificacion);
            Assert.AreEqual(response.Id, responseModel.Id);
        }
    }
}
