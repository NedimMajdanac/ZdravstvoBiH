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
    public class UstanovaProfile:Profile
    {
        public UstanovaProfile() 
        {
            CreateMap<Ustanova, UstanovaDTO.ReadUstanovaDTO>();
            CreateMap<UstanovaDTO.CreateUstanovaDTO, Ustanova>();
            CreateMap<UstanovaDTO.UpdateUstanovaDTO, Ustanova>();
        }
    }
}
