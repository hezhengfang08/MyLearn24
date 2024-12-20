﻿
using Quartz.Logging;


namespace MySelf.Zhihu.Infrastructure.Quartz
{
    public class ConsoleLogProvider : ILogProvider
    {
        public Logger GetLogger(string name)
        {
            return (level, func, exception, parameters) =>
            {
                if (level >= LogLevel.Info && func != null)
                {
                    Console.WriteLine($"[{DateTime.Now.ToLongTimeString()}][{level}]{func()}", parameters);
                    // Console.WriteLine("[" + DateTime.Now.ToLongTimeString() + "] [" + level + "] " + func(), parameters);
                }
                return true;
            };
        }

        public IDisposable OpenNestedContext(string message)
        {
            return null!;
        }

        public IDisposable OpenMappedContext(string key, object value, bool destructure = false)
        {
            return null!;
        }
    }
}