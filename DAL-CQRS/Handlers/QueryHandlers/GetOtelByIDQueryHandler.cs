using DAL_CQRS.Entities.Mongo;
using DAL_CQRS.Queries.Request;
using DAL_CQRS.Queries.Response;
using DAL_CQRS.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DAL_CQRS.Handlers.QueryHandlers
{
    public class GetOtelByIDQueryHandler : IRequestHandler<GetOtelByIDQueryRequest, GetOtelByIDQueryResponse>
    {
        private readonly QueryDbContext _queryDbContext;
        public GetOtelByIDQueryHandler(QueryDbContext queryDbContext)
        {
            _queryDbContext = queryDbContext;
        }
        public async Task<GetOtelByIDQueryResponse> Handle(GetOtelByIDQueryRequest request, CancellationToken cancellationToken)
        {
            var filter = Builders<OtelMongo>.Filter.Eq(o => o.OtelId, request.OtelId);
            var otel = await _queryDbContext.Otels.Find(filter).FirstOrDefaultAsync(cancellationToken);
            if (otel == null)
            {
                return null;
            }

            return new GetOtelByIDQueryResponse
            {
                OtelId = otel.OtelId,
                Name = otel.Name,
                Price = otel.Price,
                OtelAdress = otel.OtelAdress,
                CreationTime = otel.CreationTime,
                IsActive = otel.IsActive,
                LastModificationTime = otel.LastModificationTime ?? DateTime.MinValue,
                OtelWebSite = otel.OtelWebSite ?? string.Empty
            };
        }
    }
}
