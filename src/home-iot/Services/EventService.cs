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

        }

        public void SensorConnectionLost(int sensorId)
        {

        }

    }
}
