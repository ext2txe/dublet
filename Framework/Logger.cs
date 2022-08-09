using System;
using System.IO;
using System.Threading;

namespace Framework
{
    public class Logger
    {

        public LoggerDelegate LoggerDelegate = null;
        public LoggerDelegate StatusDelegate = null;
        public string LogFile { get; set; }

        public Logger()
        {
            int step = 10;
            try
            {
                LogFile = MakeDefaultLogFile();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"BaseLib.Logger.Logger() @ [{step}] - [{ex.Message}]");
            }
        }

        public static string MakeDefaultLogFile()
        {
            return MakeDefaultLogFile(DateTime.Now);
        }

        public static string MakeDefaultLogFile(string logFolder)
        {
            return MakeDefaultLogFile(DateTime.Now, logFolder);
        }

        public static string MakeDefaultLogFile(DateTime date)
        {
            string filename = string.Format("{0}_{1}.log", date.ToString("yyyyMMdd"), PersistentSettings.GetProgramName());
            filename = Path.Combine(LogFolder(), filename);
            return filename;
        }

        public static string MakeDefaultLogFile(DateTime date, string logFolder)
        {
            string filename = string.Format("{0}_{1}.log", date.ToString("yyyyMMdd"), PersistentSettings.GetProgramName());
            filename = Path.Combine(logFolder, filename);
            return filename;
        }

        public static string MakeLogFilePath(string projectName)
        {
            string iniFileName = string.Format("{0}.ini", DateTime.Now.ToString("yyyyMMdd"));
            string path = Path.Combine(LogFolder(projectName), iniFileName);
            return path;
        }

        private static string LogFolder()
        {
            string folder = Path.Combine(PersistentSettings.BaseFolder, "log");
            if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);
            return folder;
        }

        private static string LogFolder(string projectName)
        {
            string folder = Path.Combine(projectName.ToLower(), "log");
            folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), folder);

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            return folder;
        }

        public Logger(string logFile)
        {
            LogFile = logFile;
        }

        #region logging routines

        #region LogError
        public void LogError(string msg)
        {
            string m = string.Format(">>>ERROR: {0}", msg);
            LogMsg(m);
        }

        public void LogError(string format, object argument1)
        {
            LogError(string.Format(format, argument1));
        }

        public void LogError(string format, object argument1, object argument2)
        {
            LogError(string.Format(format, argument1, argument2));
        }

        public void LogError(string format, object argument1, object argument2, object argument3)
        {
            LogError(string.Format(format, argument1, argument2, argument3));
        }

        public void LogError(string format, object argument1, object argument2, object argument3, object argument4)
        {
            LogError(string.Format(format, argument1, argument2, argument3, argument4));
        }

        public void LogError(string format, object argument1, object argument2, object argument3, object argument4, object argument5)
        {
            LogError(string.Format(format, argument1, argument2, argument3, argument4, argument5));
        }

        public void LogError(string format, object argument1, object argument2, object argument3, object argument4, object argument5, object argument6)
        {
            LogError(string.Format(format, argument1, argument2, argument3, argument4, argument5, argument6));
        }
        #endregion LogError

        public void CustomLogMsg(string filename, string msg)
        {
            string m = string.Format("{0}: {1}", DateTime.Now.ToString("HH:mm:ss.fff"), msg);
            bool done = false;
            int loop = 0;
            while (!done)
            {
                try
                {
                    StreamWriter sw = new StreamWriter(filename, true);

                    sw.WriteLine(m);
                    sw.Flush();
                    sw.Close();
                    done = true;
                }
                catch (Exception ex)
                {
                    string s = ex.Message;
                    Thread.Sleep(250);
                    loop++;
                }
            }
        }

        #region LogMsg
        /// <summary>
        /// prefix log string (msg) with DateTime stamp.  If msg is blank, then
        /// no prefix is added and blank line is written to file as a separator
        /// </summary>
        /// <param name="msg"></param>
        /// <exception cref="Exception"></exception>
        public void LogMsg(string msg)
        {
            if (LoggerDelegate != null)
            {
                LoggerDelegate(msg);
                //return;
            }
            string m = string.Format("{0}: {1}", DateTime.Now.ToString("HH:mm:ss.fff"), msg);
            if (msg == "")
            {
                m = "";  // insert blank line separators if no log msg (= "")
            }

            bool done = false;
            int loop = 0;
            if (LogFile == null)
            {
                throw new Exception("Logger.LogMsg() - logfile not found or is null!");
            }
            while (!done)
            {
                try
                {
                    StreamWriter sw = new StreamWriter(LogFile, true);

                    sw.WriteLine(m);
                    sw.Flush();
                    sw.Close();
                    done = true;
                }
                catch (DirectoryNotFoundException)
                {
                    string directory = Path.GetDirectoryName(LogFile);
                    Directory.CreateDirectory(directory);
                }
                catch (Exception ex)
                {
                    string s = ex.Message;
                    Thread.Sleep(250);
                    loop++;
                }
            }
        }

        public void LogMsg(string format, object argument1)
        {
            LogMsg(string.Format(format, argument1));
        }

        public void LogMsg(string format, object argument1, object argument2)
        {
            LogMsg(string.Format(format, argument1, argument2));
        }

        public void LogMsg(string format, object argument1, object argument2, object argument3)
        {
            LogMsg(string.Format(format, argument1, argument2, argument3));
        }

        public void LogMsg(string format, object argument1, object argument2, object argument3, object argument4)
        {
            LogMsg(string.Format(format, argument1, argument2, argument3, argument4));
        }

        public void LogMsg(string format, object argument1, object argument2, object argument3, object argument4, object argument5)
        {
            LogMsg(string.Format(format, argument1, argument2, argument3, argument4, argument5));
        }

        public void LogMsg(string format, object argument1, object argument2, object argument3, object argument4, object argument5, object argument6)
        {
            LogMsg(string.Format(format, argument1, argument2, argument3, argument4, argument5, argument6));
        }
        #endregion LogMsg

        #region Status
        public void Status(string msg)
        {
            if (StatusDelegate != null)
            {
                StatusDelegate(msg);
            }
        }

        public void Status(string format, object argument1)
        {
            Status(string.Format(format, argument1));
        }

        public void Status(string format, object argument1, object argument2)
        {
            Status(string.Format(format, argument1, argument2));
        }

        public void Status(string format, object argument1, object argument2, object argument3)
        {
            Status(string.Format(format, argument1, argument2, argument3));
        }

        public void Status(string format, object argument1, object argument2, object argument3, object argument4)
        {
            Status(string.Format(format, argument1, argument2, argument3, argument4));
        }

        public void Status(string format, object argument1, object argument2, object argument3, object argument4, object argument5)
        {
            Status(string.Format(format, argument1, argument2, argument3, argument4, argument5));
        }

        public void Status(string format, object argument1, object argument2, object argument3, object argument4, object argument5, object argument6)
        {
            Status(string.Format(format, argument1, argument2, argument3, argument4, argument5, argument6));
        }
        #endregion Status

        #endregion logging routines

    }
}
