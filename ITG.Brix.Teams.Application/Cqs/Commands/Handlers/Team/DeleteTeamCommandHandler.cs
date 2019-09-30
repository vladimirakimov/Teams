using ITG.Brix.Teams.Application.Bases;
using ITG.Brix.Teams.Application.Cqs.Commands.Definitions;
using ITG.Brix.Teams.Application.Internal;
using ITG.Brix.Teams.Application.Resources;
using ITG.Brix.Teams.Domain.Repositories;
using ITG.Brix.Teams.Infrastructure.Exceptions;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ITG.Brix.Teams.Application.Cqs.Commands.Handlers
{
    public class DeleteTeamCommandHandler : IRequestHandler<DeleteTeamCommand, Result>
    {
        private readonly ITeamWriteRepository _teamWriteRepository;

        public DeleteTeamCommandHandler(ITeamWriteRepository teamWriteRepository)
        {
            _teamWriteRepository = teamWriteRepository ?? throw Error.ArgumentNull(nameof(teamWriteRepository));
        }

        public async Task<Result> Handle(DeleteTeamCommand command, CancellationToken cancellationToken)
        {
            Result result;

            try
            {
                await _teamWriteRepository.DeleteAsync(command.Id, command.Version);
                return Result.Ok();
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
            catch (EntityVersionDbException)
            {
                result = Result.Fail(new List<Failure>()
                {
                    new HandlerFault(){
                        Code = HandlerFaultCode.NotMet.Name,
                        Message = HandlerFailures.NotMet,
                        Target =  "version"
                    }
                });
            }
            catch
            {
                result = Result.Fail(CustomFailures.DeleteTeamFailure);
            }

            return result;
        }
    }
}
