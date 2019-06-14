﻿using HomeIot.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HomeIot.Models
{
    public class SensorDetailViewModel
    {
        public int SensorId { get; set; }
        public string SensorName { get; set; }
        public string SensorDescription { get; set; }
        public Unit Unit { get; set; }

        [Display(Name = "Aktuální hodnota:")]
        public SensorDataViewModel ActualValue { get; set; }
        [Display(Name = "Dnešní minimum:")]
        public SensorDataViewModel TodayMax { get; set; }
        [Display(Name = "Dnešní maximum:")]
        public SensorDataViewModel TodayMin { get; set; }
        public Trend Trend { get; set; }
        public DateTime ChartFrom { get; set; }
        public DateTime ChartTo { get; set; }
    }
}