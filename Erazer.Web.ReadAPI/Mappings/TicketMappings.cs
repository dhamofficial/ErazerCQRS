﻿using AutoMapper;
using Erazer.Domain.Data.DTOs;
using Erazer.Domain.Data.DTOs.Events;
using Erazer.Web.ReadAPI.ViewModels;
using Erazer.Web.ReadAPI.ViewModels.Events;

namespace Erazer.Web.ReadAPI.Mappings
{
    public class TicketMappings : Profile
    {
        public TicketMappings()
        {
            CreateMap<TicketDto, TicketViewModel>();
            CreateMap<TicketDto, TicketListViewModel>();
            CreateMap<TicketListDto, TicketListViewModel>();

            CreateMap<TicketEventDto, TicketEventViewModel>();
            CreateMap<StatusEventDto, TicketStatusEventViewModel>()
                .IncludeBase<TicketEventDto, TicketEventViewModel>();
            CreateMap<PriorityEventDto, TicketPriorityEventViewModel>()
                .IncludeBase<TicketEventDto, TicketEventViewModel>();
            CreateMap<CommentEventDto, TicketCommentEventViewModel>()
                .IncludeBase<TicketEventDto, TicketEventViewModel>();
            CreateMap<StatusEventDto, TicketStatusEventViewModel>()
                .IncludeBase<TicketEventDto, TicketEventViewModel>();
            CreateMap<CreatedEventDto, TicketCreatedEventViewModel>()
                .IncludeBase<TicketEventDto, TicketEventViewModel>();
        }
    }
}
