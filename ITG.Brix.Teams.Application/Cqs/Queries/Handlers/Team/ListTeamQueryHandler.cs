using AutoMapper;
using ITG.Brix.Teams.Application.Bases;
using ITG.Brix.Teams.Application.Cqs.Queries.Definitions;
using ITG.Brix.Teams.Application.Cqs.Queries.Models;
using ITG.Brix.Teams.Application.Extensions;
using ITG.Brix.Teams.Application.Internal;
using ITG.Brix.Teams.Application.Resources;
using ITG.Brix.Teams.Domain.Repositories;
using ITG.Brix.Teams.Infrastructure.Exceptions;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ITG.Brix.Teams.Application.Cqs.Queries.Teams.Handlers
{
    public class ListTeamQueryHandler : IRequestHandler<ListTeamQuery, Result>
    {
        private readonly IMapper _mapper;
        private readonly ITeamReadRepository _teamReadRepository;
        private readonly IOperatorReadRepository _operatorReadRepository;

        public ListTeamQueryHandler(IMapper mapper,
                                    ITeamReadRepository teamReadRepository,
                                    IOperatorReadRepository operatorReadRepository)
        {
            _mapper = mapper ?? throw Error.ArgumentNull(nameof(mapper));
            _teamReadRepository = teamReadRepository ?? throw Error.ArgumentNull(nameof(teamReadRepository));
            _operatorReadRepository = operatorReadRepository ?? throw Error.ArgumentNull(nameof(operatorReadRepository));
        }

        public async Task<Result> Handle(ListTeamQuery request, CancellationToken cancellationToken)
        {
            Result result;

            try
            {
                var filter = request.Filter;
                int? skip = request.Skip.ToNullableInt();
                int? top = request.Top.ToNullableInt();
                var teamDomains = await _teamReadRepository.ListAsync(filter, skip, top);
                var teamModels = new List<TeamModel>();

                foreach (var teamDomain in teamDomains)
                {
                    var members = new List<MemberModel>();
                    foreach (var member in teamDomain.Members)
                    {
                        var operatorDomain = await _operatorReadRepository.GetAsync(member);
                        members.Add(_mapper.Map<MemberModel>(operatorDomain));
                    }
                    var teamModel = _mapper.Map<TeamModel>(teamDomain);
                    teamModel.Members = new List<MemberModel>(members);
                    teamModels.Add(teamModel);
                }

                var count = teamModels.Count;
                var teamsModel = new TeamsModel { Value = teamModels, Count = count, NextLink = null };

                result = Result.Ok(teamsModel);
            }
            catch (FilterODataException)
            {
                result = Result.Fail(new List<Failure>() {
                    new HandlerFault(){
                        Code = HandlerFaultCode.InvalidQueryFilter.Name,
                        Message = HandlerFailures.InvalidQueryFilter,
                        Target = "$filter"
                    }
                });
            }
            catch
            {
                result = Result.Fail(CustomFailures.ListTeamFailure);
            }

            return result;
        }
    }
}
