using System;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;

namespace Framework
{
    public class PersistentSettings
    {
        public string IniFileName;
        protected INIFile _iniFile;

        public const string DefaultProjectvalueName = "base";

        /// <summary>
        /// Group Name provides related programs with a path to common settings, if required
        /// </summary>
        public string GroupName { get { return GetString("GroupName"); } set { SetString("GroupName", value); } }


        #region BaseFolder
        public static string BaseFolder { get { return GetBaseFolder(); } set { SetBaseFolder(value); } }

        private static string _baseFolder = null;
        private static string GetBaseFolder()
        {
            if (_baseFolder != null) return _baseFolder;
            string programvalueame = Path.GetFileNameWithoutExtension(GetProgramName());
            _baseFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), programvalueame);
            return _baseFolder; ;
        }

        private static void SetBaseFolder(string value)
        {
            _baseFolder = value;
        }
        #endregion baseFolder

        #region DataFolder
        public static string DataFolder { get { return GetDataFolder(); } set { SetDataFolder(value); } }

        private static string _dataFolder = null;
        private static string GetDataFolder()
        {
            if (_dataFolder != null) return _dataFolder;
            string programvalueame = Path.GetFileNameWithoutExtension(GetProgramName());
            _dataFolder = Path.Combine(BaseFolder, programvalueame, "data");
            return _dataFolder; ;
        }

        private static void SetDataFolder(string value)
        {
            _dataFolder = value;
        }
        #endregion DataFolder


        #region useful stuff
        private static string MakeDefaultSettingsFilevalueame()
        {
            string iniFilevalueame = string.Format("{0}.ini", BaseUtils.GetProgramName().ToLower());
            string programvalueame = Path.GetFileNameWithoutExtension(GetProgramName());
            iniFilevalueame = Path.Combine(BaseFolder, iniFilevalueame);
            return iniFilevalueame;
        }

        public static string GetProgramName()
        {
            string programvalueame = Path.GetFileNameWithoutExtension(BaseUtils.GetProgramName());
            programvalueame = programvalueame.Replace(".vshost", "").ToLower(); // map debugger executable to release version
            return programvalueame;
        }

        /// <summary>
        /// Default settings file, automatically creates {0}.ini file in local application data folder using Projectvalueame
        /// e.g. current project will have default settings ini file located in C:\Users\<user accout>\AppData\local\spfserver\spfserver.ini
        /// </summary>
        /// <returns>path to default settings file (.ini)</returns>
        public static string MakePathToSettings()
        {
            string iniFilevalueame = string.Format("{0}.ini", DefaultProjectvalueName.ToLower());
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DefaultProjectvalueName.ToLower());
            path = Path.Combine(path, iniFilevalueame);
            return path;
        }

        #endregion

        public PersistentSettings(string iniFileName )
        {
            if (string.IsNullOrEmpty(iniFileName))
            {
                throw new ArgumentNullException();
            }
            try
            {
                IniFileName = iniFileName ;

                _iniFile = new INIFile(IniFileName)
                {
                    SectionName = "general" // IniFileName // use ini name to allow multiple programs to share the same settingw
                };
            }
            catch (Exception ex)
            {
                string s = ex.Message;
                throw ex;
            }
        }

        public void Save()
        {
            _iniFile.Flush();
        }

        #region Get* / Set*
        public void SetInt(string v, int value)
        {
            _iniFile.SetIntegerValue(v, value);
            _iniFile.Flush();
        }

        public int GetInt(string settingName)
        {
            return _iniFile.GetIntegerValue(settingName);
        }

        public int GetInt(string settingName, int value)
        {
            return _iniFile.GetIntegerValue(settingName, value);
        }

        public string GetString(string settingName, string value)
        {
            string temp = _iniFile.GetStringValue(settingName, value);
            return temp;
        }

        public string GetString(string settingName)
        {
            return _iniFile.GetStringValue(settingName);
        }

        public void SetString(string settingName, string value)
        {
            _iniFile.SetStringValue(settingName, value);
            _iniFile.Flush();
        }

        public string GetString(string sectionName, string settingName, string value)
        {
            string temp = _iniFile.GetValue(sectionName, settingName, value);
            return temp;
        }

        public void SetString(string sectionName, string settingName, string value)
        {
            _iniFile.SetValue(sectionName, settingName, value);
            _iniFile.Flush();
        }

        public Size GetSize(string settingName,Size value)
        {
            try
            {
                string temp = _iniFile.GetStringValue(settingName, "");
                if (temp == "") return value;
                string[] parts = Regex.Split(temp, ",");
                int w = int.Parse(parts[0]);
                int h = int.Parse(parts[1]);
                return new Size(w, h);
            }
            catch (Exception)
            {
                return value;
            }
        }

        public void SetSize(string settingName, Size value)
        {
            string temp = string.Format("{0},{1}", value.Width, value.Height);
            _iniFile.SetStringValue(settingName, temp);
            _iniFile.Flush();
        }


        public DateTime GetDateTime(string keyName, DateTime value)
        {
            string temp = GetString(keyName, value.ToString());
            if (!DateTime.TryParse(temp, out value))
            {
                value = DateTime.MinValue;
            }
            return value;
        }

        public void SetDateTime(string keyName, DateTime value)
        {
            SetString(keyName, value.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="settingName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public string GetMultiLineString(string settingName, string value = "")
        {
            string compositeLine = _iniFile.GetStringValue(settingName);
            if (compositeLine == "")
            {
                compositeLine = value;
            }
            string[] lines = Regex.Split(compositeLine, NEW_LINE_SEPARATOR);
            string multiLine = "";
            foreach (string line in lines)
            {
                multiLine += line + Environment.NewLine;
            }
            multiLine = multiLine.Substring(0, multiLine.Length - Environment.NewLine.Length);
            return multiLine;
        }

        public string GetMultiLineString(string sectionName, string settingName, string value = "")
        {
            string compositeLine = _iniFile.GetValue(sectionName, settingName, "");
            if (compositeLine == "")
            {
                compositeLine = value;
            }
            string[] lines = Regex.Split(compositeLine, NEW_LINE_SEPARATOR);
            string multiLine = "";
            foreach (string line in lines)
            {
                multiLine += line + Environment.NewLine;
            }
            multiLine = multiLine.Substring(0, multiLine.Length - Environment.NewLine.Length);
            return multiLine;
        }

        public const string NEW_LINE_SEPARATOR = "!@!@!";

        public void SetMultiLineString(string settingName, string value)
        {

            string[] lines = Regex.Split(value.Trim(), Environment.NewLine);
            string compositeLine = "";
            foreach (string line in lines)
            {
                if (string.IsNullOrEmpty(line)) continue;
                compositeLine += line + NEW_LINE_SEPARATOR;
            }
            if (compositeLine == "")
            {
                compositeLine = "";
            }
            else
            {
                compositeLine = compositeLine.Substring(0, compositeLine.Length - NEW_LINE_SEPARATOR.Length);
            }
            _iniFile.SetStringValue(settingName, compositeLine);
            _iniFile.Flush();
        }

        public void SetMultiLineString(string sectionName, string settingName, string value)
        {
            string[] lines = Regex.Split(value, Environment.NewLine);
            string compositeLine = "";
            foreach (string line in lines)
            {
                compositeLine += line + NEW_LINE_SEPARATOR;
            }
            compositeLine = compositeLine.Substring(0, compositeLine.Length - NEW_LINE_SEPARATOR.Length);
            _iniFile.SetValue(sectionName, settingName, compositeLine);
            _iniFile.Flush();
        }

        public bool GetBoolean(string settingName, bool value)
        {
            return _iniFile.GetBoolean(settingName, value);
        }

        public bool GetBoolean(string settingName)
        {
            return _iniFile.GetBoolean(settingName);
        }

        public void SetBoolean(string settingName, bool value)
        {
            _iniFile.SetBoolean(settingName, value);
            _iniFile.Flush();
        }

        public void SetPoint(string settingName, Point value)
        {
            string temp = string.Format("{0},{1}", value.X, value.Y);
            SetString(settingName, temp);
        }

        public Point GetPoint(string settingName, Point defaultValue)
        {
            try
            {
                string temp = GetString(settingName);
                if (!string.IsNullOrEmpty(temp))
                {
                    string[] parts = Regex.Split(temp, ",");
                    int x = int.Parse(parts[0]);
                    int y = int.Parse(parts[1]);

                    Point newValue = new Point(x, y);
                    return newValue;
                }
                return defaultValue;
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        public Color GetColor(string settingName)
        {
            string sColorCode = _iniFile.GetStringValue(settingName);
            int colorCode = int.Parse(sColorCode);
            //_iniFile.GetIntegerValue(settingName);
            return Color.FromArgb(colorCode);
        }

        public Color GetColor(string settingName, Color defaultColor)
        {
            int defaultColorCode = defaultColor.ToArgb();
            string sDefaultColorCode = defaultColorCode.ToString();
            string sColorCode = _iniFile.GetStringValue(settingName, sDefaultColorCode);
            //int colorCode = _iniFile.GetIntegerValue(settingName, defaultColorCode);
            int colorCode = int.Parse(sColorCode);
            return Color.FromArgb(colorCode);
        }

        public void SetColor(string settingName, Color color)
        {
            int colorCode = color.ToArgb();
            string sColorCode = colorCode.ToString();
            _iniFile.SetStringValue(settingName, sColorCode);
            _iniFile.Flush();
        }

        public Rectangle GetRectangle(string settingName, Rectangle defaultRectangle)
        {
            int step = 10;
            try
            {
                string s = _iniFile.GetStringValue(settingName);
                if (string.IsNullOrEmpty(s))
                {
                    return defaultRectangle;
                }

                string[] parts = Regex.Split(s, ",");
                if (parts.Length == 4)
                {
                    int l = int.Parse(parts[0]);
                    int t = int.Parse(parts[1]);
                    int w = int.Parse(parts[2]);
                    int h = int.Parse(parts[3]);
                    Rectangle r = new Rectangle(l, t, w, h);
                    return r;
                }
                else
                {
                    throw new Exception();
                }

            }
            catch (Exception ex)
            {
                string msg = $"BaseLib.PersistentSettings.GetRectangle({ settingName}) @ [{step}] - [{ex.Message}])";
                Console.WriteLine(msg); 
                throw new Exception(msg);
            }
        }

        public Rectangle? GetRectangle(string settingName)
        {
            try
            {
                string s = _iniFile.GetStringValue(settingName);
                if (string.IsNullOrEmpty(s))
                {
                    return null;
                }

                string[] parts = Regex.Split(s, ",");
                if (parts.Length == 4)
                {
                    int l = int.Parse(parts[0]);
                    int t = int.Parse(parts[1]);
                    int w = int.Parse(parts[2]);
                    int h = int.Parse(parts[3]);
                    Rectangle r = new Rectangle(l, t, w, h);
                    return r;
                }
                else
                {
                    throw new Exception();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SetRectangle(string settingName, Rectangle r)
        {
            string s = string.Format("{0},{1},{2},{3}", r.Left, r.Top, r.Width, r.Height);
            _iniFile.SetStringValue(settingName, s);
            _iniFile.Flush();
            return;
        }


        public string GetSample(string settingName, string defaultSample)
        {
            try
            {
                string s = _iniFile.GetStringValue(settingName);

                if (s != null)
                {
                    return s;
                }
                else
                {
                    return defaultSample;
                }
            }
            catch (Exception)
            {
                return defaultSample;
            }
        }

        public void SetSample(string settingName, string sampleAsLine)
        {
            string[] parts = sampleAsLine.Split(',');
            if (parts.Length != 5)
            {
                throw new Exception($"PersistentSettings.SetSample({settingName},{sampleAsLine}) Invalid length");
            }
            string s = string.Format("{0},{1},{2},{3},{4}", parts[0], parts[1], parts[2], parts[3], parts[4]);
            _iniFile.SetStringValue(settingName, s);
            _iniFile.Flush();
            return;
        }

        #endregion Get* / Set*

    }
}
