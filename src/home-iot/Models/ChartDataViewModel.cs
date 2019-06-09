using HomeIot.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeIot.Models
{
    public class ChartDataViewModel
    {
        public SensorID SensorID { get; set; }
        public IEnumerable<SensorDataViewModel> Data { get; set; }
    }
}
