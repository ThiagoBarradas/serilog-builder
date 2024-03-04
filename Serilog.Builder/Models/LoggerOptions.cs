namespace Serilog.Builder.Models
{
    /// <summary>
    /// Logger options class
    /// </summary>
    public class LoggerOptions
    {
        /// <summary>
        /// Application
        /// </summary>
        public string Application { get; set; }

        /// <summary>
        /// Domain
        /// </summary>
        public string Domain { get; set; }

        /// <summary>
        /// Is Debug Enabled
        /// </summary>
        public bool IsDebugEnabled { get; set; }

        /// <summary>
        /// Console
        /// </summary>
        public ConsoleOptions Console { get; set; } = new ConsoleOptions { Enabled = true };

        /// <summary>
        /// Seq
        /// </summary>
        public SeqOptions Seq { get; set; } = new SeqOptions();

        /// <summary>
        /// Splunk
        /// </summary>
        public SplunkOptions Splunk { get; set; } = new SplunkOptions();

        /// <summary>
        /// Lapi
        /// </summary>
        public LapiOptions Lapi { get; set; } = new LapiOptions();

        /// <summary>
        /// NewRelic
        /// </summary>
        public NewRelicOptions NewRelic { get; set; } = new NewRelicOptions();

        /// <summary>
        /// DataDog
        /// </summary>
        public DataDogOptions DataDog { get; set; } = new DataDogOptions();
    }
}
