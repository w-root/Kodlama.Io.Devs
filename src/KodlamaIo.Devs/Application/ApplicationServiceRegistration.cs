using Application.Features.Auths.Rules;
using Application.Features.GithubProfileFeature.Rules;
using Application.Features.OperationClaimFeature.Rules;
using Application.Features.ProgrammingLanguageFeature.Rules;
using Application.Features.UserOperationClaimFeature.Rules;
using Application.Services.Auth;
using Application.Services.Repositories;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Validation;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddScoped<ProgrammingLanguagesRules>();
            services.AddScoped<AuthBusinessRules>();
            services.AddScoped<GithubProfileRules>();
            services.AddScoped<UserOperationClaimRules>();
            services.AddScoped<OperationClaimsRules>();

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehavior<,>));

            services.AddScoped<IAuthService, AuthManager>();

            return services;

        }

    }
}
