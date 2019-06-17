using AutoMapper;
using HomeIot.Data;
using HomeIot.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeIot.Controllers
{
    public class SensorDataDetailViewComponent : ViewComponent
    {
        private readonly DBContext _context;
        private readonly IMapper _mapper;

        public SensorDataDetailViewComponent(DBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(int sensor)
        {
            var dbData = await _context.Sensors.FindAsync(sensor);
            var model = _mapper.Map<SensorDetailViewModel>(dbData);
            var now = DateTime.Now;
            model.ActualValue = await _context.SensorData
                .Where(x => x.SensorId == sensor)
                .OrderByDescending(x => x.Timestamp)
                .Take(1)
                .Select(x => new SensorDataViewModel(sensor, x.Timestamp, x.Value))
                .FirstOrDefaultAsync();
            model.TodayMin = await _context.SensorData
                .Where(x => x.SensorId == sensor && x.Timestamp >= now.Date && x.Timestamp < now.Date.AddDays(1))
                .OrderBy(x => x.Value)
                .Take(1)
                .Select(x => new SensorDataViewModel(sensor, x.Timestamp, x.Value))
                .FirstOrDefaultAsync();
            model.TodayMax = await _context.SensorData
                .Where(x => x.SensorId == sensor && x.Timestamp >= now.Date && x.Timestamp < now.Date.AddDays(1))
                .OrderByDescending(x => x.Value)
                .Take(1)
                .Select(x => new SensorDataViewModel(sensor, x.Timestamp, x.Value))
                .FirstOrDefaultAsync();
            model.LastConnection = await _context.SensorData
                .Where(x => x.SensorId == sensor)
                .MaxAsync(x => x.Timestamp);
            model.ChartFrom = now.AddHours(-12);
            model.ChartTo = now;

            var avg = await _context.SensorData
                    .Where(x => x.SensorId == sensor)
                    .OrderByDescending(x => x.Timestamp)
                    .Take(3)
                    .AverageAsync(x => x.Value);
            if (model.ActualValue?.Value > avg)
                model.Trend = Trend.Increasing;
            else if (model.ActualValue?.Value < avg)
                model.Trend = Trend.Decreasing;
            else
                model.Trend = Trend.Consistent;

            return View("~/Views/Sensors/Components/SensorDataDetailComponent.cshtml", model);
        }
    }
}
