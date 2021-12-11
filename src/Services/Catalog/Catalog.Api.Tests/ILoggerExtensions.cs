using Microsoft.Extensions.Logging;
using Moq;
using System;

namespace Catalog.Api.Tests
{
    internal static class ILoggerExtensions
    {
        public static Mock<ILogger<T>> VerifyLogging<T>(this Mock<ILogger<T>> logger, string expectedMessage, LogLevel expectedLogLevel = LogLevel.Debug, Times? times = null)
        {
            times ??= Times.Once();

#pragma warning disable CS8602 // Dereference of a possibly null reference.
            Func<object, Type, bool> state = (v, t) => v!.ToString().CompareTo(expectedMessage) == 0;
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            logger.Verify(
                x => x.Log(
                    It.Is<LogLevel>(l => l == expectedLogLevel),
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => state(v, t)),
                    It.IsAny<Exception>(),
#pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
                    It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)), (Times)times);
#pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.

            return logger;
        }
    }
}
