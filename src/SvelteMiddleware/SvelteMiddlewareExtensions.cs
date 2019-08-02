using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.SpaServices;
using System;

namespace SvelteMiddleware
{
    /// <summary>
    /// Extension methods for enabling Svelte development server middleware support.
    /// </summary>
    public static class SvelteMiddlewareExtensions
    {
        /// <summary>
        /// Handles requests by passing them through to an instance of the Svelte server.
        /// This means you can always serve up-to-date CLI-built resources without having
        /// to run the Svelte server manually.
        ///
        /// This feature should only be used in development. For production deployments, be
        /// sure not to enable the Svelte server.
        /// </summary>
        /// <param name="spaBuilder">The <see cref="ISpaBuilder"/>.</param>
        /// <param name="npmScript">The name of the script in your package.json file that launches the Svelte server.</param>
        /// <param name="port">Specify vue cli server port number. If &lt; 80, uses random port. </param>
        /// <param name="runner">Specify the runner, Npm and Yarn are valid options. Yarn support is HIGHLY experimental.</param>
        /// <param name="regex">Specify a custom regex string to search for in the log indicating Svelte serve is complete.</param>
        public static void UseSvelte(
            this ISpaBuilder spaBuilder,
            string npmScript,
            int port = 0,
            ScriptRunnerType runner = ScriptRunnerType.Npm,
            string regex = SvelteMiddleware.DefaultRegex)
        {
            if (spaBuilder == null)
            {
                throw new ArgumentNullException(nameof(spaBuilder));
            }

            var spaOptions = spaBuilder.Options;

            if (string.IsNullOrEmpty(spaOptions.SourcePath))
            {
                throw new InvalidOperationException($"To use {nameof(UseSvelte)}, you must supply a non-empty value for the {nameof(SpaOptions.SourcePath)} property of {nameof(SpaOptions)} when calling {nameof(SpaApplicationBuilderExtensions.UseSpa)}.");
            }

            SvelteMiddleware.Attach(spaBuilder, npmScript, port, runner: runner, regex: regex);
        }
    }
}
