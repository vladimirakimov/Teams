using ITG.Brix.Teams.Application.Bases;
using ITG.Brix.Teams.Application.Cqs.Commands.Definitions;
using ITG.Brix.Teams.Application.Exceptions;
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

    public class UpdateTeamCommandHandler : IRequestHandler<UpdateTeamCommand, Result>
    {
        private readonly ITeamWriteRepository _teamWriteRepository;
        private readonly ITeamReadRepository _teamReadRepository;
        private readonly IVersionProvider _versionProvider;

        public UpdateTeamCommandHandler(ITeamWriteRepository teamWriteRepository,
                                        ITeamReadRepository teamReadRepository,
                                        IVersionProvider versionProvider)
        {
            _teamWriteRepository = teamWriteRepository ?? throw Error.ArgumentNull(nameof(teamWriteRepository));
            _teamReadRepository = teamReadRepository ?? throw Error.ArgumentNull(nameof(teamReadRepository));
            _versionProvider = versionProvider ?? throw Error.ArgumentNull(nameof(versionProvider));
        }

        public async Task<Result> Handle(UpdateTeamCommand command, CancellationToken cancellationToken)
        {
            Result result;

            try
            {
                var team = await _teamReadRepository.GetAsync(command.Id);

                if (team.Version != command.Version)
                {
                    throw new CommandVersionException();
                }

                team.ChangeName(new Name(command.Name));
                team.SetImage(command.Image);
                team.SetDescription((Description)command.Description);

                if (Guid.TryParse(command.Layout, out Guid layoutId) && layoutId != default(Guid))
                {
                    var layout = new Layout(layoutId);
                    team.SetLayout(layout);
                }

                team.SetFilterContent(command.FilterContent);
                team.ChangeDriverWait(DriverWait.Parse(command.DriverWait));

                team.ClearMembers();
                foreach (var operatorId in command.Members)
                {
                    var member = new Member(operatorId);
                    team.AddMember(member);
                }

                team.SetVersion(_versionProvider.Generate());

                await _teamWriteRepository.UpdateAsync(team);
                result = Result.Ok(team.Version);
            }
            catch (EntityNotFoundDbException)
            {
                result = Result.Fail(new List<Failure>()
                {
                    new HandlerFault(){
                        Code = HandlerFaultCode.NotFound.Name,
                        Message = HandlerFailures.TeamNotFound,
                        Target = "id"
                    }
                });
            }
            catch (CommandVersionException)
            {
                result = Result.Fail(new List<Failure>()
                {
                    new HandlerFault(){
                        Code = HandlerFaultCode.NotMet.Name,
                        Message = HandlerFailures.NotMet,
                        Target = "version"
                    }
                });
            }
            catch (UniqueKeyException)
            {
                result = Result.Fail(new List<Failure>()
                {
                    new HandlerFault(){
                        Code = HandlerFaultCode.Conflict.Name,
                        Message = HandlerFailures.ConflictTeam,
                        Target = "name"
                    }
                });
            }
            catch
            {
                result = Result.Fail(CustomFailures.UpdateTeamFailure);
            }

            return result;

        }
    }
}
