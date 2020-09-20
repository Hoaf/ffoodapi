using AutoMapper;
using DataService.Models.ServiceModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataService.Models.AutoMapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Account, AccountServiceModel>().ReverseMap();
        }
    }
}
