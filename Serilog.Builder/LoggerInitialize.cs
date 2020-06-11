using Serilog.Builder.Models;

namespace Serilog.Builder
{
    /// <summary>
    /// Serilog logger initialize class
    /// </summary>
    public static class LoggerInitialize
    {
        /// <summary>
        /// Start initialization
        /// </summary>
        /// <returns></returns>
        public static void Start(LoggerOptions loggerOptions)
        {
            LoggerBuilder builder = new LoggerBuilder();

            Log.Logger = builder
                .UseSuggestedSetting(
                    loggerOptions.Domain,
                    loggerOptions.Application)
                .SetupConsole(loggerOptions.Console)
                .SetupSeq(loggerOptions.Seq)
                .SetupSplunk(loggerOptions.Splunk)
                .SetupNewRelic(loggerOptions.NewRelic)
                .BuildLogger();

            if (loggerOptions.IsDebugEnabled)
            {
                builder.EnableDebug();
                Log.Logger.Debug($"Logger working");
            }
        }
    }
}
