using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Backend.Enums;

namespace Backend.Entities
{
    public class EventLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public string Message { get; set; }
        public Severity Severity { get; set; }
    }
}
