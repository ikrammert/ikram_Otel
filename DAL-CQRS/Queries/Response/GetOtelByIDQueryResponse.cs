using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_CQRS.Queries.Response
{
    public class GetOtelByIDQueryResponse
    {
        public string OtelId { get; set; }
        public string Name { get; set; }
        public required string OtelAdress { get; set; }
        public string? OtelWebSite { get; set; }
        public bool IsActive { get; set; }
        public decimal Price { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? LastModificationTime { get; set; }
    }
}
