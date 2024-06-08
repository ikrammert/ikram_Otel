using DAL_CQRS.Entities.Mongo;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_CQRS.Repositories
{
    public class QueryDbContext
    {
        private readonly IMongoDatabase _queriyDbContext;
        public QueryDbContext(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            _queriyDbContext = client.GetDatabase(databaseName);
        }

        public IMongoCollection<OtelMongo> Otels => _queriyDbContext.GetCollection<OtelMongo>("Otels_iK");
    }
}
