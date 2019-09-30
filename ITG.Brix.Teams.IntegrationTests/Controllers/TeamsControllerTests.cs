using FluentAssertions;
using ITG.Brix.Teams.API.Context.Bases;
using ITG.Brix.Teams.API.Context.Constants;
using ITG.Brix.Teams.API.Context.Resources;
using ITG.Brix.Teams.API.Context.Services.Requests.Models.From;
using ITG.Brix.Teams.API.Context.Services.Responses.Models.Errors;
using ITG.Brix.Teams.Application.Bases;
using ITG.Brix.Teams.Application.Cqs.Queries.Models;
using ITG.Brix.Teams.Application.Resources;
using ITG.Brix.Teams.IntegrationTests.Bases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ITG.Brix.Teams.IntegrationTests.Controllers
{
    [TestClass]
    [TestCategory("Integration")]
    public class TeamsControllerTests
    {
        private HttpClient _client;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            ControllerTestsHelper.InitServer();
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
        }

        [TestInitialize]
        public void TestInitialize()
        {
            DatabaseHelper.Init("Teams");
            DatabaseHelper.Init("Operators");
            _client = ControllerTestsHelper.GetClient();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _client.Dispose();
        }

        [TestMethod]
        public async Task ListAllShouldSucceed()
        {
            // Arrange
            var apiVersion = "1.0";
            await ControllerHelper.CreateTeam("GetAllTest");

            // Act
            var response = await _client.GetAsync(string.Format("api/teams?api-version={0}", apiVersion));
            var responseAsString = await response.Content.ReadAsStringAsync();
            var teamsModel = JsonConvert.DeserializeObject<TeamsModel>(responseAsString);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            teamsModel.Value.Should().HaveCount(1);
            teamsModel.Count.Should().Be(1);
            teamsModel.NextLink.Should().BeNull();
        }

        [DataTestMethod]
        [DataRow("name ne 'Namea'", 25)]
        [DataRow("name eq 'Namea'", 1)]
        [DataRow("name ne 'Namea'", 25)]
        [DataRow("startswith(name, 'Nam') eq true", 26)]
        [DataRow("startswith(name, 'Hello') eq false", 26)]
        [DataRow("startswith(name, 'Nam') eq true and endswith(name, 'z') eq true", 1)]
        [DataRow("endswith(name, 'z') eq true", 1)]
        [DataRow("endswith(name, 'z') eq false", 25)]
        [DataRow("substringof('amea', name) eq true", 1)]
        [DataRow("tolower(name) eq 'namea'", 1)]
        [DataRow("toupper(name) eq 'NAMEA'", 1)]
        public async Task ListWithFilterShouldSucceed(string filter, int countResult)
        {
            // Arrange
            var name = "Name";

            var apiVersion = "1.0";

            string alphabet = "abcdefghijklmnopqrstuvwxyz";

            foreach (char c in alphabet)
            {
                await ControllerHelper.CreateTeam(name + c);
            }

            // Act
            var response = await _client.GetAsync(string.Format("api/teams?api-version={0}&$filter={1}", apiVersion, filter));
            var responseBody = await response.Content.ReadAsStringAsync();
            var teamsModel = JsonConvert.DeserializeObject<TeamsModel>(responseBody);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            teamsModel.Value.Should().HaveCount(countResult);
            teamsModel.Count.Should().Be(countResult);
            teamsModel.NextLink.Should().BeNull();
        }

        [TestMethod]
        public async Task ListWithFilterByMemberShouldSucceed()
        {
            // Arrange
            var teamName = "Name";

            var operatorOneId = Guid.NewGuid();
            var operatorOneLogin = "OperatorOneLogin";
            var operatorOneFirstName = "OperatorOneFirstName";
            var operatorOneLastName = "OperatorOneLastName";

            var operatorTwoId = Guid.NewGuid();
            var operatorTwoLogin = "OperatorTwoLogin";
            var operatorTwoFirstName = "OperatorTwoFirstName";
            var operatorTwoLastName = "OperatorTwoLastName";

            var filter = string.Format("members/id eq '{0}'", operatorTwoId);

            var apiVersion = "1.0";

            await ControllerHelper.CreateOperator(operatorOneId, operatorOneLogin, operatorOneFirstName, operatorOneLastName);
            await ControllerHelper.CreateOperator(operatorTwoId, operatorTwoLogin, operatorTwoFirstName, operatorTwoLastName);
            await ControllerHelper.CreateTeamWithMembers(teamName, new List<Guid> { operatorOneId, operatorTwoId });

            // Act
            var response = await _client.GetAsync(string.Format("api/teams?api-version={0}&$filter={1}", apiVersion, filter));
            var responseBody = await response.Content.ReadAsStringAsync();
            var teamsModel = JsonConvert.DeserializeObject<TeamsModel>(responseBody);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            teamsModel.Value.Should().HaveCount(1);
            teamsModel.Count.Should().Be(1);
            teamsModel.NextLink.Should().BeNull();

            var teamModel = teamsModel.Value.First();
            teamModel.Name.Should().Be(teamName);
            teamModel.Members.Count.Should().Be(2);
            teamModel.Members.Any(x => x.Id == operatorTwoId).Should().BeTrue();
        }

        [TestMethod]
        public async Task ListWithFilterByUnexistentMemberShouldSucceed()
        {
            // Arrange
            var teamName = "Name";

            var operatorOneId = Guid.NewGuid();
            var operatorOneLogin = "OperatorOneLogin";
            var operatorOneFirstName = "OperatorOneFirstName";
            var operatorOneLastName = "OperatorOneLastName";

            var operatorTwoId = Guid.NewGuid();
            var operatorTwoLogin = "OperatorTwoLogin";
            var operatorTwoFirstName = "OperatorTwoFirstName";
            var operatorTwoLastName = "OperatorTwoLastName";

            var unexistingOperatorId = Guid.NewGuid();

            var filter = string.Format("members/id eq '{0}'", unexistingOperatorId);

            var apiVersion = "1.0";

            await ControllerHelper.CreateOperator(operatorOneId, operatorOneLogin, operatorOneFirstName, operatorOneLastName);
            await ControllerHelper.CreateOperator(operatorTwoId, operatorTwoLogin, operatorTwoFirstName, operatorTwoLastName);
            await ControllerHelper.CreateTeamWithMembers(teamName, new List<Guid> { operatorOneId, operatorTwoId });

            // Act
            var response = await _client.GetAsync(string.Format("api/teams?api-version={0}&$filter={1}", apiVersion, filter));
            var responseBody = await response.Content.ReadAsStringAsync();
            var teamsModel = JsonConvert.DeserializeObject<TeamsModel>(responseBody);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            teamsModel.Value.Should().HaveCount(0);
            teamsModel.Count.Should().Be(0);
            teamsModel.NextLink.Should().BeNull();
        }

        [DataTestMethod]
        [DataRow(0, 10, 10)]
        [DataRow(1, 10, 10)]
        [DataRow(10, 100, 16)]
        public async Task ListWithSkipAndTopShouldSucceed(int skip, int top, int count)
        {
            // Arrange
            var teamName = "Team";
            var apiVersion = "1.0";

            for (var i = 0; i < 26; i++)
            {
                await ControllerHelper.CreateTeam($"{teamName}{i}");
            }

            // Assert
            var response = await _client.GetAsync($"api/teams?api-version={apiVersion}&$skip={skip}&$top={top}");
            var responseAsString = await response.Content.ReadAsStringAsync();
            var teams = JsonConvert.DeserializeObject<TeamsModel>(responseAsString);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            teams.Value.Should().HaveCount(count);
            teams.NextLink.Should().BeNull();
        }

        [DataTestMethod]
        [DataRow("startswith(name, 'Team')", 0, 10, 10)]
        [DataRow("startswith(name, 'Team')", 1, 10, 10)]
        public async Task ListWithFilterSkipAndTopShouldSucceed(string filter, int skip, int top, int count)
        {
            // Arrange
            var teamName = "Team";
            var apiVersion = "1.0";

            for (var i = 0; i < 26; i++)
            {
                await ControllerHelper.CreateTeam($"{teamName}{i}");
            }

            // Assert
            var response = await _client.GetAsync($"api/teams?api-version={apiVersion}&$skip={skip}&$top={top}");
            var responseAsString = await response.Content.ReadAsStringAsync();
            var teams = JsonConvert.DeserializeObject<TeamsModel>(responseAsString);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            teams.Value.Should().HaveCount(count);
            teams.NextLink.Should().BeNull();
        }


        [DataTestMethod]
        // NotValid
        [DataRow("test", DisplayName = "InvalidSyntax - 'test'")]
        [DataRow("members#id eq 3", DisplayName = "InvalidSyntax - 'members#id eq 3'")]
        // UnsupportedFunction
        [DataRow("try(name) eq 'Name'", DisplayName = "UnsupportedFunction - try")]
        [DataRow("concat(concat(name, ', '), name) eq 'Name, Name'", DisplayName = "UnsupportedFunction - concat")]
        [DataRow("length(name) eq 9", DisplayName = "UnsupportedFunction - length")]
        [DataRow("replace(name, ' ', '') eq 'Name'", DisplayName = "UnsupportedFunction - replace")]
        [DataRow("trim(name) eq 'Name'", DisplayName = "UnsupportedFunction - trim")]
        // DissalowedLiteral
        [DataRow("startswith(version, 'Text')", DisplayName = "DissalowedLiteral - version")]
        [DataRow("startswith(description, 'Text')", DisplayName = "DissalowedLiteral - description")]
        [DataRow("startswith(image, 'Text')", DisplayName = "DissalowedLiteral - image")]
        [DataRow("startswith(layout, 'Text')", DisplayName = "DissalowedLiteral - layout")]
        [DataRow("startswith(filterContent, 'Text')", DisplayName = "DissalowedLiteral - filterContent")]
        public async Task ListShouldFailWhenQueryFilterNotValid(string filter)
        {
            // Arrange
            var apiVersion = "1.0";

            var expectedError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.InvalidQueryParameterValue.Code,
                    Message = ServiceError.InvalidQueryParameterValue.Message,
                    Details = new List<ResponseErrorField>
                    {
                        new ResponseErrorField
                        {
                            Code = Consts.Failure.Detail.Code.InvalidQueryFilter,
                            Message = HandlerFailures.InvalidQueryFilter,
                            Target = Consts.Failure.Detail.Target.QueryFilter
                        }
                    }
                }
            };

            // Act
            var response = await _client.GetAsync(string.Format("api/teams?api-version={0}&$filter={1}", apiVersion, filter));
            var responseAsString = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(responseAsString);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            error.Should().Be(expectedError);
        }

        [DataTestMethod]
        [DataRow("")]
        [DataRow("  ")]
        public async Task ListShouldSucceedWhenQueryTopIsPressentButUnset(string top)
        {
            // Arrange
            var apiVersion = "1.0";

            // Act
            var response = await _client.GetAsync($"api/teams?api-version={apiVersion}&$top={top}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [DataTestMethod]
        [DataRow("invalid sequesnce")]
        [DataRow("null")]
        public async Task ListShouldFailWhenQueryTopIsNotValid(string top)
        {
            // Arrange
            var apiVersion = "1.0";

            var expectedError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.InvalidQueryParameterValue.Code,
                    Message = ServiceError.InvalidQueryParameterValue.Message,
                    Details = new List<ResponseErrorField>
                            {
                                new ResponseErrorField
                                {
                                    Code = Consts.Failure.Detail.Code.InvalidQueryTop,
                                    Message = CustomFailures.TopInvalid,
                                    Target = Consts.Failure.Detail.Target.QueryTop
                                }
                            }
                }
            };

            // Act
            var response = await _client.GetAsync($"api/teams?api-version={apiVersion}&$top={top}");
            var responseAsString = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(responseAsString);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            error.Should().Be(expectedError);
        }

        [DataTestMethod]
        [DataRow("0")]
        [DataRow("-1")]
        [DataRow("9999999999999")]
        public async Task ListShouldFailWhenQueryTopNotInRange(string top)
        {
            // Arrange
            var apiVersion = "1.0";

            var expectedError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.InvalidQueryParameterValue.Code,
                    Message = ServiceError.InvalidQueryParameterValue.Message,
                    Details = new List<ResponseErrorField>
                            {
                                new ResponseErrorField
                                {
                                    Code = Consts.Failure.Detail.Code.InvalidQueryTop,
                                    Message = string.Format(CustomFailures.TopRange, Application.Constants.Consts.CqsValidation.TopMaxValue),
                                    Target = Consts.Failure.Detail.Target.QueryTop
                                }
                            }
                }
            };

            // Act
            var response = await _client.GetAsync($"api/teams?api-version={apiVersion}&$top={top}");
            var responseAsString = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(responseAsString);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            error.Should().Be(expectedError);
        }

        [DataTestMethod]
        [DataRow("")]
        [DataRow("    ")]
        public async Task ListShouldSucceedWhenQuerySkipIsPresentButUnset(string skip)
        {
            // Arrange
            var apiVersion = "1.0";

            // Act
            var response = await _client.GetAsync($"api/teams?api-version={apiVersion}&$skip={skip}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [DataTestMethod]
        [DataRow("invalid skip")]
        [DataRow("null")]
        public async Task ListShouldFailWhenQuerySkipISnotValid(string skip)
        {
            // Arrange
            var apiVersion = "1.0";

            var expectedError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.InvalidQueryParameterValue.Code,
                    Message = ServiceError.InvalidQueryParameterValue.Message,
                    Details = new List<ResponseErrorField>
                            {
                                new ResponseErrorField
                                {
                                    Code = Consts.Failure.Detail.Code.InvalidQuerySkip,
                                    Message = CustomFailures.SkipInvalid,
                                    Target = Consts.Failure.Detail.Target.QuerySkip
                                }
                            }
                }
            };

            // Act
            var response = await _client.GetAsync($"api/teams?api-version={apiVersion}&$skip={skip}");
            var responseAsString = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(responseAsString);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            error.Should().Be(expectedError);
        }

        [DataTestMethod]
        [DataRow("9999999999999999999")]
        [DataRow("-1")]
        public async Task ListShouldFAilWhenQuerySkipIsNotInRange(string skip)
        {
            // Arrange
            var apiVersion = "1.0";

            var expectedError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.InvalidQueryParameterValue.Code,
                    Message = ServiceError.InvalidQueryParameterValue.Message,
                    Details = new List<ResponseErrorField>
                            {
                                new ResponseErrorField
                                {
                                    Code = Consts.Failure.Detail.Code.InvalidQuerySkip,
                                    Message = string.Format(CustomFailures.SkipRange, Application.Constants.Consts.CqsValidation.SkipMaxValue),
                                    Target = Consts.Failure.Detail.Target.QuerySkip
                                }
                            }
                }
            };

            // Act
            var response = await _client.GetAsync($"api/teams?api-version={apiVersion}&$skip={skip}");
            var responseAsString = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(responseAsString);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            error.Should().Be(expectedError);
        }

        [TestMethod]
        public async Task ListShouldFailWhenQueryApiVersionIsMissing()
        {
            // Arrange
            var expectedError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.MissingRequiredQueryParameter.Code,
                    Message = ServiceError.MissingRequiredQueryParameter.Message,
                    Details = new List<ResponseErrorField>
                    {
                        new ResponseErrorField
                        {
                            Code = Consts.Failure.Detail.Code.Missing,
                            Message = string.Format(RequestFailures.QueryParameterRequired, "api-version"),
                            Target = Consts.Failure.Detail.Target.ApiVersion
                        }
                    }
                }
            };

            // Act
            var response = await _client.GetAsync($"api/teams");
            var responseAsString = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(responseAsString);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            error.Should().Be(expectedError);
        }

        [TestMethod]
        public async Task ListShouldFailWhenQueryApiVersionIsInvalid()
        {
            // Arrange
            var apiVersion = "4.0";

            var expectedError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.InvalidQueryParameterValue.Code,
                    Message = ServiceError.InvalidQueryParameterValue.Message,
                    Details = new List<ResponseErrorField>
                    {
                        new ResponseErrorField
                        {
                            Code = Consts.Failure.Detail.Code.Invalid,
                            Message = string.Format(RequestFailures.QueryParameterInvalidValue, "api-version"),
                            Target = Consts.Failure.Detail.Target.ApiVersion
                        }
                    }
                }
            };

            // Act
            var response = await _client.GetAsync($"api/teams?api-version={apiVersion}");
            var responseAsString = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(responseAsString);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            error.Should().Be(expectedError);
        }


        [TestMethod]
        public async Task GetShouldSucceed()
        {
            // Arrange
            var apiVersion = "1.0";
            var name = "TestIntegration";
            var createdTeam = await ControllerHelper.CreateTeam(name);
            var routeId = createdTeam.Id;
            var eTag = createdTeam.ETag;

            // Act
            var response = await _client.GetAsync(string.Format("api/teams/{0}?api-version={1}", routeId, apiVersion));
            var responseAsString = await response.Content.ReadAsStringAsync();
            var team = JsonConvert.DeserializeObject<TeamModel>(responseAsString);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            response.Headers.ETag.Should().NotBeNull();
            response.Headers.ETag.Tag.Should().Be(eTag);
            team.Name.Should().Be(name);
        }

        [TestMethod]
        public async Task GetShouldFailWhenRouteIdIsInvalid()
        {
            // Arrange
            var apiVersion = "1.0";
            var name = "TestIntegrationRoute";
            await ControllerHelper.CreateTeam(name);
            var routeId = "mock";

            var expectedResponseError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.ResourceNotFound.Code,
                    Message = ServiceError.ResourceNotFound.Message,
                    Details = new List<ResponseErrorField>
                    {
                        new ResponseErrorField
                        {
                            Code = Consts.Failure.Detail.Code.NotFound,
                            Message = string.Format(RequestFailures.EntityNotFoundByIdentifier, "Team"),
                            Target = Consts.Failure.Detail.Target.Id
                        }
                    }
                }
            };

            // Act
            var response = await _client.GetAsync(string.Format("api/teams/{0}?api-version={1}", routeId, apiVersion));
            var responseAsString = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(responseAsString);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            error.Should().Be(expectedResponseError);
        }

        [TestMethod]
        public async Task GetShouldFailWhenRouteIdDoesNotExist()
        {
            // Arrange
            var apiVersion = "1.0";
            var name = "TestIntegrationRoute";
            await ControllerHelper.CreateTeam(name);
            var routeId = Guid.NewGuid();

            var expectedResponseError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.ResourceNotFound.Code,
                    Message = ServiceError.ResourceNotFound.Message,
                    Details = new List<ResponseErrorField>
                    {
                        new ResponseErrorField
                        {
                            Code = Consts.Failure.Detail.Code.NotFound,
                            Message = string.Format(HandlerFailures.NotFound, "Team"),
                            Target = Consts.Failure.Detail.Target.Id
                        }
                    }
                }
            };

            // Act
            var response = await _client.GetAsync(string.Format("api/teams/{0}?api-version={1}", routeId, apiVersion));
            var responseAsString = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(responseAsString);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            error.Should().Be(expectedResponseError);
        }

        [TestMethod]
        public async Task GetShouldFailWhenQueryApiVersionIsMissing()
        {
            // Arrange
            var name = "TestIntegrationRoute";
            await ControllerHelper.CreateTeam(name);
            var routeId = Guid.NewGuid();

            var expectedError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.MissingRequiredQueryParameter.Code,
                    Message = ServiceError.MissingRequiredQueryParameter.Message,
                    Details = new List<ResponseErrorField>
                    {
                        new ResponseErrorField
                        {
                            Code = Consts.Failure.Detail.Code.Missing,
                            Message = string.Format(RequestFailures.QueryParameterRequired, "api-version"),
                            Target = Consts.Failure.Detail.Target.ApiVersion
                        }
                    }
                }
            };

            // Act
            var response = await _client.GetAsync(string.Format("api/teams/{0}", routeId));
            var responseAsString = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(responseAsString);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            error.Should().Be(expectedError);
        }

        [TestMethod]
        public async Task GetShouldFailWhenQueryApiVersionIsInvalid()
        {
            // Arrange
            var version = "4.0";
            var name = "TestIntegrationRoute";
            var createdTeam = await ControllerHelper.CreateTeam(name);
            var routeId = createdTeam.Id;

            var expectedError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.InvalidQueryParameterValue.Code,
                    Message = ServiceError.InvalidQueryParameterValue.Message,
                    Details = new List<ResponseErrorField>
                    {
                        new ResponseErrorField
                        {
                            Code = Consts.Failure.Detail.Code.Invalid,
                            Message = string.Format(RequestFailures.QueryParameterInvalidValue, "api-version"),
                            Target = Consts.Failure.Detail.Target.ApiVersion
                        }
                    }
                }
            };

            // Act
            var response = await _client.GetAsync($"api/teams/{routeId}?api-version={version}");
            var responseAsString = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(responseAsString);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            error.Should().Be(expectedError);
        }

        [TestMethod]
        public async Task GetShouldFailWhenRouteIdIsEmptyGuid()
        {
            // Arrange
            var version = "1.0";
            var name = "TestIntegrationRoute";
            await ControllerHelper.CreateTeam(name);
            var routeId = Guid.Empty;

            var expectedError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.ResourceNotFound.Code,
                    Message = ServiceError.ResourceNotFound.Message,
                    Details = new List<ResponseErrorField>
                    {
                        new ResponseErrorField
                        {
                            Code = Consts.Failure.Detail.Code.NotFound,
                            Message = CustomFailures.TeamNotFound,
                            Target = Consts.Failure.Detail.Target.Id
                        }
                    }
                }
            };

            // Act
            var response = await _client.GetAsync($"api/teams/{routeId}?api-version={version}");
            var responseAsString = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(responseAsString);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            error.Should().Be(expectedError);
        }

        [TestMethod]
        public async Task CreateShouldSucceed()
        {
            // Arrange
            var name = "NameFromTest";
            var image = "Test";
            var description = "Description";
            var layout = Guid.NewGuid().ToString();
            var apiVersion = "1.0";
            var body = new CreateTeamFromBody
            {
                Name = name,
                Description = description,
                Image = image,
                Layout = layout
            };
            var jsonBody = JsonConvert.SerializeObject(body);

            // Act
            var response = await _client.PostAsync($"api/teams?api-version={apiVersion}", new StringContent(jsonBody, Encoding.UTF8, "application/json"));
            var responseAsString = await response.Content.ReadAsStringAsync();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            response.Headers.Location.Should().NotBeNull();
            response.Headers.ETag.Should().NotBeNull();
            responseAsString.Should().BeNullOrEmpty();
        }

        [TestMethod]
        public async Task CreateShouldSucceedOnlyWithName()
        {
            // Arrange
            var name = "OnlyName";
            var apiVersion = "1.0";
            var body = new CreateTeamFromBody
            {
                Name = name
            };
            var jsonBody = JsonConvert.SerializeObject(body);

            // Act
            var response = await _client.PostAsync($"api/teams?api-version={apiVersion}", new StringContent(jsonBody, Encoding.UTF8, "application/json"));
            var responseAsString = await response.Content.ReadAsStringAsync();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            response.Headers.Location.Should().NotBeNull();
            response.Headers.ETag.Should().NotBeNull();
            responseAsString.Should().BeNullOrEmpty();
        }

        [TestMethod]
        public async Task CreateShouldFailWhenContentTypeIsNonJson()
        {
            // Arrange
            var name = "OnlyName";
            var apiVersion = "1.0";
            var body = new CreateTeamFromBody
            {
                Name = name
            };
            var jsonBody = JsonConvert.SerializeObject(body);

            // Act
            var response = await _client.PostAsync($"api/teams?api-version={apiVersion}", new StringContent(jsonBody, Encoding.UTF8, "text/plain"));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.UnsupportedMediaType);
        }

        [TestMethod]
        public async Task CreateShouldFailWhenQueriApiVersionIsMissing()
        {
            // Arrange
            var name = "OnlyName";
            var body = new CreateTeamFromBody
            {
                Name = name
            };
            var jsonBody = JsonConvert.SerializeObject(body);

            var expectedError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.MissingRequiredQueryParameter.Code,
                    Message = ServiceError.MissingRequiredQueryParameter.Message,
                    Details = new List<ResponseErrorField>
                    {
                        new ResponseErrorField
                        {
                            Code = Consts.Failure.Detail.Code.Missing,
                            Message = string.Format(RequestFailures.QueryParameterRequired, "api-version"),
                            Target = Consts.Failure.Detail.Target.ApiVersion
                        }
                    }
                }
            };

            // Act
            var response = await _client.PostAsync($"api/teams", new StringContent(jsonBody, Encoding.UTF8, "application/json"));
            var responseAsString = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(responseAsString);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            error.Should().Be(expectedError);
        }

        [TestMethod]
        public async Task CreateShouldFailWhenQueryApiVersionIsInvalid()
        {
            // Arrange
            var name = "OnlyName";
            var apiVersion = "4.0";
            var body = new CreateTeamFromBody
            {
                Name = name
            };
            var jsonBody = JsonConvert.SerializeObject(body);

            var expectedError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.InvalidQueryParameterValue.Code,
                    Message = ServiceError.InvalidQueryParameterValue.Message,
                    Details = new List<ResponseErrorField>
                    {
                        new ResponseErrorField
                        {
                            Code = Consts.Failure.Detail.Code.Invalid,
                            Message = string.Format(RequestFailures.QueryParameterInvalidValue, "api-version"),
                            Target = Consts.Failure.Detail.Target.ApiVersion
                        }
                    }
                }
            };

            // Act
            var response = await _client.PostAsync($"api/teams?api-version={apiVersion}", new StringContent(jsonBody, Encoding.UTF8, "application/json"));
            var responseAsString = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(responseAsString);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            error.Should().Be(expectedError);
        }

        [TestMethod]
        public async Task CreateShouldFailWhenNameIsNull()
        {
            // Arrange
            string name = null;
            var image = "Test";
            var description = "Description";
            var layout = Guid.NewGuid().ToString();
            var apiVersion = "1.0";
            var body = new CreateTeamFromBody
            {
                Name = name,
                Description = description,
                Image = image,
                Layout = layout
            };
            var jsonBody = JsonConvert.SerializeObject(body);

            var expectedError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.InvalidInput.Code,
                    Message = ServiceError.InvalidInput.Message,
                    Details = new List<ResponseErrorField>
                    {
                        new ResponseErrorField
                        {
                            Code = "invalid-input",
                            Message = ValidationFailures.TeamNameMandatory,
                            Target = "name"
                        }
                    }
                }
            };

            // Act
            var response = await _client.PostAsync($"api/teams?api-version={apiVersion}", new StringContent(jsonBody, Encoding.UTF8, "application/json"));
            var responseAsString = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(responseAsString);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            error.Should().Be(expectedError);
        }


        [TestMethod]
        public async Task UpdateShouldSucceed()
        {
            // Arrange
            var apiVersion = "1.0";
            var member = Guid.NewGuid();
            var memberForUpdate = Guid.NewGuid();
            List<Guid> memberlist = new List<Guid>() { member, memberForUpdate };

            var createdTeam = await ControllerHelper.CreateTeam("TeamFromUpdate");
            var routeId = createdTeam.Id;
            var ifMatch = createdTeam.ETag;
            var updateTeam = new UpdateTeamFromBody
            {
                Name = "UpdateFromIntegrationTest",
                Description = "Update",
                Layout = Guid.NewGuid().ToString(),
                DriverWait = "No",
                FilterContent = "{site:4325345325}",
                Members = memberlist
            };
            var jsonString = JsonConvert.SerializeObject(updateTeam);

            // Act
            _client.DefaultRequestHeaders.Add("If-Match", ifMatch);
            var response = await _client.PutAsync(string.Format("api/teams/{0}?api-version={1}", routeId, apiVersion), new StringContent(jsonString, Encoding.UTF8, "application/json"));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
            response.Headers.ETag.Should().NotBeNull();
        }

        [TestMethod]
        public async Task UpdateShouldFailWhenBodyIsNonJsonContentType()
        {
            // Arrange
            var apiVersion = "1.0";
            var createdTeam = await ControllerHelper.CreateTeam("NoContent");
            var routeId = createdTeam.Id;
            var ifMatch = createdTeam.ETag;
            var updateTeam = new UpdateTeamFromBody
            {
                Name = "UpdateFromIntegrationTest",
                Description = "Update",
                Layout = Guid.NewGuid().ToString(),
                DriverWait = "No"
            };
            var jsonString = JsonConvert.SerializeObject(updateTeam);

            var expectedResponseError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.UnsupportedMediaType.Code,
                    Message = ServiceError.UnsupportedMediaType.Message,
                    Details = new List<ResponseErrorField>
                    {
                        new ResponseErrorField
                        {
                            Code = Consts.Failure.Detail.Code.Unsupported,
                            Message = string.Format(RequestFailures.HeaderUnsupportedValue, "Content-Type"),
                            Target = Consts.Failure.Detail.Target.ContentType
                        }
                    }
                }
            };

            // Act
            _client.DefaultRequestHeaders.Add("If-Match", ifMatch);
            var response = await _client.PutAsync(string.Format("api/teams/{0}?api-version={1}", routeId, apiVersion), new StringContent(jsonString, Encoding.UTF8, "text/plain"));
            var responseAsString = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(responseAsString);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.UnsupportedMediaType);
            error.Should().Be(expectedResponseError);
        }

        [TestMethod]
        public async Task UpdateShouldFailWhenRouteIdIsEmptyGuid()
        {
            // Arrange
            var apiVersion = "1.0";
            var createdTeam = await ControllerHelper.CreateTeam("NoContent");
            var routeId = Guid.Empty;
            var ifMatch = createdTeam.ETag;
            var updateTeam = new UpdateTeamFromBody
            {
                Name = "UpdateFromIntegrationTest",
                Description = "Update",
                Layout = Guid.NewGuid().ToString(),
                DriverWait = "No"
            };
            var jsonString = JsonConvert.SerializeObject(updateTeam);

            var expectedError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.ResourceNotFound.Code,
                    Message = ServiceError.ResourceNotFound.Message,
                    Details = new List<ResponseErrorField>
                    {
                        new ResponseErrorField
                        {
                            Code = Consts.Failure.Detail.Code.NotFound,
                            Message = CustomFailures.TeamNotFound,
                            Target = Consts.Failure.Detail.Target.Id
                        }
                    }
                }
            };

            // Act
            _client.DefaultRequestHeaders.Add("If-Match", ifMatch);
            var response = await _client.PutAsync(string.Format("api/teams/{0}?api-version={1}", routeId, apiVersion), new StringContent(jsonString, Encoding.UTF8, "application/json"));
            var responseAsString = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(responseAsString);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            error.Should().Be(expectedError);
        }

        [TestMethod]
        public async Task UpdateShouldFailWhenRouteIdDoesNotExist()
        {
            // Arrange
            var apiVersion = "1.0";
            var createdTeam = await ControllerHelper.CreateTeam("NoContent");
            var routeId = Guid.NewGuid();
            var ifMatch = createdTeam.ETag;
            var updateTeam = new UpdateTeamFromBody
            {
                Name = "UpdateFromIntegrationTest",
                Description = "Update",
                Layout = Guid.NewGuid().ToString(),
                DriverWait = "No"
            };
            var jsonString = JsonConvert.SerializeObject(updateTeam);

            var expectedError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.ResourceNotFound.Code,
                    Message = ServiceError.ResourceNotFound.Message,
                    Details = new List<ResponseErrorField>
                    {
                        new ResponseErrorField
                        {
                            Code = HandlerFaultCode.NotFound.Name,
                            Message = HandlerFailures.TeamNotFound,
                            Target = Consts.Failure.Detail.Target.Id
                        }
                    }
                }
            };

            // Act
            _client.DefaultRequestHeaders.Add("If-Match", ifMatch);
            var response = await _client.PutAsync(string.Format("api/teams/{0}?api-version={1}", routeId, apiVersion), new StringContent(jsonString, Encoding.UTF8, "application/json"));
            var responseAsString = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(responseAsString);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            error.Should().Be(expectedError);
        }

        [TestMethod]
        public async Task UpdateShouldFailWhenRouteIdIsInvalid()
        {
            // Arrange
            var apiVersion = "1.0";
            var createdTeam = await ControllerHelper.CreateTeam("GuidEmpty");
            var routeId = "TestGuid";
            var ifMatch = createdTeam.ETag;
            var updateTeam = new UpdateTeamFromBody
            {
                Name = "UpdateGuidEmpty",
                Description = "Update",
                Layout = Guid.NewGuid().ToString(),
                DriverWait = "No"
            };
            var jsonString = JsonConvert.SerializeObject(updateTeam);

            var expectedResponseError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.ResourceNotFound.Code,
                    Message = ServiceError.ResourceNotFound.Message,
                    Details = new List<ResponseErrorField>
                    {
                        new ResponseErrorField
                        {
                            Code = Consts.Failure.Detail.Code.NotFound,
                            Message = string.Format(RequestFailures.EntityNotFoundByIdentifier,"Team"),
                            Target = Consts.Failure.Detail.Target.Id
                        }
                    }
                }
            };

            // Act
            _client.DefaultRequestHeaders.Add("If-Match", ifMatch);
            var response = await _client.PutAsync(string.Format("api/teams/{0}?api-version={1}", routeId, apiVersion), new StringContent(jsonString, Encoding.UTF8, "application/json"));
            var responseAsString = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(responseAsString);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            error.Should().Be(expectedResponseError);
        }

        [TestMethod]
        public async Task UpdateShouldFailWhenQueryApiVersionIsMissing()
        {
            // Arrange
            var createdTeam = await ControllerHelper.CreateTeam("TeamFromUpdate");
            var routeId = createdTeam.Id;
            var ifMatch = createdTeam.ETag;
            var updateTeam = new UpdateTeamFromBody
            {
                Name = "UpdateFromIntegrationTest",
                Description = "Update",
                Layout = Guid.NewGuid().ToString(),
                DriverWait = "No"
            };
            var jsonString = JsonConvert.SerializeObject(updateTeam);

            var expectedError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.MissingRequiredQueryParameter.Code,
                    Message = ServiceError.MissingRequiredQueryParameter.Message,
                    Details = new List<ResponseErrorField>
                    {
                        new ResponseErrorField
                        {
                            Code = Consts.Failure.Detail.Code.Missing,
                            Message = string.Format(RequestFailures.QueryParameterRequired, "api-version"),
                            Target = Consts.Failure.Detail.Target.ApiVersion
                        }
                    }
                }
            };

            // Act
            _client.DefaultRequestHeaders.Add("If-Match", ifMatch);
            var response = await _client.PutAsync($"api/teams/{routeId}", new StringContent(jsonString, Encoding.UTF8, "application/json"));
            var responseAsString = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(responseAsString);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            error.Should().Be(expectedError);
        }

        [TestMethod]
        public async Task UpdateShouldFailWhenQueryApiVersionIsInvalid()
        {
            // Arrange
            var apiVersion = "4.0";
            var createdTeam = await ControllerHelper.CreateTeam("TeamFromUpdate");
            var routeId = createdTeam.Id;
            var ifMatch = createdTeam.ETag;
            var updateTeam = new UpdateTeamFromBody
            {
                Name = "UpdateFromIntegrationTest",
                Description = "Update",
                Layout = Guid.NewGuid().ToString(),
                DriverWait = "No"
            };
            var jsonString = JsonConvert.SerializeObject(updateTeam);
            var expectedError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.InvalidQueryParameterValue.Code,
                    Message = ServiceError.InvalidQueryParameterValue.Message,
                    Details = new List<ResponseErrorField>
                    {
                        new ResponseErrorField
                        {
                            Code = Consts.Failure.Detail.Code.Invalid,
                            Message = string.Format(RequestFailures.QueryParameterInvalidValue, "api-version"),
                            Target = Consts.Failure.Detail.Target.ApiVersion
                        }
                    }
                }
            };

            // Act
            _client.DefaultRequestHeaders.Add("If-Match", ifMatch);
            var response = await _client.PutAsync(string.Format("api/teams/{0}?api-version={1}", routeId, apiVersion), new StringContent(jsonString, Encoding.UTF8, "application/json"));
            var responseAsString = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(responseAsString);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            error.Should().Be(expectedError);
        }

        [TestMethod]
        public async Task UpdateShouldFailWhenHeaderIfMatchIsMissing()
        {
            // Arrange
            var apiVersion = "1.0";
            var createdTeam = await ControllerHelper.CreateTeam("TeamFromUpdate");
            var routeId = createdTeam.Id;
            var ifMatch = createdTeam.ETag;
            var updateTeam = new UpdateTeamFromBody
            {
                Name = "UpdateFromIntegrationTest",
                Description = "Update",
                Layout = Guid.NewGuid().ToString(),
                DriverWait = "No"
            };
            var jsonString = JsonConvert.SerializeObject(updateTeam);

            var expectedError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.MissingRequiredHeader.Code,
                    Message = ServiceError.MissingRequiredHeader.Message,
                    Details = new List<ResponseErrorField>
                    {
                        new ResponseErrorField
                        {
                            Code = Consts.Failure.Detail.Code.Missing,
                            Message = string.Format(RequestFailures.HeaderRequired,"If-Match"),
                            Target = Consts.Failure.Detail.Target.IfMatch
                        }
                    }
                }
            };

            // Act
            var response = await _client.PutAsync(string.Format("api/teams/{0}?api-version={1}", routeId, apiVersion), new StringContent(jsonString, Encoding.UTF8, "application/json"));
            var responseAsString = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(responseAsString);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            error.Should().Be(expectedError);
        }

        [TestMethod]
        public async Task UpdateShouldFailWhenHeaderIfMatchIsWrong()
        {
            // Arrange
            var apiVersion = "1.0";
            var createdTeam = await ControllerHelper.CreateTeam("TeamFromUpdate");
            var routeId = createdTeam.Id;
            var ifMatch = "\"43523\"";
            var updateTeam = new UpdateTeamFromBody
            {
                Name = "UpdateFromIntegrationTest",
                Description = "Update",
                Layout = Guid.NewGuid().ToString(),
                DriverWait = "No"
            };
            var jsonString = JsonConvert.SerializeObject(updateTeam);

            var expectedError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.ConditionNotMet.Code,
                    Message = ServiceError.ConditionNotMet.Message,
                    Details = new List<ResponseErrorField>
                    {
                        new ResponseErrorField
                        {
                            Code = HandlerFaultCode.NotMet.Name,
                            Message = HandlerFailures.NotMet,
                            Target = Consts.Failure.Detail.Target.IfMatch
                        }
                    }
                }
            };

            // Act
            _client.DefaultRequestHeaders.Add("If-Match", ifMatch);
            var response = await _client.PutAsync(string.Format("api/teams/{0}?api-version={1}", routeId, apiVersion), new StringContent(jsonString, Encoding.UTF8, "application/json"));
            var responseAsString = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(responseAsString);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.PreconditionFailed);
            error.Should().Be(expectedError);
        }

        [TestMethod]
        public async Task DeleteShouldSucceed()
        {
            // Arrange
            var apiVersion = "1.0";
            var createdTeam = await ControllerHelper.CreateTeam("TeamFromUpdate");
            var routeId = createdTeam.Id;
            var ifMatch = createdTeam.ETag;

            // Act
            _client.DefaultRequestHeaders.Add("If-Match", ifMatch);
            var response = await _client.DeleteAsync(string.Format("api/teams/{0}?api-version={1}", routeId, apiVersion));
            var responseAsString = await response.Content.ReadAsStringAsync();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
            responseAsString.Should().BeNullOrEmpty();
        }

        [TestMethod]
        public async Task DeleteShouldFailWhenRouteIdIsInvalid()
        {
            // Arrange
            var apiVersion = "1.0";
            var createdTeam = await ControllerHelper.CreateTeam("TeamFromUpdate");
            var routeId = "TestRouteId";
            var ifMatch = createdTeam.ETag;

            var expectedResponseError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.ResourceNotFound.Code,
                    Message = ServiceError.ResourceNotFound.Message,
                    Details = new List<ResponseErrorField>
                    {
                        new ResponseErrorField
                        {
                            Code = Consts.Failure.Detail.Code.NotFound,
                            Message = string.Format(RequestFailures.EntityNotFoundByIdentifier,"Team"),
                            Target = Consts.Failure.Detail.Target.Id
                        }
                    }
                }
            };

            // Act
            _client.DefaultRequestHeaders.Add("If-Match", ifMatch);
            var response = await _client.DeleteAsync($"api/teams/{routeId}?api-version={apiVersion}");
            var responseAsString = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(responseAsString);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            responseAsString.Should().NotBeNull();
            error.Should().Be(expectedResponseError);
        }

        [TestMethod]
        public async Task DeleteShouldFailWhenRouteIdIsEmptyGuid()
        {
            // Arrange
            var apiVersion = "1.0";
            var createdTeam = await ControllerHelper.CreateTeam("TeamFromUpdate");
            var routeId = Guid.Empty;
            var ifMatch = createdTeam.ETag;

            var expectedError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.ResourceNotFound.Code,
                    Message = ServiceError.ResourceNotFound.Message,
                    Details = new List<ResponseErrorField>
                    {
                        new ResponseErrorField
                        {
                            Code = Consts.Failure.Detail.Code.NotFound,
                            Message = CustomFailures.TeamNotFound,
                            Target = Consts.Failure.Detail.Target.Id
                        }
                    }
                }
            };

            // Act
            _client.DefaultRequestHeaders.Add("If-Match", ifMatch);
            var response = await _client.DeleteAsync(string.Format("api/teams/{0}?api-version={1}", routeId, apiVersion));
            var responseAsString = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(responseAsString);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            responseAsString.Should().NotBeNull();
            error.Should().Be(expectedError);
        }

        [TestMethod]
        public async Task DeleteShouldFailWhenRouteIdDoesNotExist()
        {
            // Arrange
            var apiVersion = "1.0";
            var createdTeam = await ControllerHelper.CreateTeam("TeamFromUpdate");
            var routeId = Guid.NewGuid();
            var ifMatch = createdTeam.ETag;

            var expectedResponseError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.ResourceNotFound.Code,
                    Message = ServiceError.ResourceNotFound.Message,
                    Details = new List<ResponseErrorField>
                    {
                        new ResponseErrorField
                        {
                            Code = HandlerFaultCode.NotFound.Name,
                            Message = HandlerFailures.TeamNotFound,
                            Target = Consts.Failure.Detail.Target.Id
                        }
                    }
                }
            };

            // Act
            _client.DefaultRequestHeaders.Add("If-Match", ifMatch);
            var response = await _client.DeleteAsync(string.Format("api/teams/{0}?api-version={1}", routeId, apiVersion));
            var responseAsString = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(responseAsString);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
            responseAsString.Should().NotBeNull();
            error.Should().Be(expectedResponseError);
        }

        [TestMethod]
        public async Task DeleteShouldFailWhenQueryApiVersionIsMissing()
        {
            // Arrange
            var routeId = Guid.NewGuid();

            var expectedError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.MissingRequiredQueryParameter.Code,
                    Message = ServiceError.MissingRequiredQueryParameter.Message,
                    Details = new List<ResponseErrorField>
                    {
                        new ResponseErrorField
                        {
                            Code = Consts.Failure.Detail.Code.Missing,
                            Message = string.Format(RequestFailures.QueryParameterRequired, "api-version"),
                            Target = Consts.Failure.Detail.Target.ApiVersion
                        }
                    }
                }
            };

            // Act
            var response = await _client.DeleteAsync(string.Format("api/teams/{0}", routeId));
            var responseAsString = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(responseAsString);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            responseAsString.Should().NotBeNull();
            error.Should().Be(expectedError);
        }

        [TestMethod]
        public async Task DeleteShouldFailWhenQueryApiVersionIsInvalid()
        {
            // Arrange
            var apiVersion = "4.0";
            var createdTeam = await ControllerHelper.CreateTeam("TeamFromUpdate");
            var routeId = createdTeam.Id;
            var ifMatch = createdTeam.ETag;

            var expectedError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.InvalidQueryParameterValue.Code,
                    Message = ServiceError.InvalidQueryParameterValue.Message,
                    Details = new List<ResponseErrorField>
                    {
                        new ResponseErrorField
                        {
                            Code = Consts.Failure.Detail.Code.Invalid,
                            Message = string.Format(RequestFailures.QueryParameterInvalidValue, "api-version"),
                            Target = Consts.Failure.Detail.Target.ApiVersion
                        }
                    }
                }
            };

            // Act
            _client.DefaultRequestHeaders.Add("If-Match", ifMatch);
            var response = await _client.DeleteAsync($"api/teams/{routeId}?api-version={apiVersion}");
            var responseAsString = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(responseAsString);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            responseAsString.Should().NotBeNull();
            error.Should().Be(expectedError);
        }

        [TestMethod]
        public async Task DeleteShouldFailWhenHeaderIfMatchIsMissing()
        {
            // Arrange
            var apiVersion = "1.0";
            await ControllerHelper.CreateTeam("TeamFromUpdate");
            var routeId = Guid.NewGuid();

            var expectedError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.MissingRequiredHeader.Code,
                    Message = ServiceError.MissingRequiredHeader.Message,
                    Details = new List<ResponseErrorField>
                    {
                        new ResponseErrorField
                        {
                            Code = Consts.Failure.Detail.Code.Missing,
                            Message = string.Format(RequestFailures.HeaderRequired,"If-Match"),
                            Target = Consts.Failure.Detail.Target.IfMatch
                        }
                    }
                }
            };

            // Act
            var response = await _client.DeleteAsync($"api/teams/{routeId}?api-version={apiVersion}");
            var responseAsString = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(responseAsString);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            responseAsString.Should().NotBeNull();
            error.Should().Be(expectedError);
        }

        [TestMethod]
        public async Task DeleteShouldFailWhenHeaderIfMatchIsWrong()
        {
            // Arrange
            var apiVersion = "1.0";
            var createdTeam = await ControllerHelper.CreateTeam("TeamFromUpdate");
            var routeId = createdTeam.Id;
            var ifMatch = "\"464545\"";

            var expectedError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.ConditionNotMet.Code,
                    Message = ServiceError.ConditionNotMet.Message,
                    Details = new List<ResponseErrorField>
                    {
                        new ResponseErrorField
                        {
                            Code = HandlerFaultCode.NotMet.Name,
                            Message = HandlerFailures.NotMet,
                            Target = Consts.Failure.Detail.Target.IfMatch
                        }
                    }
                }
            };

            // Act
            _client.DefaultRequestHeaders.Add("If-Match", ifMatch);
            var response = await _client.DeleteAsync($"api/teams/{routeId}?api-version={apiVersion}");
            var responseAsString = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(responseAsString);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.PreconditionFailed);
            responseAsString.Should().NotBeNull();
            error.Should().Be(expectedError);
        }

        [TestMethod]
        public async Task DeleteShouldFailWhenHeaderIfMatchIsInvalid()
        {
            // Arrange
            var apiVersion = "1.0";
            await ControllerHelper.CreateTeam("TeamFromUpdate");
            var routeId = Guid.NewGuid();
            var ifMatch = "\"\"";

            var expectedError = new ResponseError
            {
                Error = new ResponseErrorBody
                {
                    Code = ServiceError.InvalidHeaderValue.Code,
                    Message = ServiceError.InvalidHeaderValue.Message,
                    Details = new List<ResponseErrorField>
                    {
                        new ResponseErrorField
                        {
                            Code = Consts.Failure.Detail.Code.Invalid,
                            Message = string.Format(RequestFailures.HeaderInvalidValue, "If-Match"),
                            Target = Consts.Failure.Detail.Target.IfMatch
                        }
                    }
                }
            };

            // Act
            _client.DefaultRequestHeaders.Add("If-Match", ifMatch);
            var response = await _client.DeleteAsync($"api/teams/{routeId}?api-version={apiVersion}");
            var responseAsString = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(responseAsString);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            responseAsString.Should().NotBeNull();
            error.Should().Be(expectedError);
        }
    }
}
