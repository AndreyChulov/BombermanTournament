using System;
using System.IO;

namespace ImageChat.Shared
{
    public static class Logger
    {
        private static FileStream _logFileStream;
        private static TextWriter _logWriter;
        
        public static void AddVerboseMessage(string message)
        {
            _logWriter.WriteLine($@"[{DateTime.Now.ToLongTimeString()}]{message}");
            _logWriter.Flush();
        }
        
        public static void AddTypedVerboseMessage(Type type, string message)
        {
            _logWriter.WriteLine($@"[{DateTime.Now.ToLongTimeString()}] -> [{type.Name}] ->{message}");
            _logWriter.Flush();
        }

        public static void Initialize(string logFileName)
        {
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