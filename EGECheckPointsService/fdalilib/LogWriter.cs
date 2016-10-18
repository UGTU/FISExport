using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fdalilib
{
    /// <summary>
    /// Класс для записи лог-файлов
    /// </summary>
    public static class LogWriter
    {
        private static object _syncRoot = new Object();
        private static TextWriter _logWriter;
        public static TextWriter Logger
        {
            get
            {
                if (_logWriter != null) return _logWriter;

                lock (_syncRoot)
                {
                    _logWriter = File.CreateText("OutLogs/" + DateTime.Now.ToString("dd-MM-yyyy HH-mm-ss ") + "log.txt");
                }
                return _logWriter;
            }
        }
        /// <summary>
        /// Добавления сообщения в лог
        /// </summary>
        /// <param name="message">текст сообщения</param>
        public static void MakeLog(string message)
        {
            Logger.WriteLine(message);
            Logger.Flush();
        }

        public static void WriteToNext()
        {
            _logWriter.Close();
            _logWriter = null;
        }
    }
}
