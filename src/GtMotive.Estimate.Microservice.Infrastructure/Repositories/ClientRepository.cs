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
    public class ClientRepository : RepositoryBase, IClientRepository
    {
        private readonly IMongoCollection<Client> _collection;

        public ClientRepository(MongoService mongoService, IOptions<MongoDbSettings> options)
        {
            ArgumentNullException.ThrowIfNull(mongoService);
            ArgumentNullException.ThrowIfNull(options);

            var database = mongoService.MongoClient.GetDatabase(options.Value.MongoDbDatabaseName);
            _collection = database.GetCollection<Client>("Clients");
        }

        public Task<List<Client>> GetAllAsync() =>
            ExecuteWithExceptionHandling(() => _collection.Find(_ => true).ToListAsync());

        public Task<Client> GetByIdAsync(Guid id) =>
            ExecuteWithExceptionHandling(() =>
            {
                return id == Guid.Empty
                    ? throw new ArgumentException("Id no puede ser nullo.")
                    : _collection.Find(v => v.Id == id).FirstOrDefaultAsync();
            });

        public Task CreateAsync(Client client) =>
            ExecuteWithExceptionHandling(() =>
            {
                ArgumentNullException.ThrowIfNull(client);
                return _collection.InsertOneAsync(client);
            });

        public Task UpdateAsync(Client client) =>
            ExecuteWithExceptionHandling(() =>
            {
                ArgumentNullException.ThrowIfNull(client);
                return _collection.ReplaceOneAsync(v => v.Id == client.Id, client);
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
