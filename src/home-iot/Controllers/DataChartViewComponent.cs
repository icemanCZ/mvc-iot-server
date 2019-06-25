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
    public class DataChartViewComponent : ViewComponent
    {
        private const int MAX_SAMPLES_COUNT = 200;

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
                    .OrderBy(x => x.Timestamp)
                    .Select(x => new SensorDataViewModel(sensor, x.Timestamp, x.Value))
                    .ToListAsync()
                    
            };

            // TODO: tohle nejak udelat uz pri nacitani z DB. Nebo jeste lepe udelat nejakou intepolaci
            var indexModulo = Math.Ceiling(model.Data.Count() / (float)MAX_SAMPLES_COUNT);
            model.Data = model.Data.Where((x, index) => index % indexModulo == 0);

            return View("~/Views/Sensors/Components/DataChartComponent.cshtml", model);
        }
    }
}
