using HomeIot.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeIot.Services
{
    public class EventService
    {
        private readonly DBContext _context;

        public EventService(DBContext context)
        {
            _context = context;
        }

        public void NewSensorRegistered(int sensorId)
        {
            var e = new ApplicationEvent();
            e.EventType = ApplicationEventType.NewSensorRegistered;
            e.Timestamp = DateTime.Now;
            e.SensorId = sensorId;
            _context.ApplicationEvents.Add(e);
            _context.SaveChanges();
        }

        public void SensorConnectionLost(int sensorId)
        {

        }

    }
}
