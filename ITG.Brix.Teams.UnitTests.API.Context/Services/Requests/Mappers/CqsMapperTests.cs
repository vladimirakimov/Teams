using FluentAssertions;
using ITG.Brix.Teams.API.Context.Services.Requests.Mappers.Impl;
using ITG.Brix.Teams.API.Context.Services.Requests.Models;
using ITG.Brix.Teams.API.Context.Services.Requests.Models.From;
using ITG.Brix.Teams.Application.Cqs.Commands.Definitions;
using ITG.Brix.Teams.Application.Cqs.Queries.Definitions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace ITG.Brix.Teams.UnitTests.API.Context.Services.Requests.Mappers
{
    [TestClass]
    public class CqsMapperTests
    {
        private CqsMapper _cqsMapper;

        [TestInitialize]
        public void TestInialize()
        {
            _cqsMapper = new CqsMapper();
        }

        [TestMethod]
        public void MapListTeamsRequestShouldSucceed()
        {
            // Arrange
            var filter = "?id gt 100";
            var skip = "10";
            var top = "30";
            var apiVersion = "1.0";

            var listTeamsRequest = new ListTeamRequest(new ListTeamFromQuery()
            {
                ApiVersion = apiVersion,
                Filter = filter,
                Skip = skip,
                Top = top
            });

            // Act
            var query = new ListTeamQuery(filter, top, skip);
            var mappedQuery = _cqsMapper.Map(listTeamsRequest);

            // Assert
            mappedQuery.Should().BeEquivalentTo(query);
        }

        [TestMethod]
        public void MapGetTeamRequestShoulsSucceed()
        {
            // Arrange
            var id = Guid.NewGuid().ToString();
            var apiVersion = "1.0";
            var request = new GetTeamRequest(new GetTeamFromRoute() { Id = id }, new GetTeamFromQuery() { ApiVersion = apiVersion });

            // Act
            var query = new GetTeamQuery(new Guid(id));
            var mappedQuery = _cqsMapper.Map(request);

            // Assert
            query.Should().BeEquivalentTo(mappedQuery);
        }

        [TestMethod]
        public void MapCreateTeamRequestShouldSucceed()
        {
            // Arrange
            var name = "Test";
            var image = "Image";
            var description = "Descriptor";
            var layout = Guid.NewGuid().ToString();
            var apiVersion = "1.0";

            // Act
            var request = new CreateTeamRequest(new CreateTeamFromQuery() { ApiVersion = apiVersion },
                                                                            new CreateTeamFromBody()
                                                                            {
                                                                                Name = name,
                                                                                Image = image,
                                                                                Description = description,
                                                                                Layout = layout
                                                                            });
            var command = new CreateTeamCommand(name, image, description, layout);
            var mappedCommand = _cqsMapper.Map(request);

            // Assert
            command.Should().BeEquivalentTo(mappedCommand);
        }

        [TestMethod]
        public void MapUpdateTeamRequestShouldSucceed()
        {
            // Arrange
            var ifMatch = "123456";
            var version = 123456;
            var apiVersion = "1.0";
            var id = Guid.NewGuid();
            var name = "Test";
            var image = "update";
            var description = "updatedeDescription";
            var driverWait = "No";
            var layout = Guid.NewGuid().ToString();
            var filterContent = "{site:123456}";
            var members = new List<Guid>();

            // Act
            var request = new UpdateTeamRequest(new UpdateTeamFromHeader() { IfMatch = ifMatch },
                                                new UpdateTeamFromQuery() { ApiVersion = apiVersion },
                                                new UpdateTeamFromRoute() { Id = id.ToString() },
                                                new UpdateTeamFromBody()
                                                {
                                                    Name = name,
                                                    Image = image,
                                                    Description = description,
                                                    DriverWait = driverWait,
                                                    Layout = layout,
                                                    Members = members,
                                                    FilterContent = filterContent
                                                });

            var command = new UpdateTeamCommand(id,
                                                name,
                                                image,
                                                description,
                                                driverWait,
                                                layout,
                                                members,
                                                filterContent,
                                                version);
            var mappedCommand = _cqsMapper.Map(request);

            // Assert
            command.Should().BeEquivalentTo(mappedCommand);
        }

        [TestMethod]
        public void MapDeleteTeamShouldSucceed()
        {
            // Arrange
            var id = Guid.NewGuid();
            var apiVersion = "1.0";
            var version = 34567;
            var ifMatch = "34567";

            // Act
            var request = new DeleteTeamRequest(new DeleteTeamFromRoute() { Id = id.ToString() },
                                                new DeleteTeamFromQuery() { ApiVersion = apiVersion },
                                                new DeleteTeamFromHeader() { IfMatch = ifMatch });

            var command = new DeleteTeamCommand(id, version);
            var mappedCommand = _cqsMapper.Map(request);

            // Assert
            command.Should().BeEquivalentTo(mappedCommand);

        }

        [TestMethod]
        public void MapListOperatorsRequestShouldSucceed()
        {
            // Arrange
            var filter = "?id gt 100";
            var skip = "10";
            var top = "30";
            var apiVersion = "1.0";

            // Act
            var request = new ListOperatorRequest(new ListOperatorFromQuery()
            {
                ApiVersion = apiVersion,
                Filter = filter,
                Skip = skip,
                Top = top
            });

            var query = new ListOperatorQuery(filter, top, skip);
            var mappedQuery = _cqsMapper.Map(request);

            // Assert
            query.Should().BeEquivalentTo(mappedQuery);
        }

        [TestMethod]
        public void MapListDriverWaitRewuestShouldSucceed()
        {
            // Arrange
            var apiVersion = "1.0";

            // Act
            var request = new ListDriverWaitRequest(new ListDriverWaitFromQuery() { ApiVersion = apiVersion });
            var query = new ListDriverWaitQuery();
            var mappedQuery = _cqsMapper.Map(request);

            // Assert
            query.Should().Equals(mappedQuery);
        }
    }
}
