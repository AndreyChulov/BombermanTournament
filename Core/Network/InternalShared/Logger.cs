using System.IO.Compression;
using System.Runtime.CompilerServices;
using Core.Network.Shared;

namespace Core.Network.InternalShared
{
    public static class Logger
    {
        private static FileStream _logFileStream;
        private static TextWriter _logWriter;
        private static string _logFileName;
        private static object _logFileLockObject = new object();

        public static void AddVerboseMessage(string message, [CallerFilePath] string callerFilePath = "")
        {
            var callerClassName = Path.GetFileNameWithoutExtension(callerFilePath);

            lock (_logFileLockObject)
            {
                _logWriter.WriteLine($@"[{DateTime.Now.ToLongTimeString()}] -> [{callerClassName}] ->{message}");
                _logWriter.Flush();
            }

            SaveLogFileToArchive();
        }

        private static void SaveLogFileToArchive()
        {
            var fileSize = (new FileInfo(_logFileName)).Length;
            var fileSizeInMegabytes = fileSize / 1024 / 1024;

            if (fileSizeInMegabytes < NetworkSettings.MaxUnarchivedLogFileSize)
            {
                return;
            }
            
            lock (_logFileLockObject)
            {
                _logWriter.Close();
                _logFileStream.Close();

                var archiveFileName = Path.Join(
                    Path.GetDirectoryName(_logFileName),
                    $"{Path.GetFileNameWithoutExtension(_logFileName)}_{DateTime.Now.ToShortDateString()}.zip");
                
                
                using (var archiveStream = File.Open(archiveFileName, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                using (var archive = new ZipArchive(archiveStream, ZipArchiveMode.Update))    
                using (var archiveFileStream = archive.CreateEntry($"{DateTime.Now.ToLongTimeString()}.txt").Open())        
                using (var sourceFileStream = File.Open(_logFileName, FileMode.Open))
                {
                    sourceFileStream.CopyTo(archiveFileStream, 1024);
                    archiveFileStream.Flush();
                }
                
                _logFileStream = new FileStream(_logFileName, FileMode.Create, FileAccess.Write);
                _logWriter = new StreamWriter(_logFileStream);
            }
        }

        [Obsolete($"Please use {nameof(AddVerboseMessage)} instead of {nameof(AddTypedVerboseMessage)}")]
        public static void AddTypedVerboseMessage(Type type, string message)
        {
            lock (_logFileLockObject)
            {
                _logWriter.WriteLine($@"[{DateTime.Now.ToLongTimeString()}] -> [{type.Name}] ->{message}");
                _logWriter.Flush();
                
                SaveLogFileToArchive();
            }
        }

        public static void Initialize(string[] logFileNames)
        {
            foreach (var logFileName in logFileNames)
            {
                try
                {
                    Initialize(logFileName);
                    return;
                }
                catch (IOException e)
                {
                    var fileName = Path.GetFileName(logFileName);
                    if (!e.Message.Contains(fileName))
                    {
                        throw;
                    }
                }
            }
        }
        
        public static void Initialize(string logFileName)
        {
            _logFileName = logFileName;
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