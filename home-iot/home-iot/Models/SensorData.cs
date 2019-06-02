using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeIot.Models
{
    public class SensorData
    {
        public enum SensorDataTypes : byte
        {
            Temperature
        }

        public enum SensorIds : byte
        {
            TestTemperature = 0,
        }

        public int ID { get; set; }
        public SensorIds SensorId { get; set; }
        public SensorDataTypes SensorDataType { get; set; }
        public DateTime Timestamp { get; set; }
        public float Value { get; set; }
    }
}
