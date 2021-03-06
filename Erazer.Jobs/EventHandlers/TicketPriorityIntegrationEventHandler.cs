﻿using System;
using Erazer.Messages.IntegrationEvents.Events;
using MediatR;

namespace Erazer.Jobs.EventHandlers
{
    public class TicketPriorityIntegrationEventHandler : NotificationHandler<TicketPriorityIntegrationEvent>
    {
        protected override void HandleCore(TicketPriorityIntegrationEvent notification)
        {
            Console.WriteLine("Priority of a ticket was changed!");
            Console.WriteLine($"Ticket title: {notification.TicketTitle}");
            Console.WriteLine($"New priority: {notification.PriorityName}");
        }
    }
}
