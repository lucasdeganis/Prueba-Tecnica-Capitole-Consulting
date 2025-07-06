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
    public class RentRepository : RepositoryBase, IRentRepository
    {
        private readonly IMongoCollection<Rent> _collection;

        public RentRepository(MongoService mongoService, IOptions<MongoDbSettings> options)
        {
            ArgumentNullException.ThrowIfNull(mongoService);
            ArgumentNullException.ThrowIfNull(options);

            var database = mongoService.MongoClient.GetDatabase(options.Value.MongoDbDatabaseName);
            _collection = database.GetCollection<Rent>("Rents");
        }

        public Task<List<Rent>> GetAllAsync() =>
            ExecuteWithExceptionHandling(() => _collection.Find(_ => true).ToListAsync());

        public Task<Rent> GetByIdAsync(Guid id) =>
            ExecuteWithExceptionHandling(() =>
            {
                return id == Guid.Empty
                    ? throw new ArgumentException("Id no puede ser nullo.")
                    : _collection.Find(v => v.Id == id).FirstOrDefaultAsync();
            });

        public Task CreateAsync(Rent rent) =>
            ExecuteWithExceptionHandling(() =>
            {
                ArgumentNullException.ThrowIfNull(rent);
                return _collection.InsertOneAsync(rent);
            });

        public Task UpdateAsync(Rent rent) =>
            ExecuteWithExceptionHandling(() =>
            {
                ArgumentNullException.ThrowIfNull(rent);
                return _collection.ReplaceOneAsync(v => v.Id == rent.Id, rent);
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
