using Serilog.Builder.Models;
using Serilog.Events;

namespace Serilog.Builder
{
    /// <summary>
    /// Serilog Logger builder interface
    /// </summary>
    public interface ILoggerBuilder  
    {
        /// <summary>
        /// Use suggested setting. This method reset all setting before apply suggested configurations
        /// - enable console
        /// - add enrich with environment
        /// - enrich with domain and application
        /// - minimum level : debug
        /// - override Microsoft/warning and System/error
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="application"></param>
        /// <returns></returns>
        LoggerBuilder UseSuggestedSetting(string domain, string applications);

        /// <summary>
        /// Enable console
        /// </summary>
        /// <returns></returns>
        LoggerBuilder EnableConsole();

        /// <summary>
        /// Enable debug
        /// </summary>
        /// <returns></returns>
        LoggerBuilder EnableDebug();

        /// <summary>
        /// Disable debug
        /// </summary>
        /// <returns></returns>
        LoggerBuilder DisableDebug();

        /// <summary>
        /// Disable console
        /// </summary>
        /// <returns></returns>
        LoggerBuilder DisableConsole();

        /// <summary>
        /// Enable seq
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        LoggerBuilder EnableSeq(string url);

        /// <summary>
        /// Enable seq
        /// </summary>
        /// <param name="url"></param>
        /// <param name="apiKey"></param>
        /// <returns></returns>
        LoggerBuilder EnableSeq(string url, string apiKey);

        /// <summary>
        /// Enable seq
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        LoggerBuilder SetupSeq(SeqOptions options);

        /// <summary>
        /// Disable seq
        /// </summary>
        /// <returns></returns>
        LoggerBuilder DisableSeq();

        /// <summary>
        /// Enable splunk
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        LoggerBuilder EnableSplunk(string url);

        /// <summary>
        /// Enable splunk
        /// </summary>
        /// <param name="url"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        LoggerBuilder EnableSplunk(string url, string token);

        /// <summary>
        /// Enable seq
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        LoggerBuilder SetupSplunk(SplunkOptions options);

        /// <summary>
        /// Disable splunk
        /// </summary>
        /// <returns></returns>
        LoggerBuilder DisableSplunk();

        /// <summary>
        /// Disable all
        /// </summary>
        /// <returns></returns>
        LoggerBuilder DisableAllOutputs();

        /// <summary>
        /// Enrich with environment properties
        /// <returns></returns>
        LoggerBuilder EnableEnrichWithEnvironment();

        /// <summary>
        /// Disable enrich with environment properties
        /// <returns></returns>
        LoggerBuilder DisableEnrichWithEnvironment();

        /// <summary>
        /// Enrich properties
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        LoggerBuilder AddEnrichProperty(string key, object value);

        /// <summary>
        /// Remove some enrich
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        LoggerBuilder RemoveEnrichProperty(string key);

        /// <summary>
        /// Set MinimumLevel
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        LoggerBuilder SetMinimumLevel(LogEventLevel level);

        /// <summary>
        /// Override MinimumLevel by namespace
        /// </summary>
        /// <param name="_namespace"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        LoggerBuilder AddOverrideMinimumLevel(string _namespace, LogEventLevel level);

        /// <summary>
        /// Remove Override MinimumLevel by namespace
        /// </summary>
        /// <param name="_namespace"></param>
        /// <returns></returns>
        LoggerBuilder RemoveOverrideMinimumLevel(string _namespace);

        /// <summary>
        /// Build Logger
        /// </summary>
        /// <returns></returns>
        ILogger BuildLogger();

        /// <summary>
        /// Build configuration
        /// </summary>
        /// <returns></returns>
        LoggerConfiguration BuildConfiguration();
    }
}
