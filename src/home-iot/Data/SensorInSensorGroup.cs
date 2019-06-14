using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeIot.Data
{
    public class SensorInSensorGroup
    {
        public int SensorInSensorGroupId { get; set; }

        public int SensorId { get; set; }
        public Sensor Sensor { get; set; }

        public int SensorGroupId { get; set; }
        public SensorGroup SensorGroup { get; set; }
    }
}
