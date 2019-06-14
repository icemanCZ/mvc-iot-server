using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HomeIot.Data;
using AutoMapper;
using HomeIot.Models;

namespace home_iot.Controllers
{
    public class SensorsController : Controller
    {
        private readonly DBContext _context;
        private readonly IMapper _mapper;

        public SensorsController(DBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: Sensors
        public async Task<IActionResult> Index()
        {
            var data = await _context.Sensors.ToListAsync();
            return View(data.Select(x => _mapper.Map<SensorViewModel>(x)).ToList());
        }

        // GET: Sensors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sensor = await _context.Sensors
                .FirstOrDefaultAsync(m => m.SensorId == id);
            if (sensor == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<SensorViewModel>(sensor));
        }

        // GET: Sensors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sensor = await _context.Sensors.FindAsync(id);
            if (sensor == null)
            {
                return NotFound();
            }
            return View(_mapper.Map<SensorViewModel>(sensor));
        }

        // POST: Sensors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Units,Name,Description")] SensorViewModel sensor)
        {
            var sensorData = await _context.Sensors.FindAsync(id);
            if (sensorData == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    sensorData.Units = sensor.Units;
                    sensorData.Name = sensor.Name;
                    sensorData.Description = sensor.Description;
                    _context.Update(sensor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SensorExists(sensor.SensorId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(sensor);
        }

        private bool SensorExists(int id)
        {
            return _context.Sensors.Any(e => e.SensorId == id);
        }
    }
}
