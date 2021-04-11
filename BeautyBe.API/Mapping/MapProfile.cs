using AutoMapper;
using BeautyBe.API.Dtos.Auth;
using BeautyBe.Core.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeautyBe.API.Mapping
{
    public class MapProfile:Profile
    {
        public MapProfile()
        {
            CreateMap<User, LoginDto>();
            CreateMap<LoginDto, User>();

        }
    }
}
