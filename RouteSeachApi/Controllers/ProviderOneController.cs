using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RouteSeachApi.Models.Errors;
using RouteSeachApi.Models.Requests;
using RouteSeachApi.Models.Responses;

namespace RouteSeachApi.Controllers {
    [ApiController, ApiVersion("1.0"), Route("provider-one/api/v{version:apiVersion}/"), Produces(SupportedMimeTypes.Json)]
    [ProducesResponseType(401)]
    [ProducesResponseType(typeof(GenericErrorModel), 500)]
    public class ProviderOneController : Controller {

        public ProviderOneController(IMediator dispatcher) {
            _dispatcher = dispatcher;
        }
        private readonly IMediator _dispatcher;

        [HttpGet, Route("ping")]
        [ProducesResponseType(typeof(bool), 200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Ping(CancellationToken cancellationToken) {
            var status = await _dispatcher.Send(new ProviderOneAvailableRequest(), cancellationToken);
            if (status) {
                return Ok();
            }
            else {
                return StatusCode(500, "Service Unavailable");
            }
        }

        [HttpPost, Route("search")]
        [ProducesResponseType(typeof(ProviderOneSearchResponse), 200)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> Index([FromBody] ProviderOneSearchRequest request, CancellationToken cancellationToken) {
            return Ok(await _dispatcher.Send(request, cancellationToken));
        }
    }
}
