﻿using System;
using System.Threading.Tasks;
using AutoMapper;
using Erazer.Domain.Data.DTOs.Events;
using Erazer.Domain.Data.Repositories;
using Erazer.Domain.Events;
using Erazer.Framework.FrontEnd;
using Erazer.Web.ReadAPI.DomainEvents.EventHandlers.Redux;
using Erazer.Web.ReadAPI.ViewModels.Events;
using MediatR;

namespace Erazer.Web.ReadAPI.DomainEvents.EventHandlers
{
    public class TicketStatusEventHandler : AsyncNotificationHandler<TicketStatusDomainEvent>
    {
        private readonly IMapper _mapper;
        private readonly IWebsocketEmittor _websocketEmittor;
        private readonly ITicketQueryRepository _ticketRepository;
        private readonly IStatusQueryRepository _statusRepository;
        private readonly ITicketEventQueryRepository _eventRepository;

        public TicketStatusEventHandler(IMapper mapper, IWebsocketEmittor websocketEmittor, ITicketQueryRepository ticketRepository, IStatusQueryRepository statusRepository, ITicketEventQueryRepository eventRepository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _websocketEmittor = websocketEmittor ?? throw new ArgumentNullException(nameof(websocketEmittor));
            _ticketRepository = ticketRepository ?? throw new ArgumentNullException(nameof(ticketRepository));
            _statusRepository = statusRepository ?? throw new ArgumentNullException(nameof(statusRepository));
            _eventRepository = eventRepository ?? throw new ArgumentNullException(nameof(eventRepository));
        }

        protected override async Task HandleCore(TicketStatusDomainEvent message)
        {
            var ticketTask =  _ticketRepository.Find(message.AggregateRootId.ToString());
            var oldStatusTask = _statusRepository.Find(message.FromStatusId);
            var newStatusTask = _statusRepository.Find(message.ToStatusId);

            await Task.WhenAll(ticketTask, oldStatusTask, newStatusTask);

            var ticket = ticketTask.Result;
            var oldStatus = oldStatusTask.Result;
            var newStatus = newStatusTask.Result;

            // Update ticket in ReadModel
            ticket.Status = newStatus;

            // Add ticket event in ReadModel
            var ticketEvent = new StatusEventDto(oldStatus, newStatus)
            {
                Id = Guid.NewGuid().ToString(),
                TicketId = message.AggregateRootId.ToString(),
                Created = message.Created,
                UserId = message.UserId.ToString(),
            };

            await Task.WhenAll(
                    _ticketRepository.Update(ticket),
                    _eventRepository.Add(ticketEvent));

            await _websocketEmittor.Emit(
                new ReduxUpdateStatusAction(_mapper.Map<TicketStatusEventViewModel>(ticketEvent)));
        }
    }
}
