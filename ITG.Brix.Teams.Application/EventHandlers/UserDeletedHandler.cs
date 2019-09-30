using ITG.Brix.Communication.Events;
using ITG.Brix.Teams.Application.Cqs.Commands.Definitions;
using MediatR;
using NServiceBus;
using System;
using System.Threading.Tasks;

namespace ITG.Brix.Teams.Application.EventHandlers
{
    public class UserDeletedHandler : IHandleMessages<UserDeleted>
    {
        private readonly IMediator _mediator;

        public UserDeletedHandler(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task Handle(UserDeleted message, IMessageHandlerContext context)
        {
            var command = new DeleteOperatorCommand(message.Id);
            await _mediator.Send(command);
        }
    }
}
