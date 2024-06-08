using DAL_CQRS.Commands.Request;
using DAL_CQRS.Commands.Response;
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

        public DeleteOtelCommandHandler(CommandDbContext commandDbContext)
        {
            _commandDbContext = commandDbContext;
        }
        public async Task<DeleteOtelCommandResponse> Handle(DeleteOtelCommandRequest request, CancellationToken cancellationToken)
        {
            var otel = await _commandDbContext.iK_Otels.FindAsync(request.OtelId);

            if (otel == null)
            {
                // Eğer otel bulunamadıysa uygun bir yanıt döndür
                return new DeleteOtelCommandResponse { IsSuccess = false};
            }
            _commandDbContext.iK_Otels.Remove(otel);
            await _commandDbContext.SaveChangesAsync();

            return new DeleteOtelCommandResponse { IsSuccess = true };

        }
    }
}
