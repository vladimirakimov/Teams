using ITG.Brix.Teams.Application.Bases;
using MediatR;
using System;

namespace ITG.Brix.Teams.Application.Cqs.Queries.Definitions
{
    public class GetTeamQuery : IRequest<Result>
    {
        public Guid Id { get; private set; }

        public GetTeamQuery(Guid id)
        {
            Id = id;
        }
    }
}
