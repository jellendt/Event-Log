using Backend.Entities;
using Backend.Extensions;
using Backend.Models;
using Backend.Services.EventLogService;

using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("event-log")]
    public class EventLogController(IEventLogService eventLogService) : Controller
    {
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] IEnumerable<EventLogDto> eventLogs)
        {
            IEnumerable<EventLog> eventLogsToAdd = eventLogs.Select(e => new EventLog
            {
                Message = e.Message,
                Severity = e.Severity,
                Timestamp = DateTimeOffset.UtcNow
            });
            await eventLogService.Add(eventLogsToAdd);
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PaginationData paginationData, [FromQuery] DateTimeOffset timeFrom)
        {
            IQueryable<EventLog> query = eventLogService.Get(timeFrom);
            return Ok(await query.ToPagedResultAsync(paginationData.Page, paginationData.PageSize));
        }
    }
}
