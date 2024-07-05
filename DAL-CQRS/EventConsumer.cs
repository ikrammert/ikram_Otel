using DAL_CQRS.Entities;
using DAL_CQRS.Entities.Events;
using MongoDB.Driver;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using DAL_CQRS.Entities.Mongo;
using DAL_CQRS.Repositories;

namespace DAL_CQRS.EventConsumers
{
    public class EventConsumerService : BackgroundService
    {
        private readonly EventStore _eventStore;
        private readonly QueryDbContext _queryDbContext;

        public EventConsumerService(EventStore eventStore, QueryDbContext queryDbContext)
        {
            _eventStore = eventStore;
            _queryDbContext = queryDbContext;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var filter = Builders<HotelEvent>.Filter.Eq(e => e.Processed, false);
                var unprocessedEvents = await _eventStore.Events.Find(filter).ToListAsync();

                foreach (var hotelEvent in unprocessedEvents)
                {
                    await HandleEvent(hotelEvent);
                    hotelEvent.Processed = true;
                    await _eventStore.Events.ReplaceOneAsync(e => e.Id == hotelEvent.Id, hotelEvent);
                }

                await Task.Delay(1000, stoppingToken); // 1 saniye bekle, sonra tekrar kontrol et
            }
        }

        private async Task HandleEvent(HotelEvent hotelEvent)
        {
            switch (hotelEvent.EventType)
            {
                case "CreateHotel":
                    var otelForMongo = new OtelMongo
                    {
                        OtelId = hotelEvent.OtelModel.OtelId.ToString(),
                        OtelAdress = hotelEvent.OtelModel.OtelAdress,
                        CreationTime = hotelEvent.OtelModel.CreationTime,
                        IsActive = hotelEvent.OtelModel.IsActive,
                        LastModificationTime = hotelEvent.OtelModel.LastModificationTime,
                        Name = hotelEvent.OtelModel.Name,
                        OtelWebSite = hotelEvent.OtelModel.OtelWebSite,
                        Price = hotelEvent.OtelModel.Price
                    }; 
                    await _queryDbContext.Otels.InsertOneAsync(otelForMongo);
                    break;

                case "DeleteHotel":
                    var otelId = hotelEvent.OtelModel.OtelId;
                    await _queryDbContext.Otels.DeleteOneAsync(o=>o.OtelId == otelId.ToString());
                    break;

                default:
                    // Bilinmeyen bir event tipi varsa burada işlem yapabilirsiniz.
                    break;
            }
        }
    }
}
