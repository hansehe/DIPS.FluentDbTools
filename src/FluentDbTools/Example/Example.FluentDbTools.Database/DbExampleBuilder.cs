﻿using System;
using System.Collections.Generic;
using FluentDbTools.Common.Abstractions;
using Example.FluentDbTools.Common;
using FluentDbTools.Extensions.MSDependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Example.FluentDbTools.Database
{
    public static class DbExampleBuilder
    {
        public static IServiceProvider BuildDbExample(
            SupportedDatabaseTypes databaseType,
            bool useDbProviderFactory,
            Dictionary<string, string> overrideConfig = null)
        {
            return new ServiceCollection()
                .Register(services => ServiceRegistration.Register(services, useDbProviderFactory))
                .UseExampleConfiguration(
                    databaseType,
                    overrideConfig)
                .UseDefaultLogging()
                .BuildServiceProvider();
        }
    }
}