using Backend.Enums;

namespace Backend.Models
{
    public class EventLogDto
    {
        public string Message { get; set; }
        public Severity Severity { get; set; }
    }
}
