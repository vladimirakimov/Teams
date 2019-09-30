using ITG.Brix.Teams.API.Context.Services.Requests.Models.From;
using ITG.Brix.Teams.IntegrationTests.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ITG.Brix.Teams.IntegrationTests.Bases
{
    public static class ControllerHelper
    {
        private static HttpClient _client = ControllerTestsHelper.GetClient();

        public static async Task<CreatedTeamResult> CreateTeam(string name)
        {
            var apiVersion = "1.0";
            var body = new CreateTeamFromBody
            {
                Name = name
            };
            var jsonBody = JsonConvert.SerializeObject(body);

            var response = await _client.PostAsync($"api/teams?api-version={apiVersion}", new StringContent(jsonBody, Encoding.UTF8, "application/json"));
            var id = response.Headers.Location.GetId();
            var eTag = response.Headers.ETag.Tag;
            var result = new CreatedTeamResult { Id = id, ETag = eTag };

            return result;
        }

        public static async Task CreateTeamWithMembers(string name, List<Guid> members)
        {
            var apiVersion = "1.0";
            var createdTeam = await ControllerHelper.CreateTeam(name);
            var routeId = createdTeam.Id;
            var ifMatch = createdTeam.ETag;
            var updateTeam = new UpdateTeamFromBody
            {
                Put = "dfgfdgfd",
                Name = name,
                Image = null,
                Description = null,
                DriverWait = "Yes",
                Members = members,
                FilterContent = "json"
            };



            var jsonString = JsonConvert.SerializeObject(updateTeam);

            // Act
            _client.DefaultRequestHeaders.Add("If-Match", ifMatch);
            await _client.PutAsync(string.Format("api/teams/{0}?api-version={1}", routeId, apiVersion), new StringContent(jsonString, Encoding.UTF8, "application/json"));

        }

        public static async Task CreateOperator(Guid id, string login, string firstName, string lastName)
        {
            await Task.Yield();
            RepositoryHelper.CreateOperator(id, login, firstName, lastName);
        }
    }
}
