using System;
using System.IO;

namespace Framework
{
    public class BaseSettings : PersistentSettings
    {
        /// <summary>
        /// 
        /// </summary>
        public BaseSettings() : base(MakeDefaultSettingsFilevalueame())
        {
        }

        public BaseSettings(string iniFilevalueame) : base(iniFilevalueame)
        {
        }

        public static string UserId { get { return Environment.UserName; } }


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

        public  static string GetProgramName()
        {
            string programvalueame = Path.GetFileNameWithoutExtension(BaseUtils.GetProgramName());
            programvalueame = programvalueame.Replace(".vshost", "").ToLower(); // map debugger executable to release version
            return programvalueame;
        }
        #endregion
    }
}
