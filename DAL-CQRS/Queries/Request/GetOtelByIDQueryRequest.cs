using DAL_CQRS.Queries.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_CQRS.Queries.Request
{
    public class GetOtelByIDQueryRequest : IRequest<GetOtelByIDQueryResponse>
    {
        public string OtelId { get; set; }
    }
}
