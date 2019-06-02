using HomeIot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeIot.Data
{
    public static class DBInitializer
    {
        public static void Initialize(DBContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.SensorData.Any())
            {
                return;   // DB has been seeded
            }

            var data = new SensorData[]
            {
            new SensorData{SensorId = SensorData.SensorIds.TestTemperature, SensorDataType = SensorData.SensorDataTypes.Temperature, Timestamp = DateTime.Now, Value = 1},
            new SensorData{SensorId = SensorData.SensorIds.TestTemperature, SensorDataType = SensorData.SensorDataTypes.Temperature, Timestamp = DateTime.Now.AddSeconds(1), Value = 2},
            new SensorData{SensorId = SensorData.SensorIds.TestTemperature, SensorDataType = SensorData.SensorDataTypes.Temperature, Timestamp = DateTime.Now.AddSeconds(2), Value = 3},
            new SensorData{SensorId = SensorData.SensorIds.TestTemperature, SensorDataType = SensorData.SensorDataTypes.Temperature, Timestamp = DateTime.Now.AddSeconds(3), Value = 4},
            };
            foreach (var s in data)
            {
                context.SensorData.Add(s);
            }
            context.SaveChanges();
        }
    }
}
