using DAL_CQRS.Queries.Request;
using DAL_CQRS.Queries.Response;
using DAL_CQRS.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DAL_CQRS.Handlers.QueryHandlers
{
    public class GetAllOtelQueryHandler : IRequestHandler<GetAllOtelQueryRequest, List<GetAllOtelQueryResponse>>
    {
        //private readonly CommandDbContext _commandContext;
        private readonly QueryDbContext _queryDbContext;

        //public GetAllOtelQueryHandler(CommandDbContext commandContext)
        //{
        //    _commandContext = commandContext;
        //}

        public GetAllOtelQueryHandler(QueryDbContext queryDbContext)
        {
            _queryDbContext = queryDbContext;
        }
        public async Task<List<GetAllOtelQueryResponse>> Handle(GetAllOtelQueryRequest request, CancellationToken cancellationToken)
        {
            var otels = await _queryDbContext.Otels.Find(_=>true).ToListAsync(cancellationToken);

            var response = otels.Select(otel => new GetAllOtelQueryResponse
            {
                OtelId = otel.OtelId,
                Name = otel.Name,
                OtelAdress = otel.OtelAdress,
                OtelWebSite = otel.OtelWebSite ?? string.Empty,
                CreationTime = otel.CreationTime,
                IsActive = otel.IsActive,
                LastModificationTime = otel.LastModificationTime ?? DateTime.MinValue,
                Price = otel.Price
            }).ToList();

            return response;
        }
    }
}