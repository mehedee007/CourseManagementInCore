using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CourseManagementInCore.Models;
using CourseManagementInCore.Models.ViewModels;

namespace CourseManagementInCore.Mapping
{
    public class MappingTools : Profile
    {
        public MappingTools()
        {
            CreateMap<Trainer, TrainerVM>();
            CreateMap<TrainerVM, Trainer>();
        }
    }
}
