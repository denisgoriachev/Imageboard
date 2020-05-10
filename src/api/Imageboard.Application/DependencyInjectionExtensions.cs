using AutoMapper;
using FluentValidation;
using Imageboard.Application.Behaviors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Imageboard.Application
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var currentAssembly = Assembly.GetExecutingAssembly();

            services.AddAutoMapper(currentAssembly);
            services.AddValidatorsFromAssembly(currentAssembly);
            services.AddMediatR(currentAssembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));

            return services;
        }
    }
}
