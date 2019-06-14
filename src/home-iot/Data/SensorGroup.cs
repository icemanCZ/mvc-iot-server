using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeIot.Data
{
    public class SensorGroup
    {
        public int SensorGroupId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<SensorInSensorGroup> Sensors { get; set; }
    }
}
