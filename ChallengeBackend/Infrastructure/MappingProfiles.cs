using AutoMapper;
using Challenge.Domain.Dtos;
using Challenge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Infrastructure
{
    internal class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Permission, PermissionsDto>();
        }
    }
}

