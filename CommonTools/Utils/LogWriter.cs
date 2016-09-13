using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CommonTools.Utils
{
    public sealed class LogWriter : IDisposable
    {
        #region Singleton
        private static readonly Lazy<LogWriter> _instance = new Lazy<LogWriter>(() => new LogWriter());
        public static LogWriter Instance => _instance.Value;
        #endregion

        private StreamWriter _logWriter;
        private long fileCount = 1;
        //private int nfileCount = 1;
        private static string _logPath = $"{AppDomain.CurrentDomain.BaseDirectory}//LOGS//";
        private string _logFilename = string.Empty;
        private string _logXmlFilename = string.Empty;
        private long _logFileSize = 2097152;

        /// <summary>
        /// Write line to logFile in a different Task
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task WriteLogAsync(string message, CancellationToken cancellationToken)
        {
            await Task.Run(() => { WriteLog(message); }, cancellationToken);
        }

        /// <summary>
        /// Write line to logFile
        /// </summary>
        /// <param name="message"></param>
        public void WriteLog(string message)
        {
            try
            {
                while (true)
                {
                    if (!File.Exists(_logFilename))
                    {
                        _logWriter = new StreamWriter(_logFilename);
                        break;
                    }
                    else
                    {
                        if (_logFileSize == 0)
                            _logFileSize = 2097152;
                        FileInfo fi = new FileInfo(_logFilename);
                        if (fi.Length > _logFileSize)
                        {
                            fileCount++;
                            _logFilename = _logPath + "\\" + DateTime.Now.ToString("yyyyMMdd") + "_" + fileCount + ".txt";
                            continue;
                        }
                        else
                        {
                            _logWriter = File.AppendText(_logFilename);
                            break;
                        }
                    }
                }

                _logWriter.WriteLine(DateTime.Now.ToString("g") + ": " + message);
                _logWriter.Flush();
                _logWriter.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Method to configure the directory where logs are saved.
        /// Default path is AppDomain.CurrentDomain.BaseDirectory/LOGS/
        /// </summary>
        /// <param name="logPath"></param>
        public void ConfigureLogWriter(string logPath = null, string logFilename = null)
        {
            if (String.IsNullOrEmpty(logPath))
            {
                _logPath = $"{AppDomain.CurrentDomain.BaseDirectory}//LOGS//";
            }
            else
            {
                //if (!Directory.Exists(logPath.Split('\\').Last()))
                //    Directory.CreateDirectory(logPath.Split('\\').Last());
                if (!Directory.Exists(_logPath))
                    Directory.CreateDirectory(_logPath);

                _logPath = logPath;
            }

            if (string.IsNullOrEmpty(_logFilename))
            {
                _logFilename = $"{_logPath}\\{DateTime.Now.ToString("yyyyMMdd")}.Log.{fileCount}.txt";
            }
            else
            {
                _logFilename = $"{_logPath}\\logFilename";
            }


        }

        public void Dispose()
        {
            this._logWriter.Close();
            GC.Collect();
        }


        public static void WriteQuickMessage(string message, string logPath = "\\QuickMessage.txt")
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter($"{AppDomain.CurrentDomain.BaseDirectory}{logPath}", true))
            {
                file.WriteLine(message);
            }
        }
    }
}
