using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DAL_CQRS.Entities.Events
{
    // public class HotelCreatedEvent
    // {
    //     public Guid HotelId { get; set; }
    //     public string Name { get; set; }
    //     public string OtelAdress { get; set; }
    //     public DateTime CreatedAt { get; set; }
    // }

    // public class HotelEvent
    // {
    //     public Guid HotelId { get; set; }
    //     public string? Name { get; set; }
    //     public string? OtelAdress { get; set; }
    //     public decimal? Price { get; set; }
    //     public string? OtelWebSite { get; set; }
    //     public bool? IsActive { get; set; }
    //     public DateTime? LastModificationTime { get; set; }
    //     public DateTime OccurredAt { get; set; }
    //     public string EventType { get; set; }

    //     public HotelEvent(string eventType){
    //         EventType = eventType;
    //         OccurredAt = DateTime.Now;
    //     }
    // }

    public class HotelEvent
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public required Otel OtelModel { get; set; }
        public DateTime OccurredAt { get; set; }
        public string EventType { get; set; }
        public bool Processed { get; set; }
        public HotelEvent(string eventType){
            EventType = eventType;
            OccurredAt = DateTime.Now;
        }
    }

}
