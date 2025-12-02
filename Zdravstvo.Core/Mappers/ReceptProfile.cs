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
    public class ReceptProfile : Profile
    {
        public ReceptProfile()
        {
            CreateMap<Recepti, ReceptDTO.ReadReceptDTO>();
            CreateMap<ReceptDTO.CreateReceptDTO, Recepti>();
            CreateMap<ReceptDTO.UpdateReceptDTO, ReceptDTO>();
        }
    }
}
