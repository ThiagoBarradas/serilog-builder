using Serilog.Events;

namespace Serilog.Builder.Models
{
    public class LapiOptions
    {
        /// <summary>
        /// Set if Lapi is enabled
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// Minimum Level
        /// </summary>
        public LogEventLevel? MinimumLevel { get; set; }

        /// <summary>
        /// Lapi index
        /// </summary>
        public string Index { get; set; }

        /// <summary>
        /// Lapi Application
        /// </summary>
        public string Application { get; set; }

        /// <summary>
        /// Process name
        /// </summary>
        public string ProcessName { get; set; }

        /// <summary>
        /// Company
        /// </summary>
        public string Company { get; set; }

        /// <summary>
        /// Product version
        /// </summary>
        public string ProductVersion { get; set; }

        /// <summary>
        /// Server Url
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Source type
        /// </summary>
        public string SourceType { get; set; }

        /// <summary>
        /// Batch size limit bytes
        /// </summary>
        public long? BatchSizeLimitBytes { get; set; }

        /// <summary>
        /// Log events in batch limit
        /// </summary>
        public int? LogEventsInBatchLimit { get; set; }

        /// <summary>
        /// Queue limit bytes
        /// </summary>
        public long? QueueLimitBytes { get; set; }
    }
}
