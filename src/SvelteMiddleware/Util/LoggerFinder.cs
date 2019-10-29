// Copyright (c) .NET Foundation Contributors. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Original Source: https://github.com/aspnet/JavaScriptServices

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using System;
using System.Threading.Tasks;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("SvelteMiddleware.Tests")]
namespace SvelteMiddleware
{
	internal static class LoggerFinder
	{
		public static ILogger GetOrCreateLogger(IApplicationBuilder appBuilder, string logCategoryName)
		{
			// If the DI system gives us a logger, use it. Otherwise, set up a default one.
			var loggerFactory = appBuilder.ApplicationServices.GetService<ILoggerFactory>();
			ILogger logger;
			if (loggerFactory != null)
			{
				logger = loggerFactory.CreateLogger(logCategoryName);
			}
			else
			{
				using var newFactory = LoggerFactory.Create(builder =>
				{
					builder.AddConsole();
				});
				logger = newFactory.CreateLogger(logCategoryName);
			}
			return logger;
		}
	}
}
