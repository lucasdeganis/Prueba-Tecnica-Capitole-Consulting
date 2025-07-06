using System;
using System.Threading.Tasks;
using AutoMapper;
using GtMotive.Estimate.Microservice.ApplicationCore.Configurations;
using GtMotive.Estimate.Microservice.ApplicationCore.Interfaces;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Rent;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using Moq;
using Xunit;

namespace GtMotive.Estimate.Microservice.UnitTests.ApplicationCore.Rents
{
    /// <summary>
    /// En esta clase se encuentran las pruebas unitarias del caso de uso RentVehicleUseCase.
    /// </summary>
    public class RentVehicleUseCaseTests
    {
        private readonly Mock<IVehicleRepository> _vehicleRepoMock;
        private readonly Mock<IRentRepository> _rentRepoMock;
        private readonly Mock<IClientRepository> _clientRepoMock;
        private readonly Mock<IAppLogger<RentVehicleUseCase>> _loggerMock;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="RentVehicleUseCaseTests"/> class.
        /// </summary>
        public RentVehicleUseCaseTests()
        {
            _vehicleRepoMock = new Mock<IVehicleRepository>();
            _rentRepoMock = new Mock<IRentRepository>();
            _clientRepoMock = new Mock<IClientRepository>();
            _loggerMock = new Mock<IAppLogger<RentVehicleUseCase>>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = config.CreateMapper();
        }

        /// <summary>
        /// Esta prueba verifica que el método Execute del caso de uso RentVehicleUseCase se realice correctamente.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Fact]
        public async Task ExecuteCallsStandardHandleWhenAddVehicleLessFiveYearsOld()
        {
            // Arrange
            _rentRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync([]);

            var client = new Client
            {
                Id = Guid.NewGuid(),
                FirstName = "Juan",
                LastName = "Pérez",
                DocumentNumber = "12345678",
                DocumentType = "DNI",
                Email = "juan.perez@email.com",
                PhoneNumber = "123456789",
                Address = "Calle Falsa 123",
                LicenseNumber = "LIC123",
                CreatedAt = DateTime.UtcNow
            };
            _clientRepoMock.Setup(r => r.GetByIdAsync(client.Id)).ReturnsAsync(client);

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
                PricePerHour = 10,
                Fleet = new Fleet
                {
                    Id = Guid.NewGuid(),
                    Name = "Flota Principal Fast car rent",
                    Description = "Flota principal de la empresa Fast car rent",
                    Type = "Comercial",
                    ResponsiblePerson = "Carlos López",
                    Empresa = "Fast car rent",
                    CreatedAt = DateTime.UtcNow
                }
            };
            _vehicleRepoMock.Setup(r => r.GetByIdAsync(vehicle.Id)).ReturnsAsync(vehicle);

            var useCase = new RentVehicleUseCase(_rentRepoMock.Object, _vehicleRepoMock.Object, _clientRepoMock.Object, _loggerMock.Object, _mapper);
            var outputPortMock = new Mock<IOutputPortStandard<RentVehicleUseCaseOutput>>();
            var notFoundPortMock = new Mock<IOutputPortNotFound>();
            var validationPortMock = new Mock<IOutputPortValidation>();

            // Act
            await useCase.Execute(new RentVehicleUseCaseInput { ClientId = client.Id, VehicleId = vehicle.Id, RentStartDate = DateTime.Now, RentEndDate = DateTime.Now.AddDays(3) }, outputPortMock.Object, notFoundPortMock.Object, validationPortMock.Object);

            // Assert
            outputPortMock.Verify(p => p.StandardHandle(It.Is<RentVehicleUseCaseOutput>(o => o.Success)), Times.Once);
            notFoundPortMock.Verify(p => p.NotFoundHandle(It.IsAny<string>()), Times.Never);
        }

        /// <summary>
        /// Esta prueba verifica que el método Execute del caso de uso RentVehicleUseCase no se realice correctamente por la antigüedad del vehículo.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Fact]
        public async Task ExecuteCallsStandardHandleWhenAddVehicleGreaterFiveYearsOld()
        {
            // Arrange
            _rentRepoMock.Setup(r => r.GetAllAsync()).ReturnsAsync([]);

            var client = new Client
            {
                Id = Guid.NewGuid(),
                FirstName = "Juan",
                LastName = "Pérez",
                DocumentNumber = "12345678",
                DocumentType = "DNI",
                Email = "juan.perez@email.com",
                PhoneNumber = "123456789",
                Address = "Calle Falsa 123",
                LicenseNumber = "LIC123",
                CreatedAt = DateTime.UtcNow
            };
            _clientRepoMock.Setup(r => r.GetByIdAsync(client.Id)).ReturnsAsync(client);

            var vehicle = new Vehicle
            {
                Id = Guid.NewGuid(),
                Brand = "Toyota",
                Model = "Corolla",
                Year = 2019,
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
                PricePerHour = 10,
                Fleet = new Fleet
                {
                    Id = Guid.NewGuid(),
                    Name = "Flota Principal Fast car rent",
                    Description = "Flota principal de la empresa Fast car rent",
                    Type = "Comercial",
                    ResponsiblePerson = "Carlos López",
                    Empresa = "Fast car rent",
                    CreatedAt = DateTime.UtcNow
                }
            };
            _vehicleRepoMock.Setup(r => r.GetByIdAsync(vehicle.Id)).ReturnsAsync(vehicle);

            var useCase = new RentVehicleUseCase(_rentRepoMock.Object, _vehicleRepoMock.Object, _clientRepoMock.Object, _loggerMock.Object, _mapper);
            var outputPortMock = new Mock<IOutputPortStandard<RentVehicleUseCaseOutput>>();
            var notFoundPortMock = new Mock<IOutputPortNotFound>();
            var validationPortMock = new Mock<IOutputPortValidation>();

            // Act
            await useCase.Execute(new RentVehicleUseCaseInput { ClientId = client.Id, VehicleId = vehicle.Id, RentStartDate = DateTime.Now, RentEndDate = DateTime.Now.AddDays(3) }, outputPortMock.Object, notFoundPortMock.Object, validationPortMock.Object);

            // Assert
            outputPortMock.Verify(o => o.StandardHandle(It.IsAny<RentVehicleUseCaseOutput>()), Times.Never);
            notFoundPortMock.Verify(o => o.NotFoundHandle(It.IsAny<string>()), Times.Never);
            validationPortMock.Verify(o => o.ValidationHandle(It.Is<string>(v => v == "El vehiculo no puede ser alquilado por antiguedad.")), Times.Once);
        }
    }
}
