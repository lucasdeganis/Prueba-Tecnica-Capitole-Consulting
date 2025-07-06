using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Api.Controllers;
using GtMotive.Estimate.Microservice.Api.UseCases;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicle;
using GtMotive.Estimate.Microservice.Domain.Dto;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace GtMotive.Estimate.Microservice.UnitTests.Api
{
    /// <summary>
    ///
    /// </summary>
    public class VehiclesControllerTests
    {
        private readonly Mock<IUseCaseWithPresenter<GetVehicleUseCaseInput, GetVehicleUseCaseOutput>> _useCaseMock;
        private readonly VehiclePresenter<GetVehicleUseCaseOutput> _presenter;

        /// <summary>
        /// Initializes a new instance of the <see cref="VehiclesControllerTests"/> class.
        /// Este constructor inicializa los mocks necesarios para las pruebas unitarias del controlador de vehículos.
        /// </summary>
        public VehiclesControllerTests()
        {
            _useCaseMock = new Mock<IUseCaseWithPresenter<GetVehicleUseCaseInput, GetVehicleUseCaseOutput>>();
            _presenter = new VehiclePresenter<GetVehicleUseCaseOutput>();
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Fact]
        public async Task GetVehicleReturnsOkWhenVehicleExists()
        {
            // Arrange
            var expectedResult = new OkObjectResult(new GetVehicleUseCaseOutput { Vehicle = new VehicleDto { Id = Guid.NewGuid() }, Success = true, Message = "Vehículo encontrado." });

            // Asigna el resultado directamente
            typeof(VehiclePresenter<GetVehicleUseCaseOutput>)
                .GetProperty(nameof(VehiclePresenter<GetVehicleUseCaseOutput>.ActionResult))
                .SetValue(_presenter, expectedResult);

            var controller = new VehicleController(
                Mock.Of<IUseCaseWithPresenter<GetVehiclesUseCaseInput, GetVehiclesUseCaseOutput>>(),
                _useCaseMock.Object,
                Mock.Of<IUseCaseWithPresenter<CreateVehicleUseCaseInput, CreateVehicleUseCaseOutput>>(),
                Mock.Of<VehiclePresenter<GetVehiclesUseCaseOutput>>(),
                _presenter,
                Mock.Of<VehiclePresenter<CreateVehicleUseCaseOutput>>());

            // Act
            var result = await controller.GetVehicle(Guid.NewGuid());

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
