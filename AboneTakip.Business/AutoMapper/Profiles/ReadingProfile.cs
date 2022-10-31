using AboneTakip.DTOs.Readings;
using AboneTakip.Entity.Concrete;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AboneTakip.Business.AutoMapper.Profiles
{
    public class ReadingProfile : Profile
    {
        public ReadingProfile()
        {
            CreateMap<ReadingDTO, Reading>().ReverseMap();
        }
    }
}
