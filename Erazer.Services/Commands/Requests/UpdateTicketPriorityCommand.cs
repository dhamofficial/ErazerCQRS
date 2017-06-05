﻿using System;
using MediatR;

namespace Erazer.Services.Commands.Requests
{
    public class UpdateTicketPriorityCommand : IRequest
    {
        public Guid TicketId { get; set; }
        public Guid UserId { get; set; }

        public string PriorityId { get; set; }
    }
}
