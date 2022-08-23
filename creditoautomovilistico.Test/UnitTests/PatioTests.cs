using creditoautomovilistico.API.Controllers;
using creditoautomovilistico.API.Models;
using creditoautomovilistico.Domain.Interfaces;
using creditoautomovilistico.Domain.Services;
using creditoautomovilistico.Test.Mocks;
using creditoautomovilistico.Test.UnitTests.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;
using System.Threading.Tasks;

namespace creditoautomovilistico.Test.UnitTests
{
    [TestFixture]
    internal class PatiosTests : UnitTestBase
    {
        private PatiosController _PatiosController;
        private IPatioRepository _repo;
        private IPatioService _service;
        private ILogger<PatiosController> _logger;

        public PatiosTests() : base()
        {

        }

        [SetUp]
        public void SetUp()
        {
            _logger = Substitute.For<ILogger<PatiosController>>();
            _repo = Substitute.For<IPatioRepository>();
            _service = new PatioService(_repo);
            _PatiosController = new PatiosController(_service, _logger, Mapper);
        }

        [Test]
        public void Patios_GetPatio_ByNombre_Succeed()
        {
            string idPatio = "12345678901";

            _repo.GetPatioByIdentificacion(idPatio)
                .Returns(Mapper.Map<Entities.Patio>(PatiosMocks.GetMockedPatioDbo(idPatio)));

            var result = (OkObjectResult)_PatiosController.GetPatio(idPatio);

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);

            var responseModel = (PatioResponseModel)result.Value;
            Assert.AreEqual(idPatio, responseModel.Nombre);
        }

        [Test]
        public async Task Patios_PostPatio_Succeed()
        {
            string idPatio = "12345678901";

            _repo.GetPatioByIdentificacion(Arg.Any<string>())
                .Returns((Entities.Patio)null);

            var response = PatiosMocks.GetMockedPatioDbo(idPatio);
            _repo.AddPatio(Arg.Any<Entities.Patio>())
                .Returns(Mapper.Map<Entities.Patio>(response));

            var model = PatiosMocks.GetMockedPatioPayload(idPatio);
            var result = await _PatiosController.PostPatio(model) as CreatedAtActionResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(201, result.StatusCode);

            var responseModel = (PatioResponseModel)result.Value;
            Assert.AreEqual(idPatio, responseModel.Nombre);
            Assert.AreEqual(response.Id, responseModel.Id);
        }

        [Test]
        public void Patios_PutPatio_Succeed()
        {
            string idPatio = "12345678901";

            var response = PatiosMocks.GetMockedPatioDbo(idPatio);

            _repo.EditPatio(Arg.Any<Entities.Patio>())
                .Returns(Mapper.Map<Entities.Patio>(response));

            _repo.GetPatioByIdentificacion(idPatio)
                .Returns(Mapper.Map<Entities.Patio>(response));

            var model = PatiosMocks.GetMockedPatioPayload(idPatio);

            var result = (OkObjectResult)_PatiosController.PutPatio(model);

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);

            var responseModel = (PatioResponseModel)result.Value;
            Assert.AreEqual(idPatio, responseModel.Nombre);
        }

        [Test]
        public void Patios_DeletePatio_Succeed()
        {
            string idPatio = "12345678901";

            _repo.RemovePatio(idPatio).Returns(true);

            var result = _PatiosController.DeletePatio(idPatio) as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, 200);
            Assert.AreEqual(result.Value, true);
        }

        [Test]
        public void Patios_DeletePatio_Fails_AssociatedInfo()
        {
            string idPatio = "12345678901";

            _repo.RemovePatio(idPatio).Returns(true);
            _repo.HaveAssociatedInfo(idPatio).Returns(true);

            var result = _PatiosController.DeletePatio(idPatio) as OkObjectResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatusCode, 200);
            Assert.AreEqual(result.Value, false);
        }

        [Test]
        public async Task Patios_PostPatio_Fails_DuplicatedId()
        {
            string idPatio = "12345678901";

            var model = PatiosMocks.GetMockedPatioPayload(idPatio);

            var response = PatiosMocks.GetMockedPatioDbo(idPatio);

            _repo.GetPatioByIdentificacion(Arg.Any<string>())
                .Returns(Mapper.Map<Entities.Patio>(response));

            _repo.AddPatio(Arg.Any<Entities.Patio>())
                .Returns(Mapper.Map<Entities.Patio>(response));


            var result = await _PatiosController.PostPatio(model);

            Assert.IsNotNull(result);
        }


        [Test]
        public void Patios_GetPatio_ById_Succeed()
        {
            string idPatio = "12345678901";

            _repo.GetPatioByIdentificacion(idPatio)
                .Returns(Mapper.Map<Entities.Patio>(PatiosMocks.GetMockedPatioDbo(idPatio)));

            var result = (OkObjectResult)_PatiosController.GetPatio(idPatio);

            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);

            var responseModel = (PatioResponseModel)result.Value;
            Assert.AreEqual(idPatio, responseModel.Nombre);
        }
    }
}
