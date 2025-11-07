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
    public class TerminProfile : Profile
    {
        public TerminProfile() 
        {
            CreateMap<Termin, TerminDTO.ReadTerminDTO>();
            CreateMap<TerminDTO.CreateTerminDTO, Termin>();
            CreateMap<TerminDTO.UpdateTerminDTO, Termin>();
        }
    }
}
