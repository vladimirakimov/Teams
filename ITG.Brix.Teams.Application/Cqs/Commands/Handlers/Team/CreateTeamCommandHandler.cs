using ITG.Brix.Teams.Application.Bases;
using ITG.Brix.Teams.Application.Cqs.Commands.Definitions;
using ITG.Brix.Teams.Application.Internal;
using ITG.Brix.Teams.Application.Resources;
using ITG.Brix.Teams.Domain;
using ITG.Brix.Teams.Domain.Repositories;
using ITG.Brix.Teams.Infrastructure.Exceptions;
using ITG.Brix.Teams.Infrastructure.Providers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ITG.Brix.Teams.Application.Cqs.Commands.Handlers
{
    public class CreateTeamCommandHandler : IRequestHandler<CreateTeamCommand, Result>
    {
        private readonly ITeamWriteRepository _teamWriteRepository;
        private readonly IIdentifierProvider _identifierProvider;
        private readonly IVersionProvider _versionProvider;

        public CreateTeamCommandHandler(ITeamWriteRepository teamWriteRepository,
                                        IIdentifierProvider identifierProvider,
                                        IVersionProvider versionProvider)
        {
            _teamWriteRepository = teamWriteRepository ?? throw Error.ArgumentNull(nameof(teamWriteRepository));
            _identifierProvider = identifierProvider ?? throw Error.ArgumentNull(nameof(identifierProvider));
            _versionProvider = versionProvider ?? throw Error.ArgumentNull(nameof(versionProvider));
        }

        public async Task<Result> Handle(CreateTeamCommand command, CancellationToken cancellationToken)
        {
            var id = _identifierProvider.Generate();

            var teamToCreate = new Team(TeamId.With(id), new Name(command.Name));

            teamToCreate.SetImage(command.Image);
            teamToCreate.SetDescription((Description)command.Description);

            if (Guid.TryParse(command.Layout, out Guid layoutId) && layoutId != default(Guid))
            {
                var layout = new Layout(layoutId);
                teamToCreate.SetLayout(layout);
            }

            teamToCreate.SetVersion(_versionProvider.Generate());

            Result result;

            try
            {
                await _teamWriteRepository.CreateAsync(teamToCreate);
                return Result.Ok(id, teamToCreate.Version);
            }
            catch (UniqueKeyException)
            {
                result = Result.Fail(new List<Failure>()
                {
                    new HandlerFault(){
                        Code = HandlerFaultCode.Conflict.Name,
                        Message = HandlerFailures.ConflictTeam,
                        Target = "name" }
                    }
                );
            }
            catch
            {
                result = Result.Fail(CustomFailures.CreateTeamFailure);
            }

            return result;
        }
    }
}
