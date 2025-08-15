using Serilog;

namespace Logger
{
    public static class Recorder
    {
        //Serilog core
        private static ILogger Logger;

        //Log inicialization
        public static void Initialize()
        {
            //Setting up log rules
            Logger = new LoggerConfiguration()
                //Minimum level set to Debug. It'll log everything from Debug and above (Debug, Information, Warning, Error, Fatal)
                .MinimumLevel.Debug()
                //Output log on console
                .WriteTo.Console()
                //Output log to a file. RollingInterval set to Day, meaning a new log file will be created every day
                .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day)
                //Create the logger instance
                .CreateLogger();
            //A message to indicate that the logger has been initialized. Appearing in both console and log file
            Logger?.Information("Logger initialized.");
        }

        //Close the logger and flush any buffered log messages when the application is closing/shutting down
        public static void Shutdown()
        {
            //Log a shutdown message
            //Logger?.Information("Logger shutting down.");
            //Flush any buffered log entries
            Log.CloseAndFlush();
        }

        //Informational message
        public static void WriteInfo(String message)
        {
            Logger?.Information(message);
        }

        //Warning message
        //public static void WriteWarning(String message)
        //{
        //    //Write warning message to all sinks
        //    Logger?.Warning(message);
        //}

        //Log error message. Exception is optional, if provided it will log the exception details. If an exception is provided, it logs both the message and the exception. If not, it just logs the message.
        public static void WriteError(String message, Exception? ex = null)
        {
            if (ex != null)
                Logger?.Error(ex, message);
            else
                Logger?.Error(message);
        }
    }
}
