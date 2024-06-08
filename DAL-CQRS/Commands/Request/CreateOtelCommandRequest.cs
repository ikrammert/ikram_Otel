using DAL_CQRS.Commands.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_CQRS.Commands.Request
{
    public class CreateOtelCommandRequest : IRequest<CreateOtelCommandResponse>
    {
        public Guid OtelId { get; set; }
        public required string Name { get; set; }
        public required string OtelAdress { get; set; }
        public string OtelWebSite { get; set; }
        public bool IsActive {  get; set; }
        public decimal Price { get; set; }
    }
}
