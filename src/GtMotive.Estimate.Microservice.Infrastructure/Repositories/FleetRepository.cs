using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.Interfaces;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Infrastructure.MongoDb;
using GtMotive.Estimate.Microservice.Infrastructure.MongoDb.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace GtMotive.Estimate.Microservice.Infrastructure.Repositories
{
    public class FleetRepository : RepositoryBase, IFleetRepository
    {
        private readonly IMongoCollection<Fleet> _collection;

        public FleetRepository(MongoService mongoService, IOptions<MongoDbSettings> options)
        {
            ArgumentNullException.ThrowIfNull(mongoService);
            ArgumentNullException.ThrowIfNull(options);

            var database = mongoService.MongoClient.GetDatabase(options.Value.MongoDbDatabaseName);
            _collection = database.GetCollection<Fleet>("Fleets");
        }

        public Task<List<Fleet>> GetAllAsync() =>
            ExecuteWithExceptionHandling(() => _collection.Find(_ => true).ToListAsync());

        public Task<Fleet> GetByIdAsync(Guid id) =>
            ExecuteWithExceptionHandling(() =>
            {
                return id == Guid.Empty
                    ? throw new ArgumentException("Id no puede ser nullo.")
                    : _collection.Find(v => v.Id == id).FirstOrDefaultAsync();
            });

        public Task CreateAsync(Fleet fleet) =>
            ExecuteWithExceptionHandling(() =>
            {
                ArgumentNullException.ThrowIfNull(fleet);
                return _collection.InsertOneAsync(fleet);
            });

        public Task UpdateAsync(Fleet fleet) =>
            ExecuteWithExceptionHandling(() =>
            {
                ArgumentNullException.ThrowIfNull(fleet);
                return _collection.ReplaceOneAsync(v => v.Id == fleet.Id, fleet);
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
    }
}
