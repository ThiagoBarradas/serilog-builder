using Serilog.Builder.Models;
using System;

namespace Serilog.Builder
{
    /// <summary>
    /// Serilog Logger builder
    /// </summary>
    public partial class LoggerBuilder : ILoggerBuilder
    {
        /// <summary>
        /// Build console configuration
        /// </summary>
        /// <param name="logger"></param>
        private void BuildConsole(LoggerConfiguration logger)
        {
            if (this.OutputConfiguration.Console.Enabled == true)
            {
                var logLevel = this.OutputConfiguration.Console.Options.MinimumLevel ?? this.OutputConfiguration.MinimumLevel;
                logger.WriteTo.Console(restrictedToMinimumLevel: logLevel);
            }
        }

        /// <summary>
        /// Enable console
        /// </summary>
        /// <returns></returns>
        public LoggerBuilder EnableConsole()
        {
            return this.SetupConsole(new ConsoleOptions
            {
                Enabled = true
            });
        }

        /// <summary>
        /// Setup console
        /// </summary>
        /// <returns></returns>
        public LoggerBuilder SetupConsole(ConsoleOptions options)
        {
            this.OutputConfiguration.Console.Options = options
                ?? throw new ArgumentNullException(nameof(options));

            this.OutputConfiguration.Console.Enabled = options.Enabled;
            return this;
        }

        /// <summary>
        /// Disable console
        /// </summary>
        /// <returns></returns>
        public LoggerBuilder DisableConsole()
        {
            this.OutputConfiguration.Console.Enabled = false;
            return this;
        }
    }
}