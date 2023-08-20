using System.Runtime.CompilerServices;

namespace Core.Network.InternalShared
{
    public static class Logger
    {
        private static FileStream _logFileStream;
        private static TextWriter _logWriter;
        
        public static void AddVerboseMessage(string message, [CallerFilePath] string callerFilePath = "")
        {
            var callerClassName = Path.GetFileNameWithoutExtension(callerFilePath);
            _logWriter.WriteLine($@"[{DateTime.Now.ToLongTimeString()}] -> [{callerClassName}] ->{message}");
            _logWriter.Flush();
        }
        
        [Obsolete($"Please use {nameof(AddVerboseMessage)} instead of {nameof(AddTypedVerboseMessage)}")]
        public static void AddTypedVerboseMessage(Type type, string message)
        {
            
            _logWriter.WriteLine($@"[{DateTime.Now.ToLongTimeString()}] -> [{type.Name}] ->{message}");
            _logWriter.Flush();
        }

        public static void Initialize(string logFileName)
        {
            var logFolder = Path.GetDirectoryName(logFileName);

            if (logFolder != null && !Directory.Exists(logFolder))
            {
                Directory.CreateDirectory(logFolder);
            }
            
            _logFileStream = new FileStream(logFileName, FileMode.Create, FileAccess.Write);
            _logWriter = new StreamWriter(_logFileStream);
        }

        public static void FreeUpResources()
        {
            _logWriter.Dispose();
            _logFileStream.Dispose();
        }

    }
}