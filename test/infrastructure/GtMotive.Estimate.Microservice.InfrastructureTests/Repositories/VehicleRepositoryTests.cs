using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Infrastructure.MongoDb;
using GtMotive.Estimate.Microservice.Infrastructure.MongoDb.Settings;
using GtMotive.Estimate.Microservice.Infrastructure.Repositories;
using GtMotive.Estimate.Microservice.InfrastructureTests.Infrastructure;
using Microsoft.Extensions.Options;
using Xunit;

namespace GtMotive.Estimate.Microservice.InfrastructureTests.Repositories
{
    public sealed class VehicleRepositoryTests : InfrastructureTestBase
    {
        private readonly VehicleRepository _repository;

        public VehicleRepositoryTests(GenericInfrastructureTestServerFixture fixture) : base(fixture)
        {
            // Configuración de MongoService para pruebas
            var mongoDbSettings = Options.Create(new MongoDbSettings
            {
                ConnectionString = "mongodb://admin:secret@localhost:27017/", // Ajusta según tu entorno de test
                MongoDbDatabaseName = "RentTestDb"
            });
            var mongoService = new MongoService(mongoDbSettings);

            _repository = new VehicleRepository(mongoService, mongoDbSettings);
        }

        [Fact]
        public async Task AddVehicleShouldPersistInDatabase()
        {
            // Arrange
            var vehicle = new Vehicle
            {
                // Asume que tienes estas propiedades, ajústalas según tu entidad
                Id = Guid.NewGuid(),
                Brand = "Toyota",
                Model = "Corolla"
            };

            // Act
            await _repository.CreateAsync(vehicle);

            // Assert
            var retrieved = await _repository.GetByIdAsync(vehicle.Id);
            Assert.NotNull(retrieved);
            Assert.Equal(vehicle.Brand, retrieved.Brand);
            Assert.Equal(vehicle.Model, retrieved.Model);
        }

        [Fact]
        public async Task GetVehicleByIdShouldReturnCorrectVehicle()
        {
            // Arrange
            var vehicle = new Vehicle
            {
                Id = Guid.NewGuid(),
                Brand = "Honda",
                Model = "Civic"
            };
            await _repository.CreateAsync(vehicle);

            // Act
            var retrieved = await _repository.GetByIdAsync(vehicle.Id);

            // Assert
            Assert.NotNull(retrieved);
            Assert.Equal(vehicle.Id, retrieved.Id);
            Assert.Equal(vehicle.Brand, retrieved.Brand);
            Assert.Equal(vehicle.Model, retrieved.Model);
        }

        [Fact]
        public async Task UpdateVehicleShouldModifyExistingRecord()
        {
            // Arrange
            var vehicle = new Vehicle
            {
                Id = Guid.NewGuid(),
                Brand = "Ford",
                Model = "Focus"
            };
            await _repository.CreateAsync(vehicle);

            // Act
            vehicle.Model = "Fiesta";
            await _repository.UpdateAsync(vehicle);

            // Assert
            var updatedVehicle = await _repository.GetByIdAsync(vehicle.Id);
            Assert.NotNull(updatedVehicle);
            Assert.Equal("Fiesta", updatedVehicle.Model);
        }

        [Fact]
        public async Task DeleteVehicleShouldRemoveFromDatabase()
        {
            // Arrange
            var vehicle = new Vehicle
            {
                Id = Guid.NewGuid(),
                Brand = "Chevrolet",
                Model = "Malibu"
            };
            await _repository.CreateAsync(vehicle);
            // Act
            await _repository.DeleteAsync(vehicle.Id);
            // Assert
            var deletedVehicle = await _repository.GetByIdAsync(vehicle.Id);
            Assert.Null(deletedVehicle);
        }
    }
}
