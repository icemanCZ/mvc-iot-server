using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeIot.Data
{
    public class ApplicationEvent
    {
        public int ApplicationEventId { get; set; }
        public ApplicationEventType EventType { get; set; }
        public DateTime Timestamp { get; set; }
        public int SensorId { get; set; }
        public Sensor Sensor { get; set; }
        public bool Resolved { get; set; }
        public DateTime? ResolvedTimestamp { get; set; }
    }
}
