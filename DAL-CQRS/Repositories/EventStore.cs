using DAL_CQRS.Entities.Events;
using MongoDB.Driver;

namespace DAL_CQRS.Repositories
{
    public class EventStore
    {
        // private readonly IMongoCollection<HotelCreatedEvent> _createdEvents;
        private readonly IMongoDatabase _eventStore;


        public EventStore(string connectionString, string databaseName)
        {
            // _createdEvents = database.GetCollection<HotelCreatedEvent>("createdEvents");
            var client = new MongoClient(connectionString);
            _eventStore = client.GetDatabase(databaseName);
        }

        public IMongoCollection<HotelEvent> Events => _eventStore.GetCollection<HotelEvent>("EventStore_iK");

        public async Task SaveEventAsync(HotelEvent hotelEvent)
        {
            await Events.InsertOneAsync(hotelEvent);
        }

        public async Task<List<HotelEvent>> GetEventsAsync(Guid hotelId)
        {
            return await Events.Find(e => e.OtelModel.OtelId == hotelId).ToListAsync();
        }
    }
}
