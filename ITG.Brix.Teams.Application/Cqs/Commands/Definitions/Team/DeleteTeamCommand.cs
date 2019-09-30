using ITG.Brix.Teams.Application.Bases;
using MediatR;
using System;

namespace ITG.Brix.Teams.Application.Cqs.Commands.Definitions
{
    public class DeleteTeamCommand : IRequest<Result>
    {
        public Guid Id { get; private set; }
        public int Version { get; private set; }

        public DeleteTeamCommand(Guid id, int version)
        {
            Id = id;
            Version = version;
        }
    }
}
