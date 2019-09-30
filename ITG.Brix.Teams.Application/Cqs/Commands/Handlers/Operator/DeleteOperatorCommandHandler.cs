using ITG.Brix.Teams.Application.Bases;
using ITG.Brix.Teams.Application.Cqs.Commands.Definitions;
using ITG.Brix.Teams.Application.Resources;
using ITG.Brix.Teams.Domain;
using ITG.Brix.Teams.Domain.Repositories;
using ITG.Brix.Teams.Infrastructure.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ITG.Brix.Teams.Application.Cqs.Commands.Handlers
{
    public class DeleteOperatorCommandHandler : IRequestHandler<DeleteOperatorCommand, Result>
    {
        private readonly ITeamReadRepository _teamReadRepository;
        private readonly ITeamWriteRepository _teamWriteRepository;
        private readonly IOperatorWriteRepository _operatorWriteRepository;

        public DeleteOperatorCommandHandler(ITeamReadRepository teamReadRepository, ITeamWriteRepository teamWriteRepository, IOperatorWriteRepository operatorWriteRepository)
        {
            _teamReadRepository = teamReadRepository ?? throw new ArgumentNullException(nameof(teamReadRepository));
            _teamWriteRepository = teamWriteRepository ?? throw new ArgumentNullException(nameof(teamWriteRepository));
            _operatorWriteRepository = operatorWriteRepository ?? throw new ArgumentNullException(nameof(operatorWriteRepository));
        }

        public async Task<Result> Handle(DeleteOperatorCommand command, CancellationToken cancellationToken)
        {
            Result result;

            try
            {
                IEnumerable<Team> teams = await _teamReadRepository.ListAsync(string.Format("members/id eq '{0}'", command.Id), null, null);
                foreach (var item in teams)
                {
                    item.RemoveMember(command.Id);
                    await _teamWriteRepository.UpdateAsync(item);
                }

                await _operatorWriteRepository.DeleteAsync(command.Id, 0);
                return Result.Ok();
            }
            catch (EntityNotFoundDbException)
            {
                result = Result.Fail(new List<Failure>()
                {
                    new HandlerFault(){
                        Code = HandlerFaultCode.NotFound.Name,
                        Message = HandlerFailures.OperatorNotFound,
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
                result = Result.Fail(CustomFailures.DeleteOperatorFailure);
            }

            return result;
        }
    }
}
