using AutoMapper;
using ITG.Brix.Teams.API.Context.Services;
using ITG.Brix.Teams.API.Context.Services.Arrangements;
using ITG.Brix.Teams.API.Context.Services.Arrangements.Impl;
using ITG.Brix.Teams.API.Context.Services.Impl;
using ITG.Brix.Teams.API.Context.Services.Requests;
using ITG.Brix.Teams.API.Context.Services.Requests.Impl;
using ITG.Brix.Teams.API.Context.Services.Requests.Mappers;
using ITG.Brix.Teams.API.Context.Services.Requests.Mappers.Impl;
using ITG.Brix.Teams.API.Context.Services.Requests.Validators;
using ITG.Brix.Teams.API.Context.Services.Requests.Validators.Components.Impl;
using ITG.Brix.Teams.API.Context.Services.Requests.Validators.Impl;
using ITG.Brix.Teams.API.Context.Services.Responses;
using ITG.Brix.Teams.API.Context.Services.Responses.Impl;
using ITG.Brix.Teams.API.Context.Services.Responses.Mappers;
using ITG.Brix.Teams.API.Context.Services.Responses.Mappers.Impl;
using ITG.Brix.Teams.Domain.Repositories;
using ITG.Brix.Teams.Infrastructure.DataAccess.ClassMaps;
using ITG.Brix.Teams.Infrastructure.DataAccess.Configurations;
using ITG.Brix.Teams.Infrastructure.DataAccess.Configurations.Impl;
using ITG.Brix.Teams.Infrastructure.DataAccess.Repositories;
using ITG.Brix.Teams.Infrastructure.Providers;
using ITG.Brix.Teams.Infrastructure.Providers.Impl;
using Microsoft.Extensions.DependencyInjection;

namespace ITG.Brix.Teams.DependencyResolver
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper();

            return services;
        }

        public static IServiceCollection Persistence(this IServiceCollection services, string connectionString)
        {
            ClassMapsRegistrator.RegisterMaps();

            services.AddScoped<IPersistenceContext, PersistenceContext>();
            services.AddScoped<IPersistenceConfiguration>(x => new PersistenceConfiguration(connectionString));
            services.AddScoped<IOperatorReadRepository, OperatorReadRepository>();
            services.AddScoped<IOperatorWriteRepository, OperatorWriteRepository>();
            services.AddScoped<ITeamWriteRepository, TeamWriteRepository>();
            services.AddScoped<ITeamReadRepository, TeamReadRepository>();

            return services;
        }

        public static IServiceCollection Providers(this IServiceCollection services)
        {
            services.AddScoped<IVersionProvider, VersionProvider>();
            services.AddScoped<ITeamOdataProvider, TeamOdataProvider>();
            services.AddScoped<IIdentifierProvider, IdentifierProvider>();
            services.AddScoped<IOperatorOdataProvider, OperatorOdataProvider>();
            services.AddScoped<ITeamOdataProvider, TeamOdataProvider>();
            return services;
        }

        public static IServiceCollection ApiServices(this IServiceCollection services)
        {
            services.AddScoped<IApiRequest, ApiRequest>();
            services.AddScoped<IApiResult, ApiResult>();
            services.AddScoped<IApiResponse, ApiResponse>();
            services.AddScoped<IValidationArrangement, ValidationArrangement>();
            services.AddScoped<IOperationArrangement, OperationArrangement>();

            services.AddScoped<IErrorMapper, ErrorMapper>();
            services.AddScoped<ICqsMapper, CqsMapper>();
            services.AddScoped<IHttpStatusCodeMapper, HttpStatusCodeMapper>();

            return services;
        }

        public static IServiceCollection RequestValidators(this IServiceCollection services)
        {
            services.AddScoped<IRequestComponentValidator, RequestComponentValidator>();
            services.AddScoped<IRequestValidator, ListTeamRequestValidator>();
            services.AddScoped<IRequestValidator, GetTeamRequestValidator>();
            services.AddScoped<IRequestValidator, CreateTeamRequestValidator>();
            services.AddScoped<IRequestValidator, UpdateTeamRequestValidator>();
            services.AddScoped<IRequestValidator, ListOperatorRequestValidator>();
            services.AddScoped<IRequestValidator, ListDriverWaitRequestValidator>();
            services.AddScoped<IRequestValidator, DeleteTeamRequestValidator>();

            return services;
        }
    }
}
