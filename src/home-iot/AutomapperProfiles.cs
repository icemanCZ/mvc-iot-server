using AutoMapper;
using HomeIot.Data;
using HomeIot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeIot
{
    public class SensorProfile : Profile
    {
        public SensorProfile()
        {
            CreateMap<Sensor, SensorViewModel>();
            CreateMap<SensorViewModel, Sensor>()
                .ForMember(s => s.Name, opt => opt.MapFrom(svm => svm.Name))
                .ForMember(s => s.Description, opt => opt.MapFrom(svm => svm.Description))
                .ForMember(s => s.Units, opt => opt.MapFrom(svm => svm.Units));
            CreateMap<Sensor, SensorDetailViewModel>()
                .ForMember(svm => svm.SensorId, opt => opt.MapFrom(s => s.SensorId))
                .ForMember(svm => svm.SensorName, opt => opt.MapFrom(s => s.Name))
                .ForMember(svm => svm.SensorDescription, opt => opt.MapFrom(s => s.Description))
                .ForMember(svm => svm.IsFavorited, opt => opt.MapFrom(s => s.IsFavorited))
                .ForMember(svm => svm.Units, opt => opt.MapFrom(s => s.Units));
        }
    }

    public class SensorGroupProfile : Profile
    {
        public SensorGroupProfile()
        {
            CreateMap<SensorGroup, SensorGroupViewModel>();
        }
    }

    public class ApplicationEventProfile : Profile
    {
        public ApplicationEventProfile()
        {
            CreateMap<ApplicationEvent, ApplicationEventViewModel>()
                .ForMember(evm => evm.ApplicationEventId, opt => opt.MapFrom(e => e.SensorId))
                .ForMember(evm => evm.EventType, opt => opt.MapFrom(e => e.EventType))
                .ForMember(evm => evm.Resolved, opt => opt.MapFrom(e => e.Resolved))
                .ForMember(evm => evm.ResolvedTimestamp, opt => opt.MapFrom(e => e.ResolvedTimestamp))
                .ForMember(evm => evm.Timestamp, opt => opt.MapFrom(e => e.Timestamp))
                .ForMember(evm => evm.SensorId, opt => opt.MapFrom(e => e.SensorId))
                .ForMember(evm => evm.SensorName, opt => opt.MapFrom(e => e.Sensor.Name))
                .ForMember(evm => evm.SensorUnits, opt => opt.MapFrom(e => e.Sensor.Units));
        }
    }
}
