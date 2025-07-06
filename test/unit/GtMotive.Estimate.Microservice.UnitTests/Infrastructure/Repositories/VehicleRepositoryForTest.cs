using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Entities;
using MongoDB.Driver;

namespace GtMotive.Estimate.Microservice.UnitTests.Infrastructure.Repositories
{
    /// <summary>
    ///
    /// </summary>
    public class VehicleRepositoryForTest
    {
        private readonly IMongoCollection<Vehicle> _collection;

        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleRepositoryForTest"/> class.
        /// </summary>
        public VehicleRepositoryForTest(IMongoCollection<Vehicle> collection)
        {
            _collection = collection;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task<Vehicle> GetByIdAsync(Guid id)
        {
            // Simula la lógica real de tu repositorio
            var cursor = await _collection.FindAsync(v => v.Id == id);

            while (await cursor.MoveNextAsync())
            {
                var batch = cursor.Current;
                foreach (var item in batch)
                {
                    return item;
                }
            }

            return null;
        }
    }
}
