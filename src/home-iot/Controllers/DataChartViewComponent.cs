﻿using HomeIot.Data;
using HomeIot.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeIot.Controllers
{
    public class DataChartViewComponent : ViewComponent
    {
        private readonly DBContext _context;

        public DataChartViewComponent(DBContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int sensor, DateTime from, DateTime to)
        {
            var model = new ChartDataViewModel()
            {
                SensorId = sensor,
                Data = await _context.SensorData
                    .Where(x => x.SensorId == sensor && x.Timestamp >= from && x.Timestamp <= to)
                    .Select(x => new SensorDataViewModel(sensor, x.Timestamp, x.Value))
                    .OrderBy(x => x.Timestamp)
                    .ToListAsync()
            };
            return View("~/Views/SensorData/Components/DataChartPartialView.cshtml", model);
        }
    }
}
