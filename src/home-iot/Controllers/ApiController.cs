using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HomeIot.ActionFilters;
using HomeIot.Data;
using HomeIot.Services;
using Microsoft.AspNetCore.Mvc;

namespace home_iot.Controllers
{
    public class ApiController : Controller
    {
        private readonly DBContext _context;
        private readonly IMapper _mapper;
        private readonly EventService _eventService;

        public ApiController(DBContext context, IMapper mapper, EventService eventService)
        {
            _context = context;
            _mapper = mapper;
            _eventService = eventService;
        }

        [ServiceFilter(typeof(APIIPFilter))]
        public IActionResult Write(string sensorIdentificator, float value)
        {
            if (string.IsNullOrWhiteSpace(sensorIdentificator))
                return UnprocessableEntity();
            var sensorId = _context.Sensors.FirstOrDefault(x => x.Identificator == sensorIdentificator)?.SensorId;
            if (sensorId == null)
            {
                var sensor = new Sensor();
                sensor.Name = "new sensor";
                sensor.Identificator = sensorIdentificator;
                sensor.Units = Unit.Unknown;
                _context.Sensors.Add(sensor);
                _context.SaveChanges();
                sensorId = sensor.SensorId;

                _eventService.NewSensorRegistered(sensorId.Value);
            }

            var data = new SensorData();
            data.SensorId = sensorId.Value;
            data.Timestamp = DateTime.Now;
            data.Value = value;
            _context.SensorData.Add(data);
            _context.SaveChanges();

            return Ok();
        }
    }
}