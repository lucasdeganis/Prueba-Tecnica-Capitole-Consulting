using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.Interfaces;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Infrastructure.MongoDb;
using GtMotive.Estimate.Microservice.Infrastructure.MongoDb.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace GtMotive.Estimate.Microservice.Infrastructure.Repositories
{
    public class VehicleRepository : RepositoryBase, IVehicleRepository
    {
        private readonly IMongoCollection<Vehicle> _collection;

        public VehicleRepository(MongoService mongoService, IOptions<MongoDbSettings> options)
        {
            ArgumentNullException.ThrowIfNull(mongoService);
            ArgumentNullException.ThrowIfNull(options);

            var database = mongoService.MongoClient.GetDatabase(options.Value.MongoDbDatabaseName);
            _collection = database.GetCollection<Vehicle>("Vehicles");
        }

        public Task<List<Vehicle>> GetAllAsync() =>
            ExecuteWithExceptionHandling(() => _collection.Find(_ => true).ToListAsync());

        public Task<Vehicle> GetByIdAsync(Guid id) =>
            ExecuteWithExceptionHandling(() =>
            {
                return id == Guid.Empty
                    ? throw new ArgumentException("Id no puede ser nullo.")
                    : _collection.Find(v => v.Id == id).FirstOrDefaultAsync();
            });

        public Task CreateAsync(Vehicle vehicle) =>
            ExecuteWithExceptionHandling(() =>
            {
                ArgumentNullException.ThrowIfNull(vehicle);
                return _collection.InsertOneAsync(vehicle);
            });

        public Task UpdateAsync(Vehicle vehicle) =>
            ExecuteWithExceptionHandling(() =>
            {
                ArgumentNullException.ThrowIfNull(vehicle);
                return _collection.ReplaceOneAsync(v => v.Id == vehicle.Id, vehicle);
            });

        public Task DeleteAsync(Guid id)
        {
            return ExecuteWithExceptionHandling(() =>
            {
                return id == Guid.Empty
                    ? throw new ArgumentException("Id no puede ser nullo.")
                    : _collection.DeleteOneAsync(v => v.Id == id);
            });
        }

        public Task<List<Vehicle>> GetQueryAsync(Expression<Func<Vehicle, bool>> predicate) =>
            ExecuteWithExceptionHandling(() => _collection.Find(predicate).ToListAsync());
    }
}
