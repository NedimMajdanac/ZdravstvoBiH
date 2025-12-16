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
    public class SpecijelizacijaProfile : Profile
    {
        public SpecijelizacijaProfile()
        {
            CreateMap<Specijalizacija, SpecijelizacijaDTO.ReadSpecijalizacijaDTO>();
            CreateMap<SpecijelizacijaDTO.CreateSpecijalizacijaDTO, Specijalizacija>();
            CreateMap<SpecijelizacijaDTO.UpdateSpecijalizacijaDTO, Specijalizacija>();
        }
    }
}
