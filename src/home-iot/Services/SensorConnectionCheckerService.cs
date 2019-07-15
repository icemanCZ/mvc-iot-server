using AutoMapper;
using HomeIot.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HomeIot.Services
{
    public class SensorConnectionCheckerService : BackgroundService
    {
        private readonly AppSettings _settings;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public SensorConnectionCheckerService(IServiceScopeFactory serviceScopeFactory, IOptions<AppSettings> settings)
        {
            _settings = settings.Value;
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    DBContext context = scope.ServiceProvider.GetRequiredService<DBContext>();
                    var dataThreshold = DateTime.Now.AddMilliseconds(-ApplicationEvent.SENSOR_CONNECTION_LOST_INTERVAL);
                    var disconnected = await context
                        .Sensors
                        .Where(s =>
                            s.Data.OrderByDescending(x => x.Timestamp).FirstOrDefault().Timestamp < dataThreshold
                            && !s.Events.Any(e => e.EventType == ApplicationEventType.SensorConnectionLost && !e.Resolved))
                        .Select(x => x.SensorId)
                        .ToListAsync();

                    if (disconnected.Any())
                    {
                        IEventService eventService = scope.ServiceProvider.GetRequiredService<IEventService>();
                        foreach (var id in disconnected)
                            eventService.SensorConnectionLost(id);
                    }
                    
                }

                await Task.Delay(_settings.SensorConnectionCheckInterval * 1000, stoppingToken);
            }
        }
    }
}
