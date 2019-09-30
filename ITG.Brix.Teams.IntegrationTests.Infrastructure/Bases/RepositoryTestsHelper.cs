
using ITG.Brix.Teams.Domain;
using ITG.Brix.Teams.Infrastructure.Constants;
using ITG.Brix.Teams.Infrastructure.DataAccess.ClassModels;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq;
using System.Security.Authentication;

namespace ITG.Brix.Teams.IntegrationTests.Infrastructure.Bases
{
    public static class RepositoryTestsHelper
    {
        private static string _connectionString = null;
        private static string _dbName = "Brix-Teams";
        private static MongoClient _client;

        public static void Init(string collectionName)
        {
            _client = GetMongoClient();
            var databaseExists = DatabaseExists(_dbName);
            if (!databaseExists)
            {
                DatabaseCreate(_dbName);
            }

            var collectionExists = CollectionExists(collectionName);
            if (collectionExists)
            {
                CollectionClear(collectionName);
            }
            else
            {
                CollectionCreate(collectionName);
            }
        }

        public static string ConnectionString
        {
            get
            {
                if (_connectionString == null)
                {
                    var config = new ConfigurationBuilder().AddJsonFile("settings.json", optional: false).Build();
                    _connectionString = config["ConnectionString"];
                }

                return _connectionString;
            }
        }

        private static MongoClient GetMongoClient()
        {
            var settings = MongoClientSettings.FromUrl(new MongoUrl(ConnectionString));
            settings.SslSettings = new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
            var client = new MongoClient(settings);

            return client;
        }

        private static bool DatabaseExists(string databaseName)
        {
            var dbList = _client.ListDatabases().ToList().Select(db => db.GetValue("name").AsString);
            return dbList.Contains(databaseName);
        }

        private static void DatabaseCreate(string databaseName)
        {
            _client.GetDatabase(databaseName);
        }

        private static bool CollectionExists(string collectionName)
        {
            var database = _client.GetDatabase(_dbName);
            var filter = new BsonDocument("name", collectionName);
            var collections = database.ListCollections(new ListCollectionsOptions { Filter = filter });
            return collections.Any();
        }

        private static void CollectionCreate(string collectionName)
        {
            var database = _client.GetDatabase(_dbName);

            switch (collectionName)
            {
                case Consts.Collections.Teams:
                    database.GetCollection<TeamClass>(collectionName).Indexes.CreateOneAsync(Builders<TeamClass>.IndexKeys.Ascending(_ => _.Name), new CreateIndexOptions() { Unique = true });
                    break;
                case Consts.Collections.Operators:
                    database.GetCollection<Operator>(collectionName).Indexes.CreateOneAsync(Builders<Operator>.IndexKeys.Ascending(_ => _.Login), new CreateIndexOptions() { Unique = true });
                    break;

            }

        }

        private static void CollectionClear(string collectionName)
        {
            var database = _client.GetDatabase(_dbName);
            var collection = database.GetCollection<BsonDocument>(collectionName);
            var filter = Builders<BsonDocument>.Filter.Ne("Id", "0");
            collection.DeleteMany(filter);
        }
    }
}
