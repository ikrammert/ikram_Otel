using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_CQRS.Commands.Response
{
    public class CreateOtelCommandResponse
    {
        public bool IsSuccess { get; set; }
        public Guid OtelId { get; set; }
    }
}
