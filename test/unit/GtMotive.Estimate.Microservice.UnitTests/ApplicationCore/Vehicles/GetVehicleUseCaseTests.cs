using System;
using System.Threading.Tasks;
using AutoMapper;
using GtMotive.Estimate.Microservice.ApplicationCore.Configurations;
using GtMotive.Estimate.Microservice.ApplicationCore.Interfaces;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicle;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using Moq;
using Xunit;

namespace GtMotive.Estimate.Microservice.UnitTests.ApplicationCore.Vehicles
{
    /// <summary>
    ///
    /// </summary>
    public class GetVehicleUseCaseTests
    {
        private readonly Mock<IVehicleRepository> _vehicleRepoMock;
        private readonly Mock<IAppLogger<GetVehicleUseCase>> _loggerMock;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetVehicleUseCaseTests"/> class.
        /// </summary>
        public GetVehicleUseCaseTests()
        {
            _vehicleRepoMock = new Mock<IVehicleRepository>();
            _loggerMock = new Mock<IAppLogger<GetVehicleUseCase>>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = config.CreateMapper();
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Fact]
        public async Task ExecuteCallsStandardHandleWhenVehicleExists()
        {
            // Arrange
            var vehicle = new Vehicle
            {
                Id = Guid.NewGuid(),
                Brand = "Toyota",
                Model = "Corolla",
                Year = 2025,
                Status = Domain.Enumerators.VehicleStatus.Available,
                VIN = "1234567890",
                LicensePlate = "ABC123",
                Color = "Blanco",
                Type = Domain.Enumerators.VehicleType.Sedan,
                Kilometer = 10000,
                Transmission = Domain.Enumerators.VehicleTransmission.Automatic,
                Seats = 5,
                FuelType = Domain.Enumerators.VehicleFuelType.Electric,
                Doors = 4,
                Power = 120,
                PricePerHour = 10
            };
            _vehicleRepoMock.Setup(r => r.GetByIdAsync(vehicle.Id)).ReturnsAsync(vehicle);

            var useCase = new GetVehicleUseCase(_vehicleRepoMock.Object, _loggerMock.Object, _mapper);
            var outputPortMock = new Mock<IOutputPortStandard<GetVehicleUseCaseOutput>>();
            var notFoundPortMock = new Mock<IOutputPortNotFound>();
            var validationPortMock = new Mock<IOutputPortValidation>();

            // Act
            await useCase.Execute(new GetVehicleUseCaseInput { Id = vehicle.Id }, outputPortMock.Object, notFoundPortMock.Object, validationPortMock.Object);

            // Assert
            outputPortMock.Verify(p => p.StandardHandle(It.Is<GetVehicleUseCaseOutput>(o => o.Vehicle.Id == vehicle.Id)), Times.Once);
            notFoundPortMock.Verify(p => p.NotFoundHandle(It.IsAny<string>()), Times.Never);
        }
    }
}
