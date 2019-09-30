using FluentAssertions;
using ITG.Brix.Teams.API.Context.Bases;
using ITG.Brix.Teams.API.Context.Constants;
using ITG.Brix.Teams.API.Context.Resources;
using ITG.Brix.Teams.API.Context.Services.Responses.Models.Errors;
using ITG.Brix.Teams.Application.Cqs.Queries.Models;
using ITG.Brix.Teams.Application.Resources;
using ITG.Brix.Teams.IntegrationTests.Bases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ITG.Brix.Teams.IntegrationTests.Controllers
{
    [TestClass]
    [TestCategory("Integration")]
    public class OperatorsControllerTests
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
            var id = Guid.NewGuid();
            var login = "Login";
            var firstName = "FirstName";
            var lastName = "LastName";

            var apiVersion = "1.0";

            await ControllerHelper.CreateOperator(id, login, firstName, lastName);

            // Act
            var response = await _client.GetAsync(string.Format("api/operators?api-version={0}", apiVersion));
            var responseBody = await response.Content.ReadAsStringAsync();
            var operatorsModel = JsonConvert.DeserializeObject<OperatorsModel>(responseBody);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            operatorsModel.Value.Should().HaveCount(1);
            operatorsModel.Count.Should().Be(1);
            operatorsModel.NextLink.Should().BeNull();
        }

        [DataTestMethod]
        [DataRow("login eq 'Logina'", 1)]
        [DataRow("firstName eq 'FirstNamea'", 1)]
        [DataRow("firstName ne 'FirstNamea'", 25)]
        [DataRow("lastName eq 'LastNamea'", 1)]
        [DataRow("lastName ne 'LastNamea'", 25)]
        [DataRow("startswith(firstName, 'First') eq true", 26)]
        [DataRow("startswith(firstName, 'Hello') eq false", 26)]
        [DataRow("startswith(firstName, 'First') eq true and endswith(firstName, 'z') eq true", 1)]
        [DataRow("endswith(firstName, 'z') eq true", 1)]
        [DataRow("endswith(firstName, 'z') eq false", 25)]
        [DataRow("substringof('Namea', firstName) eq true", 1)]
        [DataRow("tolower(firstName) eq 'firstnamea'", 1)]
        [DataRow("toupper(firstName) eq 'FIRSTNAMEA'", 1)]
        public async Task ListWithFilterShouldSucceed(string filter, int countResult)
        {
            // Arrange
            var login = "Login";
            var firstName = "FirstName";
            var lastName = "LastName";

            var apiVersion = "1.0";

            string alphabet = "abcdefghijklmnopqrstuvwxyz";

            foreach (char c in alphabet)
            {
                await ControllerHelper.CreateOperator(Guid.NewGuid(), login + c, firstName + c, lastName + c);
            }


            // Act
            var response = await _client.GetAsync(string.Format("api/operators?api-version={0}&$filter={1}", apiVersion, filter));
            var responseBody = await response.Content.ReadAsStringAsync();
            var operatorsModel = JsonConvert.DeserializeObject<OperatorsModel>(responseBody);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            operatorsModel.Value.Should().HaveCount(countResult);
            operatorsModel.Count.Should().Be(countResult);
            operatorsModel.NextLink.Should().BeNull();
        }

        [DataTestMethod]
        [DataRow(0, 10, 10)]
        [DataRow(1, 10, 10)]
        [DataRow(10, 100, 16)]
        public async Task ListWithSkipAndTopShouldSucceed(int skip, int top, int countResult)
        {
            // Arrange
            var login = "Login";
            var firstName = "FirstName";
            var lastName = "LastName";

            var apiVersion = "1.0";

            string alphabet = "abcdefghijklmnopqrstuvwxyz";

            foreach (char c in alphabet)
            {
                await ControllerHelper.CreateOperator(Guid.NewGuid(), login + c, firstName + c, lastName + c);
            }


            // Act
            var response = await _client.GetAsync(string.Format("api/operators?api-version={0}&$skip={1}&$top={2}", apiVersion, skip, top));
            var responseBody = await response.Content.ReadAsStringAsync();
            var operatorsModel = JsonConvert.DeserializeObject<OperatorsModel>(responseBody);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            operatorsModel.Value.Should().HaveCount(countResult);
            operatorsModel.Count.Should().Be(countResult);
            operatorsModel.NextLink.Should().BeNull();
        }

        [DataTestMethod]
        [DataRow("name eq 'FirstName'")]
        [DataRow("login")]
        public async Task ListShouldFailWhenQueryFilterIsNotValid(string filter)
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
            var response = await _client.GetAsync(string.Format("api/operators?api-version={0}&$filter={1}", apiVersion, filter));
            var body = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(body);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            body.Should().NotBeNull();
            error.Should().Be(expectedError);
        }

        [DataTestMethod]
        [DataRow("concat(concat(firstName, ', '), lastName) eq 'FirstName, LastName'")]
        [DataRow("length(firstName) eq 9")]
        [DataRow("replace(firstName, ' ', '') eq 'FirstName'")]
        [DataRow("trim(firstName) eq 'FirstName'")]
        public async Task ListShouldFailWhenQueryFilterHasUnsupportedFunctions(string filter)
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
            var response = await _client.GetAsync(string.Format("api/operators?api-version={0}&$filter={1}", apiVersion, filter));
            var body = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(body);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            body.Should().NotBeNull();
            error.Should().Be(expectedError);
        }


        [DataTestMethod]
        [DataRow("")]
        [DataRow("    ")]
        public async Task ListShouldSucceedWhenQueryTopPresentButUnset(string top)
        {
            // Arrange
            var apiVersion = "1.0";

            // Act
            var response = await _client.GetAsync(string.Format("api/operators?api-version={0}&$top={1}", apiVersion, top));
            var body = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(body);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [DataTestMethod]
        [DataRow("some invalid value - not a sequence of digits")]
        [DataRow("null")]
        [DataRow("''")]
        [DataRow("'   '")]
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
            var response = await _client.GetAsync(string.Format("api/operators?api-version={0}&$top={1}", apiVersion, top));
            var body = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(body);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            body.Should().NotBeNull();
            error.Should().Be(expectedError);
        }


        [DataTestMethod]
        [DataRow("0")]
        [DataRow("-1")]
        [DataRow("99999999999999999999999")]
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
            var response = await _client.GetAsync(string.Format("api/operators?api-version={0}&$top={1}", apiVersion, top));
            var body = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(body);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            body.Should().NotBeNull();
            error.Should().Be(expectedError);
        }

        [DataTestMethod]
        [DataRow("")]
        [DataRow("    ")]
        public async Task ListShouldSucceedWhenQuerySkipPresentButUnset(string skip)
        {
            // Arrange
            var apiVersion = "1.0";

            // Act
            var response = await _client.GetAsync(string.Format("api/operators?api-version={0}&$skip={1}", apiVersion, skip));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [DataTestMethod]
        [DataRow("some invalid value - not a sequence of digits")]
        [DataRow("null")]
        [DataRow("''")]
        [DataRow("'   '")]
        public async Task ListShouldFailWhenQuerySkipIsNotValid(string skip)
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
            var response = await _client.GetAsync(string.Format("api/operators?api-version={0}&$skip={1}", apiVersion, skip));
            var body = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(body);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            body.Should().NotBeNull();
            error.Should().Be(expectedError);
        }

        [DataTestMethod]
        [DataRow("-1")]
        [DataRow("99999999999999999999999")]
        public async Task ListShouldFailWhenQuerySkipNotInRange(string skip)
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
            var response = await _client.GetAsync(string.Format("api/operators?api-version={0}&$skip={1}", apiVersion, skip));
            var body = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(body);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            body.Should().NotBeNull();
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
            var response = await _client.GetAsync("api/operators");
            var body = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(body);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            body.Should().NotBeNull();
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
            var response = await _client.GetAsync(string.Format("api/operators?api-version={0}", apiVersion));
            var body = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ResponseError>(body);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            body.Should().NotBeNull();
            error.Should().Be(expectedError);
        }

    }
}
