using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.UnitTests.Infrastructure.Repositories;
using MongoDB.Driver;
using Moq;
using Xunit;

namespace GtMotive.Estimate.Microservice.UnitTests.Infrastructure
{
    /// <summary>
    ///
    /// </summary>
    public class VehicleRepositoryTests
    {
        /// <summary>
        ///
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Fact]
        public async Task GetByIdAsyncReturnsVehicleWhenExists()
        {
            // Arrange
            var vehicleId = Guid.NewGuid();
            var vehicle = new Vehicle { Id = vehicleId, Brand = "Toyota", Model = "Corolla" };

            var cursorMock = new Mock<IAsyncCursor<Vehicle>>();
            cursorMock.SetupSequence(c => c.MoveNextAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(true)
                .ReturnsAsync(false);
            cursorMock.SetupGet(c => c.Current).Returns(new List<Vehicle> { vehicle });

            var collectionMock = new Mock<IMongoCollection<Vehicle>>();
            collectionMock
                .Setup(c => c.FindAsync(
                    It.IsAny<FilterDefinition<Vehicle>>(),
                    It.IsAny<FindOptions<Vehicle, Vehicle>>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(cursorMock.Object);

            var repository = new VehicleRepositoryForTest(collectionMock.Object);

            // Act
            var result = await repository.GetByIdAsync(vehicleId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(vehicleId, result.Id);
        }
    }
}
