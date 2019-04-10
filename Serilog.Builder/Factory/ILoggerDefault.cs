using System;
using System.Threading.Tasks;

namespace Serilog.Builder.Factory
{
    /// <summary>
    /// Serilog Logger default interface
    /// </summary>
    public interface ILoggerDefault
    {
        /// <summary>
        /// Log async with debug level
        /// </summary>
        /// <param name="message">Message</param>
        /// <returns></returns>
        Task DebugAsync(string message);

        /// <summary>
        /// Log async with information level
        /// </summary>
        /// <param name="message">Message</param>
        /// <returns></returns>
        Task InfoAsync(string message);

        /// <summary>
        /// Log async with warning level
        /// </summary>
        /// <param name="message">Message</param>
        /// <returns></returns>
        Task WarningAsync(string message);

        /// <summary>
        /// Log async with warning level with exception
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="ex">Exception</param>
        /// <returns></returns>
        Task WarningAsync(string message, Exception ex);

        /// <summary>
        /// Log async with error level
        /// </summary>
        /// <param name="message">Message</param>
        /// <returns></returns>
        Task ErrorAsync(string message);

        /// <summary>
        /// Log async with error level with exception
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="ex">Exception</param>
        /// <returns></returns>
        Task ErrorAsync(string message, Exception ex);
    }
}