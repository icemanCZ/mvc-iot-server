using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HomeIot.Data;
using HomeIot.Models;
using Microsoft.AspNetCore.Mvc;

namespace home_iot.Controllers
{
    public class HomeController : Controller
    {
        private readonly DBContext _context;
        private readonly IMapper _mapper;

        public HomeController(DBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return RedirectToActionPermanent("Favorites", "Sensors");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
