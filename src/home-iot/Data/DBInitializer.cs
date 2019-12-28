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

#if (!DEBUG)
  return;
#endif

            if (context.Sensors.Any())
                return;   // DB has been seeded

            var group = new SensorGroup();
            group.Name = "test g ";
            group.Description = "testovaci skupina";
            context.SensorGroups.Add(group);

            for (int i = 0; i < 3; i++)
            {

                var sensor = new Sensor();
                sensor.Name = "test" + i;
                sensor.Identificator = "test" + i;
                sensor.Description = "testovaci senzor " + i;
                sensor.Units = Unit.DegreeCelsius;
                sensor.IsFavorited = true;
                context.Sensors.Add(sensor);

                var sig = new SensorInSensorGroup();
                sig.Sensor = sensor;
                sig.SensorGroup = group;
                context.SensorInSensorGroups.Add(sig);

                var val = 23f;
                var time = DateTime.Now.AddMonths(-1);
                var rnd = new Random();
                for (int j = 0; j < 50000; j++)
                {
                    val += (float)((rnd.NextDouble() - 0.5) * 5);
                    context.SensorData.Add(new SensorData() { Sensor = sensor, Timestamp = time.AddMinutes(j), Value = val });
                }
                context.SaveChanges();
            }

        }
    }
}
