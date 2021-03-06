﻿using System.Threading.Tasks;
using Erazer.Domain;
using Erazer.Framework.Domain;
using Erazer.Web.WriteAPI.Commands.Requests;
using MediatR;

namespace Erazer.Web.WriteAPI.Commands.Handlers
{
    public class NewTicketCommandHandler : AsyncRequestHandler<NewTicketCommand>
    {
        private readonly IAggregateRepository _repository;

        public NewTicketCommandHandler(IAggregateRepository repository)
        {
            _repository = repository;
        }

        protected override async Task HandleCore(NewTicketCommand message)
        {
            var ticket = new Ticket(message.Id, message.Title, message.Description, message.PriorityId, message.UserId, message.Files);
            await _repository.Save(ticket);
        }
    }
}
