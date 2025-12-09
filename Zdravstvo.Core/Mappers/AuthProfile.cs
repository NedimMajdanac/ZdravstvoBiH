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
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<KorisnikDTO.RegisterKorisnikForProfile,Pacijent>();
            CreateMap<Korisnik, KorisnikDTO.ReadKorisnikDTO>();
        }
    }
}
