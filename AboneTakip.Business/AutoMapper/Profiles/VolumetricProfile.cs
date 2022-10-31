using AboneTakip.DTOs.Volumetrics;
using AboneTakip.Entity.Concrete;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AboneTakip.Business.AutoMapper.Profiles
{
    public class VolumetricProfile : Profile
    {
        public VolumetricProfile()
        {
            CreateMap<VolumetricDTO, Volumetric>().ReverseMap();
        }
    }
}
