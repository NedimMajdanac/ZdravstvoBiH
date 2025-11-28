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
    public class DijagnozaProfile : Profile
    {
        public DijagnozaProfile()
        {
            CreateMap<Dijagnoza, DijagnozaDTO.ReadDijagnozaDTO>();
            CreateMap<DijagnozaDTO.CreateDijagnozaDTO, Dijagnoza>();
            CreateMap<DijagnozaDTO.UpdateDijagnozaDTO, Dijagnoza>();
        }
    }
}
