using ITG.Brix.Teams.Application.Bases;
using MediatR;
using System;

namespace ITG.Brix.Teams.Application.Cqs.Commands.Definitions
{
    public class DeleteOperatorCommand : IRequest<Result>
    {
        public Guid Id { get; private set; }

        public DeleteOperatorCommand(Guid id)
        {
            Id = id;
        }
    }
}
