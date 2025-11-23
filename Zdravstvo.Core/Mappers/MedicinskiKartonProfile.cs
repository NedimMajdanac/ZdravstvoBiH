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
    public class MedicinskiKartonProfile:Profile
    {
        public MedicinskiKartonProfile()
        {
            CreateMap<MedicinskiKarton, MedicinskiKartonDTO.ReadMedicinskiKartonDTO>();
            CreateMap<MedicinskiKartonDTO.CreateMedicinskiKartonDTO, MedicinskiKarton>();
            CreateMap<MedicinskiKartonDTO.UpdateMedicinskiKartonDTO, MedicinskiKarton>();
        }
    }
}
