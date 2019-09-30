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

namespace ITG.Brix.Teams.Application.Cqs.Commands.Handlers.Operator
{
    public class UpdateOperatorCommandHandler : IRequestHandler<UpdateOperatorCommand, Result>
    {
        private readonly IOperatorWriteRepository _operatorWriteRepository;

        public UpdateOperatorCommandHandler(IOperatorWriteRepository operatorWriteRepository)
        {
            _operatorWriteRepository = operatorWriteRepository ?? throw Error.ArgumentNull(nameof(operatorWriteRepository));
        }

        public async Task<Result> Handle(UpdateOperatorCommand command, CancellationToken cancellationToken)
        {
            Result result;

            try
            {
                var operatorToUpdate = new Domain.Operator(command.Id,
                                                    command.Login,
                                                    command.FirstName,
                                                    command.LastName);

                await _operatorWriteRepository.UpdateAsync(operatorToUpdate);
                return Result.Ok();
            }
            catch (UniqueKeyException)
            {
                result = Result.Fail(new List<Failure>()
                {
                    new HandlerFault(){
                        Code = HandlerFaultCode.Conflict.Name,
                        Message = HandlerFailures.ConflictOperator,
                        Target = "login"
                    }
                });
            }
            catch
            {
                result = Result.Fail(CustomFailures.CreateOperatorFailure);
            }

            return result;
        }
    }
}
