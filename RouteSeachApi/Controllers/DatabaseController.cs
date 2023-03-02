using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RouteSeachApi.Interfaces.Data;

namespace RouteSeachApi.Controllers {
    [ApiController, ApiVersion("1.0"), Route("api/v{version:apiVersion}/database"), Produces(SupportedMimeTypes.Json)]
    public class DatabaseController : Controller {
        public DatabaseController(IShardMigrationManager shardMigrationManager) {
            _shardMigrationManager = shardMigrationManager;
        }
        private readonly IShardMigrationManager _shardMigrationManager;

        /// <summary>
        /// Get Shard Migrations
        /// </summary>
        /// <param name="password">Administration Password</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Returns the dictionary with all pending migrations split by the context</returns>
        /// <response code="200">Dictionary with all pending migrations split by the context</response>
        /// <response code="403">Access Denied</response>
        /// <response code="500">Unexpected server error</response>
        [HttpGet("{tenantId}/")]
        public async Task<IActionResult> IndexShard([FromHeader(Name = "Password")] string password, CancellationToken cancellationToken) {
            if (!_shardMigrationManager.IsCorrectKey(password))
                return Forbid();
            var result = await _shardMigrationManager.GetPendingMigrationsAsync(cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Apply Shard Migrations
        /// </summary>
        /// <param name="password">Administration Password</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Returns the dictionary with all pending migrations split by the context</returns>
        /// <response code="200">Dictionary with all pending migrations split by the context</response>
        /// <response code="403">Access Denied</response>
        /// <response code="500">Unexpected server error</response>
        [HttpPost("{tenantId}/")]
        public async Task<IActionResult> PostShard([FromHeader(Name = "Password")] string password, CancellationToken cancellationToken) {
            if (!_shardMigrationManager.IsCorrectKey(password))
                return Forbid();
            await _shardMigrationManager.MigrateAsync(cancellationToken);
            return await IndexShard(password, cancellationToken);
        }
    }
}
