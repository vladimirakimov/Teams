using ITG.Brix.Teams.Application.Bases;
using ITG.Brix.Teams.Application.Cqs.Commands.Definitions;
using ITG.Brix.Teams.Application.Internal;
using ITG.Brix.Teams.Application.Resources;
using ITG.Brix.Teams.Domain;
using ITG.Brix.Teams.Domain.Repositories;
using ITG.Brix.Teams.Infrastructure.Exceptions;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ITG.Brix.Teams.Application.Cqs.Commands.Operators.Handlers
{
    public class CreateOperatorCommandHandler : IRequestHandler<CreateOperatorCommand, Result>
    {
        private readonly IOperatorWriteRepository _operatorWriteRepository;

        public CreateOperatorCommandHandler(IOperatorWriteRepository operatorWriteRepository)
        {
            _operatorWriteRepository = operatorWriteRepository ?? throw Error.ArgumentNull(nameof(operatorWriteRepository));
        }

        public async Task<Result> Handle(CreateOperatorCommand command, CancellationToken cancellationToken)
        {
            Result result;

            try
            {
                var operatorToCreate = new Operator(command.Id,
                                                    command.Login,
                                                    command.FirstName,
                                                    command.LastName);

                await _operatorWriteRepository.CreateAsync(operatorToCreate);
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
