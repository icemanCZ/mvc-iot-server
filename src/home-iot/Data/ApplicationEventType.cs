using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeIot.Data
{
    public enum ApplicationEventType : byte
    {
        NewSensorRegistered = 0,
        SensorConnectionLost = 1,
    }

    public static class ApplicationEventTypeExtensions
    {
        public static string GetText(this ApplicationEventType t)
        {
            switch (t)
            {
                case ApplicationEventType.NewSensorRegistered:
                    return "Připojil se nový senzor";
                case ApplicationEventType.SensorConnectionLost:
                    return "Žádná data od senzoru za více než 10 minut";
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
