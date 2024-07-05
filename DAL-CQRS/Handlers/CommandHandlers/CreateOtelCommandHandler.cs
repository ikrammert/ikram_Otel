using DAL_CQRS.Commands.Request;
using DAL_CQRS.Commands.Response;
using DAL_CQRS.Entities;
using DAL_CQRS.Entities.Events;
using DAL_CQRS.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_CQRS.Handlers.CommandHandlers
{
    public class CreateOtelCommandHandler : IRequestHandler<CreateOtelCommandRequest, CreateOtelCommandResponse>
    {
        private readonly CommandDbContext _commandDbContext;
        private readonly EventStore _eventStore;
        
        public CreateOtelCommandHandler(CommandDbContext commandDbContext, EventStore eventStore) {
            _commandDbContext = commandDbContext;
            _eventStore = eventStore;
        }
        public async Task<CreateOtelCommandResponse> Handle(CreateOtelCommandRequest request, CancellationToken cancellationToken)
        {
            var data = new Otel
            {
                OtelId = request.OtelId,
                Name = request.Name,
                OtelAdress = request.OtelAdress,
                IsActive = request.IsActive,
                OtelWebSite = request.OtelWebSite,
                Price = request.Price,
            };
            var otelEvent = new HotelEvent("CreateHotel")
            {
                OtelModel = data
            };
            await _eventStore.SaveEventAsync(otelEvent);
            _commandDbContext.iK_Otels.Add(data);
            
            if (data == null)
            {
                return new CreateOtelCommandResponse()
                {
                    IsSuccess=false
                };
            }
            _ = await _commandDbContext.SaveChangesAsync(cancellationToken);
            return new CreateOtelCommandResponse() { IsSuccess = true, OtelId = data.OtelId};
        }
    }
}
