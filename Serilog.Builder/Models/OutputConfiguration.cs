using Serilog.Events;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Serilog.Builder.Tests")]
namespace Serilog.Builder.Models
{
    /// <summary>
    /// Outputs configurations
    /// </summary>
    internal class OutputConfiguration
    {
        /// <summary>
        /// Override namespace minimum level 
        /// </summary>
        public Dictionary<string, LogEventLevel> OverrideMinimumLevel { get; set; }

        /// <summary>
        /// Enrich Properties
        /// </summary>
        public Dictionary<string, object> EnrichProperties { get; set; }

        /// <summary>
        /// Console
        /// </summary>
        public Output<ConsoleOptions> Console { get; set; }

        /// <summary>
        /// Seq
        /// </summary>
        public Output<SeqOptions> Seq { get; set; }

        /// <summary>
        /// Splunk
        /// </summary>
        public Output<SplunkOptions> Splunk { get; set; }

        /// <summary>
        /// NewRelic
        /// </summary>
        public Output<NewRelicOptions> NewRelic { get; set; }

        /// <summary>
        /// DataDog
        /// </summary>
        public Output<DataDogOptions> DataDog { get; set; }

        /// <summary>
        /// Lapi
        /// </summary>
        public Output<LapiOptions> Lapi { get; set; }

        /// <summary>
        /// Minimum level
        /// </summary>
        public LogEventLevel MinimumLevel { get; set; }

        /// <summary>
        /// Enable enrich by environment and context
        /// </summary>
        public bool EnableEnrichWithEnvironment { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public OutputConfiguration()
        {
            this.Console = new Output<ConsoleOptions>();
            this.Seq = new Output<SeqOptions>();
            this.Splunk = new Output<SplunkOptions>();
            this.Lapi = new Output<LapiOptions>();
            this.NewRelic = new Output<NewRelicOptions>();
            this.DataDog = new Output<DataDogOptions>();
            this.OverrideMinimumLevel = new Dictionary<string, LogEventLevel>();
            this.EnrichProperties = new Dictionary<string, object>();
        }
    }
}
