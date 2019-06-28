using HomeIot.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeIot.Models
{
    public class ApplicationEventViewModel
    {
        public int ApplicationEventId { get; set; }
        public ApplicationEventType EventType { get; set; }
        public DateTime Timestamp { get; set; }
        public int SensorId { get; set; }
        public string SensorName { get; set; }
        public Unit SensorUnits { get; set; }
        public bool Resolved { get; set; }
        public DateTime? ResolvedTimestamp { get; set; }

        public string IconPath()
        {
            switch (EventType)
            {
                case ApplicationEventType.NewSensorRegistered:
                    switch (SensorUnits)
                    {
                        case Unit.DegreeCelsius:
                            return Resolved ? "/images/temperatureSensor_registered_resolved.png" : "/images/temperatureSensor_registered.png";
                    }
                    break;
                case ApplicationEventType.SensorConnectionLost:
                    switch (SensorUnits)
                    {
                        case Unit.DegreeCelsius:
                            return Resolved ? "/images/temperatureSensor_disconnected_resolved.png" : "/images/temperatureSensor_disconnected.png";
                    }
                    break;
            }

            return string.Empty;
        }
    }
}
