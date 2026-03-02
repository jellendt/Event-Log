
using Backend.Context;
using Backend.Entities;

using Microsoft.AspNetCore.Mvc;

namespace Backend.Services.EventLogService
{
    public class EventLogService([FromServices] EventContext eventContext) : IEventLogService
    {
        public async Task Add(IEnumerable<EventLog> eventLogs)
        {
            await eventContext.AddRangeAsync(eventLogs);
            await eventContext.SaveChangesAsync();
        }

        public IQueryable<EventLog> Get(DateTimeOffset timeFrom)
        {
            return eventContext.EventLogs.Where(e => e.Timestamp >= timeFrom);
        }
    }
}
