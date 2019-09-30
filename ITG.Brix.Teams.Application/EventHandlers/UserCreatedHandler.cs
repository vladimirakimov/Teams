using ITG.Brix.Communication.Events;
using ITG.Brix.Teams.Application.Cqs.Commands.Definitions;
using MediatR;
using NServiceBus;
using System.Threading.Tasks;

namespace ITG.Brix.Teams.Application.EventHandlers
{
    public class UserCreatedHandler : IHandleMessages<UserCreated>
    {
        private readonly IMediator _mediator;

        public UserCreatedHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Handle(UserCreated message, IMessageHandlerContext context)
        {
            var command = new CreateOperatorCommand(message.Id, message.Login, message.FirstName, message.LastName);
            await _mediator.Send(command);
        }
    }
}
