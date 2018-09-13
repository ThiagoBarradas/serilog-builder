using Serilog.Events;

namespace Serilog.Builder.Models
{
    /// <summary>
    /// Output
    /// </summary>
    internal class Output<T> where T : class, new()
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Output()
        {
            this.Options = new T();   
        }

        /// <summary>
        /// Enable output
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// Settings for output
        /// </summary>
        public T Options { get; set; }
    }
}
