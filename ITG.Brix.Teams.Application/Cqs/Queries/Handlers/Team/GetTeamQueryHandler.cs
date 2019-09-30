using AutoMapper;
using ITG.Brix.Teams.Application.Bases;
using ITG.Brix.Teams.Application.Cqs.Queries.Definitions;
using ITG.Brix.Teams.Application.Cqs.Queries.Models;
using ITG.Brix.Teams.Application.Internal;
using ITG.Brix.Teams.Application.Resources;
using ITG.Brix.Teams.Domain.Repositories;
using ITG.Brix.Teams.Infrastructure.Exceptions;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ITG.Brix.Teams.Application.Cqs.Queries.Handlers
{
    public class GetTeamQueryHandler : IRequestHandler<GetTeamQuery, Result>
    {
        private readonly IMapper _mapper;
        private readonly ITeamReadRepository _teamReadRepository;
        private readonly IOperatorReadRepository _operatorReadRepository;

        public GetTeamQueryHandler(IMapper mapper,
                                   ITeamReadRepository teamReadRepository,
                                   IOperatorReadRepository operatorReadRepository)
        {
            _mapper = mapper ?? throw Error.ArgumentNull(nameof(mapper));
            _teamReadRepository = teamReadRepository ?? throw Error.ArgumentNull(nameof(teamReadRepository));
            _operatorReadRepository = operatorReadRepository ?? throw Error.ArgumentNull(nameof(operatorReadRepository));
        }

        public async Task<Result> Handle(GetTeamQuery query, CancellationToken cancellationToken)
        {
            Result result;

            try
            {
                List<MemberModel> memberModels = new List<MemberModel>();
                var teamDomain = await _teamReadRepository.GetAsync(query.Id);

                foreach (var member in teamDomain.Members)
                {
                    var operatorDomain = await _operatorReadRepository.GetAsync(member);
                    memberModels.Add(_mapper.Map<MemberModel>(operatorDomain));
                }

                var teamModel = _mapper.Map<TeamModel>(teamDomain);
                teamModel.Members = new List<MemberModel>(memberModels);

                result = Result.Ok(teamModel, teamDomain.Version);
            }
            catch (EntityNotFoundDbException)
            {
                result = Result.Fail(new List<Failure>() {
                    new HandlerFault(){
                        Code = HandlerFaultCode.NotFound.Name,
                        Message = HandlerFailures.TeamNotFound,
                        Target = "id"
                    }
                });
            }
            catch
            {
                result = Result.Fail(CustomFailures.GetTeamFailure);
            }

            return result;
        }
    }
}
