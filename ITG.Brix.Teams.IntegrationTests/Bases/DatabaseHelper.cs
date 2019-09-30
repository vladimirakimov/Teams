using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Linq;
using System.Security.Authentication;

namespace ITG.Brix.Teams.IntegrationTests.Bases
{
    public static class DatabaseHelper
    {
        private static string _connectionString = null;
        private static string _dbName = "Brix-Teams";
        private static string _collectionName;
        private static MongoClient _client;

        public static void Init(string collectionName)
        {
            _collectionName = collectionName;
            _client = GetMongoClient();
            var databaseExists = DatabaseExists(_dbName);
            if (!databaseExists)
            {
                DatabaseCreate(_dbName);
            }

            var collectionExists = CollectionExists(_collectionName);
            if (collectionExists)
            {
                CollectionClear(_collectionName);
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

        private static void CollectionClear(string collectionName)
        {
            var database = _client.GetDatabase(_dbName);
            var collection = database.GetCollection<BsonDocument>(collectionName);
            var filter = Builders<BsonDocument>.Filter.Ne("Id", "0");
            collection.DeleteMany(filter);
        }
    }
}
