using DAL_CQRS.Commands.Request;
using DAL_CQRS.Commands.Response;
using DAL_CQRS.Queries.Request;
using DAL_CQRS.Queries.Response;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ikramOtelWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OtelController : ControllerBase
    {
        IMediator _mediator;

        public OtelController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get(/*[FromQuery] GetAllOtelQueryRequest requestModel*/)
        {
            var requestModel=new GetAllOtelQueryRequest();
            List<GetAllOtelQueryResponse> allProducts = await _mediator.Send(requestModel);
            return Ok(allProducts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var query = new GetOtelByIDQueryRequest { OtelId = id };
            var result = await _mediator.Send(query);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateOtelCommandRequest requestModel)
        {
            CreateOtelCommandResponse response = await _mediator.Send(requestModel);
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] DeleteOtelCommandRequest requestModel)
        {
            DeleteOtelCommandResponse response = await _mediator.Send(requestModel);
            return Ok(response);
        }
    }
}
