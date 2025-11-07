using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zdravstvo.Core.DTOs;
using Zdravstvo.Core.Entities;

namespace Zdravstvo.Core.Mappers
{
    public class DoktorProfile:Profile
    {
        public DoktorProfile()
        {
            CreateMap<Doktor, DoktorDTO.ReadDoktorDTO>();
            CreateMap<DoktorDTO.UpdateDoktorDTO, Doktor>();
            CreateMap<DoktorDTO.CreateDoktorDTO, Doktor>();
        }
    }
}
