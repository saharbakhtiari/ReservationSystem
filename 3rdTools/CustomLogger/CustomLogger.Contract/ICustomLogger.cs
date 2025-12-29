using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using LogLevel = Microsoft.Extensions.Logging.LogLevel;

namespace CustomLoggers;

public interface ICustomLogger<TCategoryName>
{
    Task Log(LogLevel logLevel, Exception exception, string message, params object[] args);
    //
    // Summary:
    //     Formats and writes a log message at the specified log level.
    //
    // Parameters:
    //   logger:
    //     The Microsoft.Extensions.Logging.ILogger to write to.
    //
    //   logLevel:
    //     Entry will be written on this level.
    //
    //   eventId:
    //     The event id associated with the log.
    //
    //   message:
    //     Format string of the log message.
    //
    //   args:
    //     An object array that contains zero or more objects to format.
    Task Log(LogLevel logLevel, EventId eventId, string message, params object[] args);
    //
    // Summary:
    //     Formats and writes a log message at the specified log level.
    //
    // Parameters:
    //   logger:
    //     The Microsoft.Extensions.Logging.ILogger to write to.
    //
    //   logLevel:
    //     Entry will be written on this level.
    //
    //   message:
    //     Format string of the log message.
    //
    //   args:
    //     An object array that contains zero or more objects to format.
    Task Log(LogLevel logLevel, string message, params object[] args);
    //
    // Summary:
    //     Formats and writes a log message at the specified log level.
    //
    // Parameters:
    //   logger:
    //     The Microsoft.Extensions.Logging.ILogger to write to.
    //
    //   logLevel:
    //     Entry will be written on this level.
    //
    //   eventId:
    //     The event id associated with the log.
    //
    //   exception:
    //     The exception to log.
    //
    //   message:
    //     Format string of the log message.
    //
    //   args:
    //     An object array that contains zero or more objects to format.
    Task Log(LogLevel logLevel, EventId eventId, Exception exception, string message, params object[] args);
    //
    // Summary:
    //     Formats and writes a critical log message.
    //
    // Parameters:
    //   logger:
    //     The Microsoft.Extensions.Logging.ILogger to write to.
    //
    //   message:
    //     Format string of the log message in message template format. Example:
    //     "User {User} logged in from {Address}"
    //
    //   args:
    //     An object array that contains zero or more objects to format.
    Task LogCritical(string message, params object[] args);
    //
    // Summary:
    //     Formats and writes a critical log message.
    //
    // Parameters:
    //   logger:
    //     The Microsoft.Extensions.Logging.ILogger to write to.
    //
    //   exception:
    //     The exception to log.
    //
    //   message:
    //     Format string of the log message in message template format. Example:
    //     "User {User} logged in from {Address}"
    //
    //   args:
    //     An object array that contains zero or more objects to format.
    Task LogCritical(Exception exception, string message, params object[] args);
    //
    // Summary:
    //     Formats and writes a critical log message.
    //
    // Parameters:
    //   logger:
    //     The Microsoft.Extensions.Logging.ILogger to write to.
    //
    //   eventId:
    //     The event id associated with the log.
    //
    //   message:
    //     Format string of the log message in message template format. Example:
    //     "User {User} logged in from {Address}"
    //
    //   args:
    //     An object array that contains zero or more objects to format.
    Task LogCritical(EventId eventId, string message, params object[] args);
    //
    // Summary:
    //     Formats and writes a critical log message.
    //
    // Parameters:
    //   logger:
    //     The Microsoft.Extensions.Logging.ILogger to write to.
    //
    //   eventId:
    //     The event id associated with the log.
    //
    //   exception:
    //     The exception to log.
    //
    //   message:
    //     Format string of the log message in message template format. Example:
    //     "User {User} logged in from {Address}"
    //
    //   args:
    //     An object array that contains zero or more objects to format.
    Task LogCritical(EventId eventId, Exception exception, string message, params object[] args);
    //
    // Summary:
    //     Formats and writes a debug log message.
    //
    // Parameters:
    //   logger:
    //     The Microsoft.Extensions.Logging.ILogger to write to.
    //
    //   eventId:
    //     The event id associated with the log.
    //
    //   exception:
    //     The exception to log.
    //
    //   message:
    //     Format string of the log message in message template format. Example:
    //     "User {User} logged in from {Address}"
    //
    //   args:
    //     An object array that contains zero or more objects to format.
    Task LogDebug(EventId eventId, Exception exception, string message, params object[] args);
    //
    // Summary:
    //     Formats and writes a debug log message.
    //
    // Parameters:
    //   logger:
    //     The Microsoft.Extensions.Logging.ILogger to write to.
    //
    //   eventId:
    //     The event id associated with the log.
    //
    //   message:
    //     Format string of the log message in message template format. Example:
    //     "User {User} logged in from {Address}"
    //
    //   args:
    //     An object array that contains zero or more objects to format.
    Task LogDebug(EventId eventId, string message, params object[] args);
    //
    // Summary:
    //     Formats and writes a debug log message.
    //
    // Parameters:
    //   logger:
    //     The Microsoft.Extensions.Logging.ILogger to write to.
    //
    //   exception:
    //     The exception to log.
    //
    //   message:
    //     Format string of the log message in message template format. Example:
    //     "User {User} logged in from {Address}"
    //
    //   args:
    //     An object array that contains zero or more objects to format.
    Task LogDebug(Exception exception, string message, params object[] args);
    //
    // Summary:
    //     Formats and writes a debug log message.
    //
    // Parameters:
    //   logger:
    //     The Microsoft.Extensions.Logging.ILogger to write to.
    //
    //   message:
    //     Format string of the log message in message template format. Example:
    //     "User {User} logged in from {Address}"
    //
    //   args:
    //     An object array that contains zero or more objects to format.
    Task LogDebug(string message, params object[] args);
    //
    // Summary:
    //     Formats and writes an error log message.
    //
    // Parameters:
    //   logger:
    //     The Microsoft.Extensions.Logging.ILogger to write to.
    //
    //   message:
    //     Format string of the log message in message template format. Example:
    //     "User {User} logged in from {Address}"
    //
    //   args:
    //     An object array that contains zero or more objects to format.
    Task LogError(string message, params object[] args);
    //
    // Summary:
    //     Formats and writes an error log message.
    //
    // Parameters:
    //   logger:
    //     The Microsoft.Extensions.Logging.ILogger to write to.
    //
    //   exception:
    //     The exception to log.
    //
    //   message:
    //     Format string of the log message in message template format. Example:
    //     "User {User} logged in from {Address}"
    //
    //   args:
    //     An object array that contains zero or more objects to format.
    Task LogError(Exception exception, string message, params object[] args);
    //
    // Summary:
    //     Formats and writes an error log message.
    //
    // Parameters:
    //   logger:
    //     The Microsoft.Extensions.Logging.ILogger to write to.
    //
    //   eventId:
    //     The event id associated with the log.
    //
    //   exception:
    //     The exception to log.
    //
    //   message:
    //     Format string of the log message in message template format. Example:
    //     "User {User} logged in from {Address}"
    //
    //   args:
    //     An object array that contains zero or more objects to format.
    Task LogError(EventId eventId, Exception exception, string message, params object[] args);
    //
    // Summary:
    //     Formats and writes an error log message.
    //
    // Parameters:
    //   logger:
    //     The Microsoft.Extensions.Logging.ILogger to write to.
    //
    //   eventId:
    //     The event id associated with the log.
    //
    //   message:
    //     Format string of the log message in message template format. Example:
    //     "User {User} logged in from {Address}"
    //
    //   args:
    //     An object array that contains zero or more objects to format.
    Task LogError(EventId eventId, string message, params object[] args);
    //
    // Summary:
    //     Formats and writes an informational log message.
    //
    // Parameters:
    //   logger:
    //     The Microsoft.Extensions.Logging.ILogger to write to.
    //
    //   eventId:
    //     The event id associated with the log.
    //
    //   message:
    //     Format string of the log message in message template format. Example:
    //     "User {User} logged in from {Address}"
    //
    //   args:
    //     An object array that contains zero or more objects to format.
    Task LogInformation(EventId eventId, string message, params object[] args);
    //
    // Summary:
    //     Formats and writes an informational log message.
    //
    // Parameters:
    //   logger:
    //     The Microsoft.Extensions.Logging.ILogger to write to.
    //
    //   exception:
    //     The exception to log.
    //
    //   message:
    //     Format string of the log message in message template format. Example:
    //     "User {User} logged in from {Address}"
    //
    //   args:
    //     An object array that contains zero or more objects to format.
    Task LogInformation(Exception exception, string message, params object[] args);
    //
    // Summary:
    //     Formats and writes an informational log message.
    //
    // Parameters:
    //   logger:
    //     The Microsoft.Extensions.Logging.ILogger to write to.
    //
    //   eventId:
    //     The event id associated with the log.
    //
    //   exception:
    //     The exception to log.
    //
    //   message:
    //     Format string of the log message in message template format. Example:
    //     "User {User} logged in from {Address}"
    //
    //   args:
    //     An object array that contains zero or more objects to format.
    Task LogInformation(EventId eventId, Exception exception, string message, params object[] args);
    //
    // Summary:
    //     Formats and writes an informational log message.
    //
    // Parameters:
    //   logger:
    //     The Microsoft.Extensions.Logging.ILogger to write to.
    //
    //   message:
    //     Format string of the log message in message template format. Example:
    //     "User {User} logged in from {Address}"
    //
    //   args:
    //     An object array that contains zero or more objects to format.
    Task LogInformation(string message, params object[] args);
    //
    // Summary:
    //     Formats and writes a trace log message.
    //
    // Parameters:
    //   logger:
    //     The Microsoft.Extensions.Logging.ILogger to write to.
    //
    //   message:
    //     Format string of the log message in message template format. Example:
    //     "User {User} logged in from {Address}"
    //
    //   args:
    //     An object array that contains zero or more objects to format.
    Task LogTrace(string message, params object[] args);
    //
    // Summary:
    //     Formats and writes a trace log message.
    //
    // Parameters:
    //   logger:
    //     The Microsoft.Extensions.Logging.ILogger to write to.
    //
    //   exception:
    //     The exception to log.
    //
    //   message:
    //     Format string of the log message in message template format. Example:
    //     "User {User} logged in from {Address}"
    //
    //   args:
    //     An object array that contains zero or more objects to format.
    Task LogTrace(Exception exception, string message, params object[] args);
    //
    // Summary:
    //     Formats and writes a trace log message.
    //
    // Parameters:
    //   logger:
    //     The Microsoft.Extensions.Logging.ILogger to write to.
    //
    //   eventId:
    //     The event id associated with the log.
    //
    //   message:
    //     Format string of the log message in message template format. Example:
    //     "User {User} logged in from {Address}"
    //
    //   args:
    //     An object array that contains zero or more objects to format.
    Task LogTrace(EventId eventId, string message, params object[] args);
    //
    // Summary:
    //     Formats and writes a trace log message.
    //
    // Parameters:
    //   logger:
    //     The Microsoft.Extensions.Logging.ILogger to write to.
    //
    //   eventId:
    //     The event id associated with the log.
    //
    //   exception:
    //     The exception to log.
    //
    //   message:
    //     Format string of the log message in message template format. Example:
    //     "User {User} logged in from {Address}"
    //
    //   args:
    //     An object array that contains zero or more objects to format.
    Task LogTrace(EventId eventId, Exception exception, string message, params object[] args);
    //
    // Summary:
    //     Formats and writes a warning log message.
    //
    // Parameters:
    //   logger:
    //     The Microsoft.Extensions.Logging.ILogger to write to.
    //
    //   eventId:
    //     The event id associated with the log.
    //
    //   message:
    //     Format string of the log message in message template format. Example:
    //     "User {User} logged in from {Address}"
    //
    //   args:
    //     An object array that contains zero or more objects to format.
    Task LogWarning(EventId eventId, string message, params object[] args);
    //
    // Summary:
    //     Formats and writes a warning log message.
    //
    // Parameters:
    //   logger:
    //     The Microsoft.Extensions.Logging.ILogger to write to.
    //
    //   eventId:
    //     The event id associated with the log.
    //
    //   exception:
    //     The exception to log.
    //
    //   message:
    //     Format string of the log message in message template format. Example:
    //     "User {User} logged in from {Address}"
    //
    //   args:
    //     An object array that contains zero or more objects to format.
    Task LogWarning(EventId eventId, Exception exception, string message, params object[] args);
    //
    // Summary:
    //     Formats and writes a warning log message.
    //
    // Parameters:
    //   logger:
    //     The Microsoft.Extensions.Logging.ILogger to write to.
    //
    //   message:
    //     Format string of the log message in message template format. Example:
    //     "User {User} logged in from {Address}"
    //
    //   args:
    //     An object array that contains zero or more objects to format.
    Task LogWarning(string message, params object[] args);
    //
    // Summary:
    //     Formats and writes a warning log message.
    //
    // Parameters:
    //   logger:
    //     The Microsoft.Extensions.Logging.ILogger to write to.
    //
    //   exception:
    //     The exception to log.
    //
    //   message:
    //     Format string of the log message in message template format. Example:
    //     "User {User} logged in from {Address}"
    //
    //   args:
    //     An object array that contains zero or more objects to format.
    Task LogWarning(Exception exception, string message, params object[] args);
}
