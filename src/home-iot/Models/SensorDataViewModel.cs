using HomeIot.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HomeIot.Models
{
    public class SensorDataViewModel
    {
        public SensorID SensorID { get; set; }
        public DateTime Timestamp { get; set; }
        public float Value { get; set; }

        public SensorDataViewModel(SensorID sensorId, DateTime timestamp, float value)
        {
            SensorID = sensorId;
            Timestamp = timestamp;
            Value = value;
        }
    }
}
