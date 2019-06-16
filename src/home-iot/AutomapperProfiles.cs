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
                .ForMember(s => s.SensorId, opt => opt.MapFrom(svm => svm.SensorId))
                .ForMember(s => s.SensorName, opt => opt.MapFrom(svm => svm.Name))
                .ForMember(s => s.SensorDescription, opt => opt.MapFrom(svm => svm.Description))
                .ForMember(s => s.IsFavorited, opt => opt.MapFrom(svm => svm.IsFavorited))
                .ForMember(s => s.Units, opt => opt.MapFrom(svm => svm.Units));

        }
    }
}
