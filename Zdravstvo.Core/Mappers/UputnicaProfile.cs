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
    public class UputnicaProfile : Profile
    {
        public UputnicaProfile()
        {
            CreateMap<Uputnica, UputnicaDTO.ReadUputnicaDTO>();
            CreateMap<UputnicaDTO.CreateUputnicaDTO, Uputnica>();
        }
    }
}
