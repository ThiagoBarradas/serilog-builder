namespace Serilog.Builder.Factory
{
    /// <summary>
    /// Serilog Logger factory interface
    /// </summary>
    public interface ILoggerFactory
    {
        /// <summary>
        /// Get logger default
        /// </summary>
        /// <returns></returns>
        ILoggerDefault GetInstance();
    }
}
