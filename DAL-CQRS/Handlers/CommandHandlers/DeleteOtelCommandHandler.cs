using DAL_CQRS.Commands.Request;
using DAL_CQRS.Commands.Response;
using DAL_CQRS.Entities.Events;
using DAL_CQRS.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_CQRS.Handlers.CommandHandlers
{
    public class DeleteOtelCommandHandler : IRequestHandler<DeleteOtelCommandRequest, DeleteOtelCommandResponse>
    {
        private readonly CommandDbContext _commandDbContext;
        private readonly EventStore _eventStore;

        public DeleteOtelCommandHandler(CommandDbContext commandDbContext, EventStore eventStore)
        {
            _commandDbContext = commandDbContext;
            _eventStore = eventStore;
        }
        public async Task<DeleteOtelCommandResponse> Handle(DeleteOtelCommandRequest request, CancellationToken cancellationToken)
        {
            var otel = await _commandDbContext.iK_Otels.FindAsync(request.OtelId);
            if (otel == null)
            {
                // Eğer otel bulunamadıysa uygun bir yanıt döndür
                return new DeleteOtelCommandResponse { IsSuccess = false};
            }

            var otelEvent = new HotelEvent("DeleteHotel")
            {
                OtelModel = otel
            };
            await _eventStore.SaveEventAsync(otelEvent);

            _commandDbContext.iK_Otels.Remove(otel);
            await _commandDbContext.SaveChangesAsync();

            return new DeleteOtelCommandResponse { IsSuccess = true };

        }
    }
}
