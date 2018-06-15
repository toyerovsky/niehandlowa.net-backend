using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using niehandlowa.net.Bll.Models;
using niehandlowa.net.Dal.Entities;

namespace niehandlowa.net.Bll.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //DbEntities
            CreateMap<POIModel, POIEntity>().ReverseMap();
            CreateMap<OpeningHoursModel, OpeningHoursEntity>().ReverseMap();
        }
    }
}
