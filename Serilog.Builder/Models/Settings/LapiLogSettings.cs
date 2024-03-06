namespace Serilog.Builder.Models.Settings
{
    public class LapiLogSettings
    {
        public string ServerURL { get; set; }

        public string Index { get; set; }

        public string SourceType { get; set; }

        public string ProductCompany { get; set; }

        public string ProductVersion { get; set; }

        public string ProcessName { get; set; }

        public string Application { get; set; }

        public long? BatchSizeLimitBytes { get; set; }

        public int? LogEventsInBatchLimit { get; set; }

        public long? QueueLimitBytes { get; set; }

        public long? LogEventLimitBytes { get; set; }
    }
}
