using System;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace Framework
{
    // 2018-06-12 added public static DialogResult Question(string prompt, string caption="Question")

    public class BaseUtils
    {
        public Logger Logger = new Logger();

        public BaseUtils()
        {
        }


        //http://stackoverflow.com/questions/7791710/convert-hex-code-to-color-name#7791803
        public static Color GetSystemDrawingColorFromHexString(string hexString)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch(hexString, @"[#]([0-9]|[a-f]|[A-F]){6}\b"))
                throw new ArgumentException();
            int red = int.Parse(hexString.Substring(1, 2), NumberStyles.HexNumber);
            int green = int.Parse(hexString.Substring(3, 2), NumberStyles.HexNumber);
            int blue = int.Parse(hexString.Substring(5, 2), NumberStyles.HexNumber);
            return Color.FromArgb(red, green, blue);
        }

        public static bool ValidateColorString(string colorHexCode)
        {
            if (!colorHexCode.StartsWith("#"))
            {
                return false;
            }
            if (colorHexCode.Length != 7)
            {
                return false;
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(colorHexCode, @"[#]([0-9]|[a-f]|[A-F]){6}\b"))
            {
                return false;
            }
            return true;
        }

        public static void GeometryFromString(string thisWindowGeometry, Form formIn)
        {
            try
            {
                if (string.IsNullOrEmpty(thisWindowGeometry))
                {
                    return;
                }
                string[] numbers = thisWindowGeometry.Split('|');
                string windowString = numbers[4];
                if (windowString == "Normal")
                {
                    Point windowPoint = new Point(int.Parse(numbers[0]),
                        int.Parse(numbers[1]));
                    Size windowSize = new Size(int.Parse(numbers[2]),
                        int.Parse(numbers[3]));

                    if (ScreensGeometryToString() == numbers[7])
                    {
                        formIn.Location = windowPoint;
                        formIn.Size = windowSize;
                        formIn.StartPosition = FormStartPosition.Manual;
                        formIn.WindowState = FormWindowState.Normal;
                    }
                }
                else if (windowString == "Maximized")
                {
                    formIn.Location = new Point(int.Parse(numbers[5]), int.Parse(numbers[6]));
                    formIn.StartPosition = FormStartPosition.Manual;
                    formIn.WindowState = FormWindowState.Maximized;
                }
            }
            catch (Exception ex)
            {
                string s = ex.Message;
                return;
            }
        }

        public static string GeometryToString(Form form1)
        {
            if (form1 == null)
            {
                throw new Exception("Invalid parameter - Form is null");
            }
            // ReSharper disable SpecifyACultureInStringConversionExplicitly
            return form1.Location.X.ToString() + "|" +   //0
                form1.Location.Y.ToString() + "|" +      //1
                form1.Size.Width.ToString() + "|" +      //2
                form1.Size.Height.ToString() + "|" +     //3
                form1.WindowState.ToString() + "|" +     //4
                (form1.Location.X + form1.Size.Width / 2).ToString() + "|" +  //5
                (form1.Location.Y + form1.Size.Height / 2).ToString() + "|" + //6
                ScreensGeometryToString();                  //7
                                                            // ReSharper restore SpecifyACultureInStringConversionExplicitly
        }

        public static int RandomInRange(int minVal, int maxVal)
        {
            Random rand = new Random();
            int Value = rand.Next(minVal, maxVal);
            return Value;

        }

        public static string ScreensGeometryToString()
        {
            string screensGeometry = "";
            // ReSharper disable LoopCanBeConvertedToQuery
            foreach (Screen s in Screen.AllScreens)
                // ReSharper restore LoopCanBeConvertedToQuery
                screensGeometry += s.WorkingArea;
            return screensGeometry;
        }

        public static Rectangle GetDesktopBounds()
        {
            var l = int.MaxValue;
            var t = int.MaxValue;
            var r = int.MinValue;
            var b = int.MinValue;
            foreach (var screen in Screen.AllScreens)
            {
                if (screen.Bounds.Left < l) l = screen.Bounds.Left;
                if (screen.Bounds.Top < t) t = screen.Bounds.Top;
                if (screen.Bounds.Right > r) r = screen.Bounds.Right;
                if (screen.Bounds.Bottom > b) b = screen.Bounds.Bottom;
            }
            return Rectangle.FromLTRB(l, t, r, b);
        }

        public static string AppDataFolder()
        {
            string localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            return localAppData;
        }

        public static string GetProgramFolder()
        {
            string temp = Environment.GetCommandLineArgs()[0];
            temp = Path.GetDirectoryName(temp);
            return temp;
        }

        public static string GetProgramName()
        {
            string temp = Environment.GetCommandLineArgs()[0];
            return temp;
        }

        public void HandleException(string ExceptionDescription, Exception ex)
        {
            if (ExceptionDescription == null) throw new ArgumentNullException(ExceptionDescription);
            if (ex == null) throw new ArgumentNullException("ex");
            LogMsg(string.Format("HandleException() at [{0}] Exception [{1}]", ExceptionDescription, ex.Message));
        }

        public void HandleExceptionWithMessage(string ExceptionDescription, string caption, Exception ex)
        {
            if (ExceptionDescription == null) throw new ArgumentNullException(ExceptionDescription);
            if (ex == null) throw new ArgumentNullException("ex");
            string msg = string.Format("HandleException() at [{0}] Exception [{1}]", ExceptionDescription, ex.Message);
            LogMsg(msg);
            MessageBox.Show(msg, caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private static readonly bool _debugIsEnabled = true;

        public static void Debug(string msg)
        {
            if (!_debugIsEnabled) return;
            Console.WriteLine("{0}: {1}", DateTime.Now.ToString("HH:mm:ss.fff"), msg);
        }

        public static void Debug(string format, string argument1)
        {
            Debug(string.Format(format, argument1));
        }

        private static void Debug(string format, object argument1, object argument2)
        {
            Debug(string.Format(format, argument1, argument2));
        }

        private static void Debug(string format, object argument1, object argument2, object argument3)
        {
            Debug(string.Format(format, argument1, argument2, argument3));
        }

        private void LogMsg(string msg)
        {
            Logger.LogMsg(msg);
        }

        public static DateTime GetNISTDate(bool convertToLocalTime)
        {
            Random ran = new Random(DateTime.Now.Millisecond);
            DateTime date = DateTime.Today;
            string serverResponse = string.Empty;

            // Represents the list of NIST servers
            string[] servers = new string[] {
                        "64.90.182.55",
                        "206.246.118.250",
                        "207.200.81.113",
                        "128.138.188.172",
                        "64.113.32.5",
                        "64.147.116.229",
                        "64.125.78.85",
                        "128.138.188.172"
                        };

            // Try each server in random order to avoid blocked requests due to too frequent request
            for (int i = 0; i < 5; i++)
            {
                try
                {
                    // Open a StreamReader to a random time server
                    StreamReader reader = new StreamReader(new System.Net.Sockets.TcpClient(servers[ran.Next(0, servers.Length)], 13).GetStream());
                    serverResponse = reader.ReadToEnd();
                    reader.Close();

                    // Check to see that the signiture is there
                    if (serverResponse.Length > 47 && serverResponse.Substring(38, 9).Equals("UTC(NIST)"))
                    {
                        // Parse the date
                        int jd = int.Parse(serverResponse.Substring(1, 5));
                        int yr = int.Parse(serverResponse.Substring(7, 2));
                        int mo = int.Parse(serverResponse.Substring(10, 2));
                        int dy = int.Parse(serverResponse.Substring(13, 2));
                        int hr = int.Parse(serverResponse.Substring(16, 2));
                        int mm = int.Parse(serverResponse.Substring(19, 2));
                        int sc = int.Parse(serverResponse.Substring(22, 2));

                        if (jd > 51544)
                            yr += 2000;
                        else
                            yr += 1999;

                        date = new DateTime(yr, mo, dy, hr, mm, sc);

                        // Convert it to the current timezone if desired
                        if (convertToLocalTime)
                            date = date.ToLocalTime();

                        // Exit the loop
                        break;
                    }

                }
                catch (Exception ex)
                {
                    string s = ex.Message;
                    /* Do Nothing...try the next server */
                }
            }

            return date;
        }

        public static DialogResult Question(string prompt, string caption="Question")
        {
            DialogResult result = System.Windows.Forms.MessageBox.Show(prompt, caption, System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question);
            return result;
        }

        public static bool PlaySound(string pathToFile)
        {
            if (!File.Exists(pathToFile)) return false;

            Thread thread = new Thread(PlaySoundThreadUsingFile);
            thread.Start(pathToFile);
            return true;
        }

        public static void PlaySoundThreadUsingFile(Object state)
        {
            try
            {
                string filename = (string)state;
                if (!File.Exists(filename)) return;

                Stream stream = new FileStream(filename, FileMode.Open);
                using (System.Media.SoundPlayer player = new System.Media.SoundPlayer(stream))
                {
                    player.PlaySync();
                }
                stream.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception("fwHelper.PlaySoundThreadUsingFile() : Exception [" + ex.Message + "]");
            }
        }


        #region TimeStamp
        public static string TimeStamp_ms()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmss.fff");
        }

        public static string TimeStamp_ms_fmt()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        }
        #endregion TimeStamp

        #region codalib Helper.cs
        #region save data file
        public string SaveDataFile(string body)
        {
            string filename = TimeStamp_ms() + ".dat";
            string result = SaveDataFile(body, filename);
            return result;
        }

        public string SaveDataFileWithExt(string body, string ext)
        {
            string filename = TimeStamp_ms() + "." + ext;
            string result = SaveDataFile(body, filename);
            return result;
        }

        public string SaveDataFile(string body, string filename)
        {
            filename = CleanupFileName(filename);
            filename = Path.Combine(PersistentSettings.DataFolder, filename);

            try
            {
                if (!Directory.Exists(PersistentSettings.DataFolder))
                {
                    Directory.CreateDirectory(PersistentSettings.DataFolder);
                }

                using (StreamWriter writer = File.CreateText(filename))
                {
                    writer.Write(body);
                    writer.Close();
                }
                return filename;
            }
            catch (Exception ex)
            {
                HandleException("SaveDataFile()", ex);
                return "";
            }
        }

        public static string CleanupFileName(string filename)
        {
            string result = filename.Replace("://", "||");
            result = filename.Replace(":", "-");
            result = result.Replace("\\", "|");
            result = result.Replace("/", "|");
            result = result.Replace("|", "!");

            result = string.Join("_", result.Split(Path.GetInvalidFileNameChars()));
            //result = result.Replace("'", "");
            //result = result.Replace("\"", "");
            //result = result.Replace("\'", "");
            //result = result.Replace("‘", "");
            //result = result.Replace("’", "");
            //result = result.Replace("?", "");

            return result;
        }
        #endregion save data file

        static public int GetIEMajorVersion()
        {
            string ieFileName = "C:\\Program Files\\Internet Explorer\\iexplore.exe";
            FileVersionInfo ver = FileVersionInfo.GetVersionInfo(ieFileName);

            if (ver != null)
            {
                return ver.FileMajorPart;
            }
            return -1;
        }

        public static string SelectFolderDialog(string prompt, string defaultFolder)
        {
            FolderBrowserDialog folder = new FolderBrowserDialog();
            folder.SelectedPath = defaultFolder;
            folder.Description = prompt;            
            if (folder.ShowDialog() == DialogResult.OK)
            {
                return folder.SelectedPath;
            }
            return defaultFolder;
        }

        public static string SelectFileDialog(string prompt, string defaultFile)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.FileName = defaultFile;
            open.Title = prompt;
            if (open.ShowDialog() == DialogResult.OK)
            {
                return open.FileName;
            }
            return defaultFile;
        }
        #endregion codalib Helper.cs        

        #region DateTime utils

        public static DateTime GetNistTime()
        {
            DateTime dateTime = DateTime.MinValue;

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://nist.time.gov/actualtime.cgi?lzbc=siqm9b");
            request.Method = "GET";
            request.Accept = "text/html, application/xhtml+xml, */*";
            request.UserAgent = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.1; Trident/6.0)";
            request.ContentType = "application/x-www-form-urlencoded";
            request.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore); //No caching
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                StreamReader stream = new StreamReader(response.GetResponseStream());
                string html = stream.ReadToEnd();//<timestamp time=\"1395772696469995\" delay=\"1395772696469995\"/>
                string time = Regex.Match(html, @"(?<=\btime="")[^""]*").Value;
                double milliseconds = Convert.ToInt64(time) / 1000.0;
                dateTime = new DateTime(1970, 1, 1).AddMilliseconds(milliseconds).ToLocalTime();
            }

            return dateTime;
        }
        #endregion
    }
}
