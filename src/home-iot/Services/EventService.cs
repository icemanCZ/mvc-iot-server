using HomeIot.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;

namespace HomeIot.Services
{
    public class EventService : IEventService
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
            var e = new ApplicationEvent();
            e.EventType = ApplicationEventType.SensorConnectionLost;
            e.Timestamp = DateTime.Now;
            e.SensorId = sensorId;
            _context.ApplicationEvents.Add(e);
            _context.SaveChanges();
        }

        public void NotifySensorConnected(int sensorId)
        {
            _context.ApplicationEvents
                .Where(x => x.EventType == ApplicationEventType.SensorConnectionLost && x.SensorId == sensorId)
                .Update(x => new ApplicationEvent() { Resolved = true, ResolvedTimestamp = DateTime.Now });
        }

    }
}
