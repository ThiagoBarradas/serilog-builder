using Microsoft.Extensions.Options;
using Serilog.Builder.Models;
using System;

namespace Serilog.Builder.Factory
{
    /// <summary>
    /// Serilog Logger factory class
    /// </summary>
    public class LoggerFactory : ILoggerFactory
    {
        /// <summary>
        /// Logger default instance
        /// </summary>
        private readonly ILoggerDefault _loggerDefault;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="loggerOptions">Logger Options</param>
        /// <param name="seqOptions">Seq Options</param>
        /// <param name="splunkOptions">Splunk Options</param>
        /// <param name="gcpOptions">Google Cloud Logging Options </param>
        public LoggerFactory(
            IOptions<LoggerOptions> loggerOptions,
            IOptions<ConsoleOptions> consoleOptions,
            IOptions<SeqOptions> seqOptions,
            IOptions<SplunkOptions> splunkOptions,
            IOptions<GoogleCloudLoggingOptions> gcpOptions)
        {
            LoggerBuilder builder = new LoggerBuilder();

            var logger = builder
                .EnableConsole()
                .UseSuggestedSetting(
                    loggerOptions.Value.Domain,
                    loggerOptions.Value.Application)
                .SetupConsole(consoleOptions.Value)
                .SetupSeq(seqOptions.Value)
                .SetupSplunk(splunkOptions.Value)
                .SetupGoogleCloudLogging(gcpOptions.Value)
                .BuildLogger();

            this._loggerDefault = new LoggerDefault(logger);

            if (loggerOptions.Value.IsDebugEnabled)
            {
                builder.EnableDebug();
                logger.Debug($"Logger working");
                _loggerDefault.DebugAsync($"LoggerDefault working");
            }
        }

        /// <summary>
        /// Get logger default
        /// </summary>
        /// <returns></returns>
        public ILoggerDefault GetInstance()
        {
            return this._loggerDefault;
        }

        /// <summary>
        /// Flush log
        /// </summary>
        public static void Flush()
        {
            Log.CloseAndFlush();
        }
    }
}
