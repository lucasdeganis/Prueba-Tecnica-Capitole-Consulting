using System;
using System.Threading.Tasks;
using AutoMapper;
using GtMotive.Estimate.Microservice.ApplicationCore.Interfaces;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicle;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using GtMotive.Estimate.Microservice.FunctionalTests.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace GtMotive.Estimate.Microservice.FunctionalTests.UseCases
{
    public class GetVehicleUseCaseTests : FunctionalTestBase
    {
        public GetVehicleUseCaseTests(CompositionRootTestFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task ShouldReturnVehicleWhenExists()
        {
            // Arrange: Insertar un vehículo de prueba en la base de datos
            var loggerMock = new Mock<IAppLogger<GetVehicleUseCase>>();
            var testVehicle = new Vehicle
            {
                Id = Guid.NewGuid(),
                Brand = "Toyota",
                Model = "Corolla"
            };

            await Fixture.UsingRepository<IVehicleRepository>((Func<IVehicleRepository, Task>)(async repo =>
            {
                await repo.CreateAsync(testVehicle);
            }));

            var useCase = new GetVehicleUseCase(Fixture.ServiceProvider.GetRequiredService<IVehicleRepository>(), loggerMock.Object, Fixture.ServiceProvider.GetRequiredService<IMapper>());
            var input = new GetVehicleUseCaseInput { Id = testVehicle.Id };
            var outputPort = new TestOutputPort();
            var notFoundPort = new TestNotFoundPort();
            var validationPort = new TestValidationPort();

            // Act
            await useCase.Execute(input, outputPort, notFoundPort, validationPort);

            // Assert
            Assert.True(outputPort.WasCalled);
            Assert.Equal(testVehicle.Id, outputPort.Output.Vehicle.Id);
            Assert.True(outputPort.Output.Success);
            Assert.Equal("Vehículo encontrado.", outputPort.Output.Message);
        }
    }
}
