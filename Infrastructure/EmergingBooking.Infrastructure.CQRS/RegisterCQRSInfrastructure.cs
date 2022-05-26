﻿using EmergingBooking.Infrastructure.CQRS.Command;
using EmergingBooking.Infrastructure.CQRS.Events;
using EmergingBooking.Infrastructure.CQRS.Queries.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmergingBooking.Infrastructure.CQRS
{
    public static class RegisterCQRSInfrastructure
    {
        public static IServiceCollection RegisterInfrastructureDependencies(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddSingleton<DependencyResolver>();

            services.AddSingleton<ICommandDispatcher, CommandDispatcher>();
            services.AddSingleton<IQueryProcessor, IQueryProcessor>();
            services.AddSingleton<IEventPublisher, EventPublisher>();

            return services;
        }
        
    }
}
