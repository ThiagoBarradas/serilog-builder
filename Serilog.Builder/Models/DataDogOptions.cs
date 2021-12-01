using Serilog.Events;

namespace Serilog.Builder.Models
{
    /// <summary>
    /// DataDog options
    /// </summary>
    public class DataDogOptions
    {
        /// <summary>
        /// Set if DataDog is enabled
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// Minimum Level
        /// </summary>
        public LogEventLevel? MinimumLevel { get; set; }

        /// <summary>
        /// DataDog API Key
        /// </summary>
        public string ApiKey { get; set; }

        /// <summary>
        /// DataDog Service
        /// </summary>
        public string Service { get; set; }

        /// <summary>
        /// DataDog Source
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// DataDog Host
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// DataDog Tags
        /// </summary>
        public string[] Tags{ get; set; }
    }
}
