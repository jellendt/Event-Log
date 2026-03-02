using Backend.DependecyInjection;
using Backend.Entities;

namespace Backend.Services.EventLogService
{
    public interface IEventLogService : IScopedDependency
    {
        Task Add(IEnumerable<EventLog> eventLogs);
        IQueryable<EventLog> Get(DateTimeOffset timeFrom);
    }
}
