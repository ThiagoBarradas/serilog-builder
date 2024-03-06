﻿using NewRelic.LogEnrichers.Serilog;
using Serilog.Builder.Models;
using System;

namespace Serilog.Builder
{
    /// <summary>
    /// DataDog Logger builder
    /// </summary>
    public partial class LoggerBuilder : ILoggerBuilder
    {
        /// <summary>
        /// Build DataDog configuration
        /// </summary>
        /// <param name="logger"></param>
        private void BuildDataDog(LoggerConfiguration logger)
        {
            if (this.OutputConfiguration.DataDog.Enabled == true)
            {
                var logLevel = this.OutputConfiguration.DataDog.Options.MinimumLevel ?? this.OutputConfiguration.MinimumLevel;

                logger.WriteTo.DatadogLogs(
                    this.OutputConfiguration.DataDog.Options.ApiKey,
                    this.OutputConfiguration.DataDog.Options.Source,
                    this.OutputConfiguration.DataDog.Options.Service,
                    this.OutputConfiguration.DataDog.Options.Host,
                    this.OutputConfiguration.DataDog.Options.Tags, 
                    null, null, logLevel, null, null, null, null, false);
            }
        }

        /// <summary>
        /// Enable DataDog
        /// </summary>
        /// <param name="appName"></param>
        /// <returns></returns>
        public LoggerBuilder EnableDataDog(string apiKey)
        {
            return this.SetupDataDog(new DataDogOptions
            {
                Enabled = true,
                ApiKey = apiKey
            });
        }

        /// <summary>
        /// Setup DataDog
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public LoggerBuilder SetupDataDog(DataDogOptions options)
        {
            this.OutputConfiguration.DataDog.Options = options
                ?? throw new ArgumentNullException(nameof(options));

            if (string.IsNullOrWhiteSpace(options.ApiKey) == true && options.Enabled == true)
            {
                throw new ArgumentNullException(nameof(options.ApiKey));
            }

            this.OutputConfiguration.DataDog.Enabled = options.Enabled;

            return this;
        }

        /// <summary>
        /// Disable DataDog
        /// </summary>
        /// <returns></returns>
        public LoggerBuilder DisableDataDog()
        {
            this.OutputConfiguration.DataDog.Enabled = false;
            return this;
        }
    }
}