using ITG.Brix.Teams.API.Context.Services.Requests.Models;
using ITG.Brix.Teams.Application.Cqs.Commands.Definitions;
using ITG.Brix.Teams.Application.Cqs.Queries.Definitions;
using System;

namespace ITG.Brix.Teams.API.Context.Services.Requests.Mappers.Impl
{
    public class CqsMapper : ICqsMapper
    {
        public ListTeamQuery Map(ListTeamRequest request)
        {
            var filter = request.Filter;
            var skip = request.Skip;
            var top = request.Top;

            var result = new ListTeamQuery(filter, top, skip);

            return result;
        }

        public GetTeamQuery Map(GetTeamRequest request)
        {
            var id = new Guid(request.RouteId);

            var result = new GetTeamQuery(id);
            return result;
        }

        public CreateTeamCommand Map(CreateTeamRequest request)
        {
            var command = new CreateTeamCommand(request.Name,
                                                request.Image,
                                                request.Description,
                                                request.Layout);
            return command;
        }

        public UpdateTeamCommand Map(UpdateTeamRequest request)
        {
            var version = ToVersion(request.HeaderIfMatch);
            var id = new Guid(request.RouteId);

            var command = new UpdateTeamCommand(id,
                                                request.Name,
                                                request.Image,
                                                request.Description,
                                                request.DriverWait,
                                                request.Layout,
                                                request.Members,
                                                request.FilterContent,
                                                version);


            return command;
        }

        public DeleteTeamCommand Map(DeleteTeamRequest request)
        {
            var id = new Guid(request.RouteId);
            var version = ToVersion(request.HeaderIfMatch);
            var command = new DeleteTeamCommand(id, version);

            return command;
        }

        public ListOperatorQuery Map(ListOperatorRequest request)
        {
            var filter = request.Filter;
            var skip = request.Skip;
            var top = request.Top;

            var query = new ListOperatorQuery(filter, top, skip);

            return query;
        }

        public ListDriverWaitQuery Map(ListDriverWaitRequest request)
        {
            var query = new ListDriverWaitQuery();

            return query;
        }

        private int ToVersion(string eTag)
        {
            var eTagValue = eTag.Replace("\"", "");
            var result = int.Parse(eTagValue);

            return result;
        }
    }
}
