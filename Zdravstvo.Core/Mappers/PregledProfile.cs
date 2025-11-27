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
    public class PregledProfile : Profile
    {
        public PregledProfile()
        {
            CreateMap<Pregled, PregledDTO.ReadPregledDTO>();
            CreateMap<PregledDTO.CreatePregledDTO, Pregled>();
            CreateMap<PregledDTO.UpdatePregledDTO, Pregled>();
        }
    }
}
