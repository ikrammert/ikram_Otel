using DAL_CQRS.Commands.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_CQRS.Commands.Request
{
    public class DeleteOtelCommandRequest : IRequest<DeleteOtelCommandResponse>
    {
        public Guid OtelId { get; set; }
    }
}
