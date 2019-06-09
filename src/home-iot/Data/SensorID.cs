using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeIot.Data
{
    public enum SensorID : byte
    {
        SWTestTemperature = 0,
        TestTemperature = 1,
    }

    public static class SensorIDExtensions
    {
        public static string GetName(this SensorID sid)
        {
            switch (sid)
            {
                case SensorID.SWTestTemperature:
                    return "Testovací teplota generovaná SW";
                case SensorID.TestTemperature:
                    return "Testovací teplota generovaná HW";
                default:
                    throw new NotImplementedException();
            }
        }

        public static string GetUnits(this SensorID sid)
        {
            switch (sid)
            {
                case SensorID.SWTestTemperature:
                case SensorID.TestTemperature:
                    return "°C";
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
