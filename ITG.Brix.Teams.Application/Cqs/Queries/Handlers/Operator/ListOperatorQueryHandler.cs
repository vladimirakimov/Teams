using AutoMapper;
using ITG.Brix.Teams.Application.Bases;
using ITG.Brix.Teams.Application.Cqs.Queries.Definitions;
using ITG.Brix.Teams.Application.Cqs.Queries.Models;
using ITG.Brix.Teams.Application.Extensions;
using ITG.Brix.Teams.Application.Internal;
using ITG.Brix.Teams.Application.Resources;
using ITG.Brix.Teams.Domain;
using ITG.Brix.Teams.Domain.Repositories;
using ITG.Brix.Teams.Infrastructure.Exceptions;
using ITG.Brix.Teams.Infrastructure.Providers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace ITG.Brix.Teams.Application.Cqs.Queries.Operators.Handlers
{
    public class ListOperatorQueryHandler : IRequestHandler<ListOperatorQuery, Result>
    {
        private readonly IMapper _mapper;
        private readonly IOperatorReadRepository _operatorReadRepository;
        private readonly IOperatorOdataProvider _operatorOdataProvider;

        public ListOperatorQueryHandler(IMapper mapper,
                                        IOperatorReadRepository operatorReadRepository,
                                        IOperatorOdataProvider operatorOdataProvider)
        {
            _mapper = mapper ?? throw Error.ArgumentNull(nameof(mapper));
            _operatorReadRepository = operatorReadRepository ?? throw Error.ArgumentNull(nameof(operatorReadRepository));
            _operatorOdataProvider = operatorOdataProvider ?? throw Error.ArgumentNull(nameof(operatorOdataProvider));
        }

        public async Task<Result> Handle(ListOperatorQuery request, CancellationToken cancellationToken)
        {
            Result result;

            try
            {
                Expression<Func<Operator, bool>> filter = _operatorOdataProvider.GetFilterPredicate(request.Filter);
                int? top = request.Top.ToNullableInt();
                int? skip = request.Skip.ToNullableInt();

                var operatorDomains = await _operatorReadRepository.ListAsync(filter, skip, top);
                var operatorModels = _mapper.Map<IEnumerable<OperatorModel>>(operatorDomains);
                var count = operatorModels.Count();
                var operatorsModel = new OperatorsModel { Value = operatorModels, Count = count, NextLink = null };

                return Result.Ok(operatorsModel);
            }
            catch (FilterODataException)
            {
                result = Result.Fail(new List<Failure>()
                {
                    new HandlerFault(){
                        Code = HandlerFaultCode.InvalidQueryFilter.Name,
                        Message = HandlerFailures.InvalidQueryFilter,
                        Target = "$filter"
                    }
                });
            }
            catch
            {
                result = Result.Fail(CustomFailures.ListOperatorFailure);
            }

            return result;
        }
    }
}
