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


            if (context.Sensors.Any())
                return;   // DB has been seeded

            var sensor = new Sensor();
            sensor.Name = "test";
            sensor.Identificator = "test";
            sensor.Description = "testovaci senzor";
            sensor.Units = Unit.DegreeCelsius;
            context.Sensors.Add(sensor);

            var group = new SensorGroup();
            group.Name = "test g";
            group.Description = "testovaci skupina";
            context.SensorGroups.Add(group);

            var sig = new SensorInSensorGroup();
            sig.Sensor = sensor;
            sig.SensorGroup = group;
            context.SensorInSensorGroups.Add(sig);

            var val = 23f;
            var time = DateTime.Now;
            var rnd = new Random();
            for (int i = 0; i < 10000; i++)
            {
                val += (float)((rnd.NextDouble()-0.5) * 5);
                context.SensorData.Add(new SensorData() { Sensor = sensor, Timestamp = time.AddMinutes(i), Value = val });
            }
            context.SaveChanges();
        }
    }
}
