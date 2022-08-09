using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;

namespace Framework
{
    public partial class Settings : BaseSettings
    {
        public const string DefaultProjectvalueame = "base";

        /// <summary>
        /// Default settings file, automatically creates {0}.ini file in local application data folder using Projectvalueame
        /// e.g. current project will have default settings ini file located in C:\Users\<user accout>\AppData\local\spfserver\spfserver.ini
        /// </summary>
        /// <returns>path to default settings file (.ini)</returns>
        public static string MakePathToSettings()
        {
            string iniFilevalueame = string.Format("{0}.ini", DefaultProjectvalueame.ToLower());
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DefaultProjectvalueame.ToLower());
            path = Path.Combine(path, iniFilevalueame);
            return path;
        }

        public static string MakeSettingsPath(string projectvalueame)
        {
            string iniFilevalueame = string.Format("{0}.ini", projectvalueame.ToLower());
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), projectvalueame.ToLower());
            path = Path.Combine(path, iniFilevalueame);
            return path;
        }

        /// <summary>
        /// Create settings file using pre determined path.
        /// in the local AppData folder
        /// </summary>
        public Settings(string iniFile) : base(iniFile)
        {

        }


        public string PathToLogFile { get { return GetString("PathToLogFile"); } set { SetString("PathToLogFile", value); } }
        public string PathToPageDefinitionsFolder { get { return GetString("PathToPageDefinitionsFolder"); } set { SetString("PathToPageDefinitionsFolder", value); } }

        /// <summary>
        /// ColorTolerance, the maximum difference between any sample color (R,G,B) to differ from the reference to 
        /// still be considered the same.
        /// </summary>
        public int ColorTolerance { get { return GetInt("ColorTolerance", 30); } set { SetInt("ColorTolerance", value); } }

        public bool TesseractUseEnlargedSourceImage { get { return GetBoolean("TesseractUseEnlargedSourceImage", true); } set { SetBoolean("TesseractUseEnlargedSourceImage", value); } }


        public string TesseractParentDirectory { get { return GetString("TesseractParentDirectory"); } set { SetString("TesseractParentDirectory", value); } }

        
        public string OCRTestImage  { get { return GetString("OCRTestImage", "Project Name"); } set { SetString("OCRTestImage", value); } }

        /// <summary>
        /// like it say on the packet
        /// </summary>
        public int OCRThreshold{ get { return GetInt("OCRThreshold", 164); } set { SetInt("OCRThreshold", value); } }



        public Point ConsoleWindowLeftTop { get { return GetPoint("ConsoleWindowLeftTop", new Point(100, 100)); } set { SetPoint("ConsoleWindowLeftTop", value); } }
        public string Projectvalueame { get { return GetString("Projectvalueame", "Project Name"); } set { SetString("Projectvalueame", value); } }
        public string ImageDLL { get; set; }

        #region jcpoc Settings
        /// 
        /// like it says
        ///
        public bool EnableSpinClickLimit { get { return GetBoolean("EnableSpinClickLimit", false); } set { SetBoolean("EnableSpinClickLimit", value); } }

        /// <summary>
        ///  number of clicks to execute before stopping
        /// </summary>
        public int SpinCountClickLimit { get { return GetInt("SpinCountClickLimit", 500); } set { SetInt("SpinCountClickLimit", value); } }

        /// <summary>
        /// number of clicks since last reset
        /// </summary>
        public int CurrentSpinClickCount { get { return GetInt("CurrentSpinClickCount", 0); } set { SetInt("CurrentSpinClickCount", value); } }

        /// <summary>
        /// like it say on the packet
        /// </summary>
        public bool EnableDelayAfterSpinButtonClick { get { return GetBoolean("EnableDelayAfterSpinButtonClick", true); } set { SetBoolean("EnableDelayAfterSpinButtonClick", value); } }
        /// <summary>
        /// Delay in ms after clicking the Spin Button, to avoid multiple clicks on the same ready button
        /// </summary>
        public int DelayAfterSpinButtonClickMS { get { return GetInt("DelayAfterSpinButtonClickMS", 200); } set { SetInt("DelayAfterSpinButtonClickMS", value); } }

        /// <summary>
        /// like it say on the packet
        /// </summary>
        public bool EnableDelayBeforeSpinButtonClick { get { return GetBoolean("EnableDelayBeforeSpinButtonClick", true); } set { SetBoolean("EnableDelayBeforeSpinButtonClick", value); } }
        /// <summary>
        /// Delay in ms before clicking on the Spin Button once Spin Button Ready is detected
        /// </summary>
        public int DelayBeforeSpinButtonClickMS { get { return GetInt("DelayBeforeSpinButtonClickMS", 200); } set { SetInt("DelayBeforeSpinButtonClickMS", value); } }


        /// <summary>
        /// After spin click return to the previous mouse position
        /// </summary>
        public bool ReturnToStartPositionAfterSpinClick { get { return GetBoolean("ReturnToStartPositionAfterSpinClick", false); } set { SetBoolean("ReturnToStartPositionAfterSpinClick", value); } }


        /// <summary>
        /// Delay in ms after spin click before returning to previous mouse position      
        /// </summary>
        public int SpinClickReturnDelayUpDown { get { return GetInt("SpinClickReturnDelayUpDown", 200); } set { SetInt("SpinClickReturnDelayUpDown", value); } }


        /// <summary>
        /// Call this after executing a call to an external assembly, will create log entries in file CallScriptMethod.log, only if _settings.LogPerformanceCalls is true (by default this is false).
        /// startedAt - must be set to DateTime.Now immediately before calling process to be timed,
        /// Method - caller method (who  is making this call)
        /// ClassName - assembly name
        /// methodvalueame - method in assembly being called
        /// args[] - arguments passed to method, defaults to null
        /// </summary>
        public bool LogPerformanceCalls { get { return GetBoolean("LogPerformanceCalls", false); } set { SetBoolean("LogPerformanceCalls", value); } }

        //public int StartColorSampleX { get { return GetInt("StartColorSampleX"); } set { SetInt("StartColorSampleX", value); } }
        //public int StartColorSampleY { get { return GetInt("StartColorSampleY"); } set { SetInt("StartColorSampleY", value); } }
        //public int StartColorA { get { return GetInt("StartColorA"); } set { SetInt("StartColorA", value); } }
        //public int StartColorR { get { return GetInt("StartColorR"); } set { SetInt("StartColorR", value); } }
        //public int StartColorG { get { return GetInt("StartColorG"); } set { SetInt("StartColorG", value); } }
        //public int StartColorB { get { return GetInt("StartColorB"); } set { SetInt("StartColorB", value); } }
        //public int StartColorTolerance { get { return GetInt("StartColorTolerance"); } set { SetInt("StartColorTolerance", value); } }


        public bool StartMinimized { get { return GetBoolean("StartMinimized", false); } set { SetBoolean("StartMinimized", value); } }
        public bool EnableDiagnostics { get { return GetBoolean("EnableDiagnostics", false); } set { SetBoolean("EnableDiagnostics", value); } }
        public bool ScreenshotMethod2 { get { return GetBoolean("ScreenshotMethod2", false); } set { SetBoolean("ScreenshotMethod2", value); } }
        private string defaultTargetWindowTitle = "Play Online Casino Games at William Hill Vegas - Google Chrome";
        private Rectangle defaultSpinButtonRectangle = new Rectangle(1049, 410, 73, 76);
        public Rectangle SpinButtonRectangle { get { return GetRectangle("SpinButtonRectangle", defaultSpinButtonRectangle); } set { SetRectangle("SpinButtonRectangle", value); } }
        private Rectangle defaultBalanceRectangle = new Rectangle(220, 706, 60, 20);
        public Rectangle BalanceRectangle { get { return GetRectangle("BalanceRectangle", defaultBalanceRectangle); } set { SetRectangle("BalanceRectangle", value); } }

        public string WindowSettings { get { return GetMultiLineString("WindowSettings"); } set { SetMultiLineString("WindowSettings", value); } }


        public bool ClickOnSpinButtonWhenReady { get { return GetBoolean("ClickOnSpinButtonWhenReady", false); } set { SetBoolean("ClickOnSpinButtonWhenReady", value); } }
        public bool ClickOnStartButtonWhenVisible { get { return GetBoolean("ClickOnStartButtonWhenVisible", false); } set { SetBoolean("ClickOnStartButtonWhenVisible", value); } }

        public bool SaveSnapshots { get { return GetBoolean("SaveSnapshots", false); } set { SetBoolean("SaveSnapshots", value); } }
        public int PollingInterval { get { return GetInt("PollingInterval", 300); } set { SetInt("PollingInterval", value); } }
        public string TargetWindowTitle { get { return GetString("TargetWindowTitle", defaultTargetWindowTitle); } set { SetString("TargetWindowTitle", value); } }

        
        public bool SaveKeyPanel { get { return GetBoolean("SaveKeyPanel", false); } set { SetBoolean("SaveKeyPanel", value); } }
        public string SaveFolder { get { return GetString("SaveFolder", ""); } set { SetString("SaveFolder", value); } }
        public string WindowGeometry { get { return GetString("WindowGeometry", ""); } set { SetString("WindowGeometry", value); } }
        public string ToolWindowGeometry { get { return GetString("ToolWindowGeometry", ""); } set { SetString("ToolWindowGeometry", value); } }
        public bool SnapshotOnClick { get { return GetBoolean("SnapshotOnClick", false); } set { SetBoolean("SnapshotOnClick", value); } }
        #endregion spfClient Settings

        #region CPOC settings
        /*
        private const string DefaultCPOCRouterUrl = "";
        public string CPOC_RouterUrl {  get { return GetString("CPOC_RouterUrl", DefaultCPOCRouterUrl); } set { SetString("CPOC_RouterUrl", value); } }

        private const int DefaultCPOCRouterResetTimeout = 10;
        public int CPOC_RouterResetTimeout { get { return GetInt("CPOC_RouterResetTimeout", DefaultCPOCRouterResetTimeout); } set { SetInt("CPOC_RouterResetTimeout", value); } }

        public bool CPOC_StopAllBrowsers { get { return GetBoolean("CPOC_StopAllBrowsers", true); } set { SetBoolean("CPOC_StopAllBrowsers", value); } }

        private const string DefaultCPOC_RouterTurnOffButtonId = "mobilenetwork_turnOff_button";
        public string CPOC_RouterTurnOffButtonId { get { return GetString("CPOC_RouterTurnOffButtonId", DefaultCPOC_RouterTurnOffButtonId); } set { SetString("CPOC_RouterTurnOffButtonId", value); } }

        private const string DefaultCPOC_RouterTurnOnButtonId = "mobilenetwork_turnOn_button";
        public string CPOC_RouterTurnOnButtonId { get { return GetString("CPOC_RouterTurnOnButtonId", DefaultCPOC_RouterTurnOnButtonId); } set { SetString("CPOC_RouterTurnOnButtonId", value); } }


        public bool CPOC_IgnoreScriptErrors { get { return GetBoolean("CPOC_IgnoreScriptErrors", true); } set { SetBoolean("CPOC_IgnoreScriptErrors", value); } }

        private const int DefaultCPOC_TurnOnDelay = 20;
        public int CPOC_TurnOnDelay { get { return GetInt("CPOC_TurnOnDelay", DefaultCPOC_TurnOnDelay); } set { SetInt("CPOC_TurnOnDelay", value); } }
        
        private const int DefaultCPOC_RefreshDelay = 2;
        public int CPOC_RefreshDelay { get { return GetInt("CPOC_RefreshDelay", DefaultCPOC_TurnOnDelay); } set { SetInt("CPOC_RefreshDelay", value); } }
        */
        #endregion CPOC settings

        private const string DefaultUploadUrl = "https://codaland.com/screenshot/uploader.php";

        public bool EnableUpload { get { return GetBoolean("EnableUpload", false); } set { SetBoolean("EnableUpload", value); } }       
        public bool EnableBmpUpload { get { return GetBoolean("EnableBmpUpload", false); } set { SetBoolean("EnableBmpUpload", value); } }
        public string UploadUrl { get { return GetString("UploadUrl", DefaultUploadUrl); } set { SetString("UploadUrl", value); } }



        // usercontrol MyControls
        // Screenshot
        public string SkipWindowList { get { return GetMultiLineString("SkipWindowList"); } set { SetMultiLineString("SkipWindowList", value); } }
        public bool IncludeOwnWindowsInSkipList { get { return GetBoolean("IncludeOwnWindowsInSkipList", true); } set { SetBoolean("IncludeOwnWindowsInSkipList", value); } }



        public List<string> GetStringList(string multilineString)
        {
            List<string> list = new List<string>();

            string[] lines = Regex.Split(multilineString, Environment.NewLine);
            foreach (string line in lines)
            {
                if (line.Trim() != "")
                {
                    list.Add(line);
                }
            }
            return list;
        }
    }

}
