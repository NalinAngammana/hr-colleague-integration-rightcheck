
namespace ColleagueInt.RTW.Core.Logging
{
	using System;
	using System.Linq;
	using System.Text;
	using Microsoft.AspNetCore.Http;
	using Microsoft.AspNetCore.Http.Extensions;
	using Microsoft.Extensions.Logging;

	public class ExtendedLogger<T> : IExtendedLogger<T>
		where T : class
	{
		private IHttpContextAccessor _httpContextAccessor;
		private ILogger<T> _logger;

		public ExtendedLogger(ILogger<T> logger, IHttpContextAccessor httpContextAccessor)
		{
			_logger = logger;
			_httpContextAccessor = httpContextAccessor;
		}

		public void Log(string message)
		{
			_logger.Log(LogLevel.Information, message);
		}

		public void Log(LogLevel logLevel, string message)
		{
			_logger.Log(logLevel, message);
		}

		public void LogException(LogLevel logLevel, string message, Exception exception)
		{
			LogUnformatted(logLevel, LogDivider("Exception logged"));
			_logger.Log(logLevel, message);
			_logger.LogError(exception, message);
			LogUnformatted(logLevel, LogDivider());
		}

		public void LogFull(LogLevel logLevel, string message, Exception exception)
		{
			LogUnformatted(logLevel, LogDivider("Exception logged with full URL"));
			_logger.Log(logLevel, message);
			var request = _httpContextAccessor.HttpContext?.Request;

			if (request != null)
			{
				var emailAddress = request.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "preferred_username")?.Value;
				_logger.Log(logLevel, "URL was - " + UriHelper.GetDisplayUrl(request));
				_logger.Log(logLevel, "Logged in User email address was - " + emailAddress);
			}

			_logger.LogError(exception, message);
			LogUnformatted(logLevel, LogDivider());
		}

		public void LogUnformatted(LogLevel logLevel, string message)
		{
			_logger.Log(logLevel, message);
		}

		public void LogURL(LogLevel logLevel)
		{
			var request = _httpContextAccessor.HttpContext?.Request;
			if (request != null)
			{
				_logger.Log(logLevel, "URL was - " + UriHelper.GetDisplayUrl(request));
			}
		}

		public void LogURL(LogLevel logLevel, string message)
		{
			LogUnformatted(logLevel, LogDivider("Message logged with full URL"));
			_logger.Log(logLevel, message);
			var request = _httpContextAccessor.HttpContext?.Request;

			if (request != null)
			{
				var emailAddress = request.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "preferred_username")?.Value;
				_logger.Log(logLevel, "URL was - " + UriHelper.GetDisplayUrl(request));
				_logger.Log(logLevel, "Logged in User email address was - " + emailAddress);
			}

			LogUnformatted(logLevel, LogDivider());
		}

		// Creates a line of stars, with stars removed to include a centered message.
		private string LogDivider(string message = null)
		{
			if (message == null)
			{
				message = string.Empty;
			}

			message = message.Trim();

			if (message.Length >= 76)
			{
				throw new NotSupportedException("Message too long for the star line.");
			}

			var starNumber = 80 - (message.Length + 2);
			var starLength = starNumber / 2;

			var line = new StringBuilder(new string('*', starLength));
			if (message.Length > 0)
			{
				line.Append(' ');
				line.Append(message);
				line.Append(' ');
			}
			else
			{
				line.Append("**");
			}

			line.Append(new string('*', starLength + (starNumber % 2)));

			return line.ToString();
		}
	}
}
