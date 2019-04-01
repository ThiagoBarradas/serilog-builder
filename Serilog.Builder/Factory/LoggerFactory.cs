using Microsoft.Extensions.Options;
using Serilog.Builder.Models;

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
        private static ILoggerDefault _loggerDefault;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="loggerOptions">Logger Options</param>
        /// <param name="seqOptions">Seq Options</param>
        /// <param name="splunkOptions">Splunk Options</param>
        /// <param name="gcpOptions">Google Cloud Logging Options </param>
        public LoggerFactory(
            IOptions<LoggerOptions> loggerOptions,
            IOptions<SeqOptions> seqOptions,
            IOptions<SplunkOptions> splunkOptions,
            IOptions<GoogleCloudLoggingOptions> gcpOptions)
        {
            LoggerBuilder builder = new LoggerBuilder();

            var logger = loggerOptions.Value;

            Log.Logger = builder
            .UseSuggestedSetting(logger.Domain, logger.Application)
            .SetupSeq(seqOptions.Value)
            .SetupSplunk(splunkOptions.Value)
            .SetupGoogleCloudLogging(gcpOptions.Value)
            .BuildLogger();
        }

        /// <summary>
        /// Create logger default
        /// </summary>
        /// <returns></returns>
        public ILoggerDefault Create()
        {
            if (_loggerDefault == null)
                _loggerDefault = new LoggerDefault();

            return _loggerDefault;
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
