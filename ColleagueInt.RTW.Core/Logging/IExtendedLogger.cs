
namespace ColleagueInt.RTW.Core.Logging
{
	using System;
	using Microsoft.Extensions.Logging;

	public interface IExtendedLogger<T>
		where T : class
	{
		/// <summary>
		/// Standard log entry - information level.
		/// </summary>
		/// <param name="message">The message to log.</param>
		void Log(string message);

		/// <summary>
		/// Standard log entry.
		/// </summary>
		/// <param name="logLevel">The log level.</param>
		/// <param name="message">The message to log.</param>
		void Log(LogLevel logLevel, string message);

		/// <summary>
		/// Logs a fully unrolled exception.
		/// </summary>
		/// <param name="logLevel">The log level.</param>
		/// <param name="message">The message to log.</param>
		/// <param name="exception">The exception.</param>
		void LogException(LogLevel logLevel, string message, Exception exception);

		/// <summary>
		/// Logs a fully unrolled exception, the URL and selected context variables.
		/// </summary>
		/// <param name="logLevel">The log level.</param>
		/// <param name="message">The message to log.</param>
		/// <param name="exception">The exception.</param>
		void LogFull(LogLevel logLevel, string message, Exception exception);

		/// <summary>
		/// Logs a line to an alternative log pointed at the same target but with no formatting.
		/// </summary>
		/// <param name="logLevel">The log level.</param>
		/// <param name="message">The message.</param>
		void LogUnformatted(LogLevel logLevel, string message);

		/// <summary>
		/// Logs the current request URL.
		/// </summary>
		/// <param name="logLevel">The log level.</param>
		void LogURL(LogLevel logLevel);

		/// <summary>
		/// Logs the current request URL and a message.
		/// </summary>
		/// <param name="logLevel">The log level.</param>
		/// <param name="message">Logs the URL with a message.</param>
		void LogURL(LogLevel logLevel, string message);
	}
}
