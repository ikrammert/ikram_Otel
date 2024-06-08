using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_CQRS.Entities.Mongo
{
    public class OtelMongo
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string OtelId { get; set; }
        public string Name { get; set; }
        public required string OtelAdress { get; set; }
        public string? OtelWebSite { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public decimal Price { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? LastModificationTime { get; set; } = null;
    }
}
