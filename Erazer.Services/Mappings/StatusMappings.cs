﻿using AutoMapper;
using Erazer.DAL.ReadModel;
using Erazer.Services.Queries.ViewModels;

namespace Erazer.Services.Mappings
{
    public class StatusMappings : Profile
    {
        public StatusMappings()
        {
            CreateMap<StatusDto, StatusViewModel>();
        }
    }
}
