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
        /// Debug Enable
        /// </summary>
        public bool DebugEnable { get; set; }
    }
}
