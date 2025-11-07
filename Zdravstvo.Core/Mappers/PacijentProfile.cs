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
    public class PacijentProfile : Profile
    {
        public PacijentProfile()
        {
            CreateMap<Pacijent, PacijentDTO.ReadPacijentDTO>();
            CreateMap<PacijentDTO.UpdatePacijentDTO, Pacijent>();
            CreateMap<PacijentDTO.CreatePacijentDTO, Pacijent>();
        }
    }
}
