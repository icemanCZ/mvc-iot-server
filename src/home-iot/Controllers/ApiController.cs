using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HomeIot.ActionFilters;
using HomeIot.Data;
using HomeIot.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace home_iot.Controllers
{
    public class ApiController : Controller
    {
        private const int _okInterval = 12;
        private readonly DBContext _context;
        private readonly IMapper _mapper;
        private readonly IEventService _eventService;

        public ApiController(DBContext context, IMapper mapper, IEventService eventService)
        {
            _context = context;
            _mapper = mapper;
            _eventService = eventService;
        }

        //[ServiceFilter(typeof(APIIPFilter))]
        public IActionResult Write(string sensorIdentificator, float value)
        {
            if (string.IsNullOrWhiteSpace(sensorIdentificator))
                return UnprocessableEntity();

            if (value == 85.5)
                return Forbid();

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

            _eventService.NotifySensorConnected(sensorId.Value);

            return Ok();
        }

        public async Task<JsonResult> SensorList()
        {
            var okTime = DateTime.Now.AddMinutes(-_okInterval);

            return new JsonResult(await _context.Sensors
                .Select(x => new
                {
                    Id = x.SensorId,
                    Name = x.Name,
                    IsFavorited = x.IsFavorited,
                    Units = x.Units.GetText(),
                    IsOk = x.Data.OrderByDescending(d => d.Timestamp).FirstOrDefault().Timestamp > okTime,
                })
                .ToListAsync());
        }

        public async Task<JsonResult> GroupList()
        {
            var okTime = DateTime.Now.AddMinutes(-_okInterval);

            return new JsonResult(await _context.SensorGroups
                .Select(x => new
                {
                    Id = x.SensorGroupId,
                    Name = x.Name,
                    IsOk = !x.Sensors.Any(s => s.Sensor.Data.OrderByDescending(d => d.Timestamp).FirstOrDefault().Timestamp < okTime)
                })
                .ToListAsync());
        }

        public async Task<JsonResult> SensorData(int sensorId, long? from, long? to)
        {
            var dateFrom = from != null ? DateTime.FromFileTimeUtc(from.Value) : DateTime.Now.AddDays(-1);
            var dateTo = to != null ? DateTime.FromFileTimeUtc(to.Value) : DateTime.Now;

            return new JsonResult(await _context.SensorData
                .Where(x => x.SensorId == sensorId && x.Timestamp >= dateFrom && x.Timestamp <= dateTo)
                .Select(x => new
                {
                    T = new DateTime(x.Timestamp.Ticks - (x.Timestamp.Ticks % TimeSpan.TicksPerSecond), x.Timestamp.Kind),
                    V = x.Value
                })
                .ToListAsync());
        }

        public async Task<JsonResult> FavoritedSensorsData(long? from, long? to)
        {
            var dateFrom = from != null ? DateTime.FromFileTimeUtc(from.Value) : DateTime.Now.AddDays(-1);
            var dateTo = to != null ? DateTime.FromFileTimeUtc(to.Value) : DateTime.Now;
            var okTime = DateTime.Now.AddMinutes(-_okInterval);

            return new JsonResult(await _context.Sensors
                .Where(x => x.IsFavorited)
                .Select(x => new
                {
                    Id = x.SensorId,
                    Name = x.Name,
                    Units = x.Units.GetText(),
                    Data = x.Data.Where(d => d.Timestamp >= dateFrom && d.Timestamp <= dateTo)
                                 .Select(d => new { T = new DateTime(d.Timestamp.Ticks - (d.Timestamp.Ticks % TimeSpan.TicksPerSecond), d.Timestamp.Kind), V = d.Value }),
                    IsOk = x.Data.OrderByDescending(d => d.Timestamp).FirstOrDefault().Timestamp > okTime,
                })
                .ToListAsync());
        }

        public async Task<JsonResult> GroupSensorsData(int groupId, long? from, long? to)
        {
            var dateFrom = from != null ? DateTime.FromFileTimeUtc(from.Value) : DateTime.Now.AddDays(-1);
            var dateTo = to != null ? DateTime.FromFileTimeUtc(to.Value) : DateTime.Now;
            var okTime = DateTime.Now.AddMinutes(-_okInterval);

            return new JsonResult(await _context.SensorInSensorGroups
                .Where(x => x.SensorGroupId == groupId)
                .Select(x => new
                {
                    Id = x.SensorId,
                    Name = x.Sensor.Name,
                    Units = x.Sensor.Units.GetText(),
                    Data = x.Sensor.Data.Where(d => d.Timestamp >= dateFrom && d.Timestamp <= dateTo)
                                 .Select(d => new { T = new DateTime(d.Timestamp.Ticks - (d.Timestamp.Ticks % TimeSpan.TicksPerSecond), d.Timestamp.Kind), V = d.Value }),
                    IsOk = x.Sensor.Data.OrderByDescending(d => d.Timestamp).FirstOrDefault().Timestamp > okTime,
                })
                .ToListAsync());
        }
    }
}