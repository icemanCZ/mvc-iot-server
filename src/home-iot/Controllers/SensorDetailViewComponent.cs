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
    public class SensorDetailViewComponent : ViewComponent
    {
        private readonly DBContext _context;

        public SensorDetailViewComponent(DBContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int sensor)
        {
            var now = DateTime.Now;
            var model = new SensorDetailViewModel()
            {
                ActualValue = await _context.SensorData
                    .Where(x => x.SensorId == sensor)
                    .OrderByDescending(x => x.Timestamp)
                    .Take(1)
                    .Select(x => new SensorDataViewModel(sensor, x.Timestamp, x.Value))
                    .FirstOrDefaultAsync(),
                TodayMin = await _context.SensorData
                    .Where(x => x.SensorId == sensor && x.Timestamp >= now.Date && x.Timestamp < now.Date.AddDays(1))
                    .OrderBy(x => x.Value)
                    .Take(1)
                    .Select(x => new SensorDataViewModel(sensor, x.Timestamp, x.Value))
                    .FirstOrDefaultAsync(),
                TodayMax = await _context.SensorData
                    .Where(x => x.SensorId == sensor && x.Timestamp >= now.Date && x.Timestamp < now.Date.AddDays(1))
                    .OrderByDescending(x => x.Value)
                    .Take(1)
                    .Select(x => new SensorDataViewModel(sensor, x.Timestamp, x.Value))
                    .FirstOrDefaultAsync(),
                ChartFrom = now.AddHours(-2),
                ChartTo = now,
            };

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

            return View("~/Views/SensorData/Components/SensorDetailPartialView.cshtml", model);
        }
    }
}
