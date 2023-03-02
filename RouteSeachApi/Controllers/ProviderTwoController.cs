using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RouteSeachApi.Models.Errors;
using RouteSeachApi.Models.Requests;
using RouteSeachApi.Models.Responses;

namespace RouteSeachApi.Controllers {
    [ApiController, ApiVersion("1.0"), Route("provider-two/api/v{version:apiVersion}/"), Produces(SupportedMimeTypes.Json)]
    [ProducesResponseType(401)]
    [ProducesResponseType(typeof(GenericErrorModel), 500)]
    public class ProviderTwoController : Controller {
        public ProviderTwoController(IMediator dispatcher) {
            _dispatcher = dispatcher;
        }
        private readonly IMediator _dispatcher;

        [HttpGet, Route("ping")]
        [ProducesResponseType(typeof(bool), 200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Ping(CancellationToken cancellationToken) {
            var status = await _dispatcher.Send(new ProviderTwoAvailableRequest(), cancellationToken);

            if (status) {
                return Ok();
            }
            else {
                return StatusCode(500, "Service Unavailable");
            }
        }

        [HttpPost, Route("search")]
        [ProducesResponseType(typeof(ProviderTwoSearchResponse), 200)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> Index([FromBody] ProviderTwoSearchRequest request, CancellationToken cancellationToken) {
            return Ok(await _dispatcher.Send(request, cancellationToken));
        }
    }
}
