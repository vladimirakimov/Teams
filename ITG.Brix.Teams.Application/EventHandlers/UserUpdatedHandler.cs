using ITG.Brix.Communication.Events;
using ITG.Brix.Teams.Application.Cqs.Commands.Definitions;
using MediatR;
using NServiceBus;
using System.Threading.Tasks;

namespace ITG.Brix.Teams.Application.EventHandlers
{
    public class UserUpdatedHandler : IHandleMessages<UserUpdated>
    {
        private readonly IMediator _mediator;

        public UserUpdatedHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Handle(UserUpdated message, IMessageHandlerContext context)
        {
            var command = new UpdateOperatorCommand(message.Id, message.Login, message.FirstName, message.LastName);
            await _mediator.Send(command);
        }
    }
}
