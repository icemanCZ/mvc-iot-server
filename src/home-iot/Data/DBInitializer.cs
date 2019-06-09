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

            if (context.SensorData.Any())
                return;   // DB has been seeded

            var val = 23f;
            var time = DateTime.Now;
            var rnd = new Random();
            for (int i = 0; i < 10000; i++)
            {
                val += (float)((rnd.NextDouble()-0.5) * 5);
                context.SensorData.Add(new SensorData() { SensorId = SensorID.SWTestTemperature, Timestamp = time.AddMinutes(i), Value = val });
            }
            context.SaveChanges();
        }
    }
}
