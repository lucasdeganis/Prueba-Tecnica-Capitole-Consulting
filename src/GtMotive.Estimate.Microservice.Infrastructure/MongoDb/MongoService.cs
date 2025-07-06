using System;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Infrastructure.MongoDb.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace GtMotive.Estimate.Microservice.Infrastructure.MongoDb
{
    public class MongoService
    {
        public MongoService(IOptions<MongoDbSettings> options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options), "MongoDbSettings no puede ser null.");
            }

            MongoClient = new MongoClient(options.Value.ConnectionString);

            RegisterBsonClasses();
        }

        public MongoClient MongoClient { get; }

        private static void RegisterBsonClasses()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(Vehicle)))
            {
                BsonClassMap.RegisterClassMap<Vehicle>(cm =>
                {
                    cm.AutoMap();

                    cm.SetIgnoreExtraElements(true); // Ignorar campos no mapeados
                });
            }
        }
    }
}
