using Microsoft.Extensions.Configuration;
using Serilog.Builder.CustomFormatters;
using Serilog.Configuration;
using Serilog.Events;
using Serilog.Sinks.Http;
using Serilog.Sinks.Http.BatchFormatters;
using System;

namespace Serilog.Builder.Extensions
{
    public static class LoggerSinkConfigurationExtensions
    {
        public static LoggerConfiguration Lapi(
            this LoggerSinkConfiguration sinkConfiguration,
            string requestUri,
            string index,
            long? queueLimitBytes = null,
            long? logEventLimitBytes = null,
            int? logEventsInBatchLimit = null,
            long? batchSizeLimitBytes = null,
            TimeSpan? period = null,
            LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum,
            IHttpClient httpClient = null,
            IConfiguration configuration = null)
        {
            return sinkConfiguration.Http(requestUri: requestUri,
                                          queueLimitBytes: queueLimitBytes > 0 ? queueLimitBytes : null,
                                          logEventLimitBytes: logEventLimitBytes,
                                          logEventsInBatchLimit: logEventsInBatchLimit > 0 ? logEventsInBatchLimit : 1000,
                                          batchSizeLimitBytes: batchSizeLimitBytes > 0 ? batchSizeLimitBytes : null,
                                          period: period,
                                          textFormatter: new LapiJsonFormatter(index),
                                          batchFormatter: new ArrayBatchFormatter(),
                                          restrictedToMinimumLevel: restrictedToMinimumLevel,
                                          httpClient: httpClient,
                                          configuration: configuration);
        }
    }
}
