using Microsoft.Win32;
using System;
using System.Windows.Forms;

namespace Framework
{
    public class BaseProject
    {
        public Logger Logger = null;
        private const string DevCompanyName = "codaland";
        private const string ModuleName = "base";  // to do what is supposed to be here?
        public BaseProject()
        {
        }

        #region UserId
        private string _userId = "";
        private string GetUserId()
        {
            if (_userId == "")
            {
                _userId = GetUserFromCommandLineArguments();
                if (_userId == "")
                {
                    _userId = GetDefaultUserId();
                }
            }
            return _userId;
        }

        private void SetUserId(string Value)
        {
            _userId = Value;
            SetAppRegistrySetting(GetDefaultUserId(), Value);
        }

        public string GetUserFromCommandLineArguments()
        {
            string[] arguments = Environment.GetCommandLineArgs();
            foreach (string argument in arguments)
            {
                if (argument.StartsWith("u="))
                {
                    return argument.Substring(2).Trim();
                }
            }
            return "";
        }

        public string GetDefaultUserId()
        {
            return Environment.UserName;
        }
        #endregion UserId

        #region Registry routines
        public void SetUserRegistrySetting(string settingName, string Value, string sectionName)
        {
            string AppRegistryKey = MakeUserRegistryKey(sectionName);
            Registry.SetValue(AppRegistryKey, settingName, Value);
        }

        /// <summary>
        /// registry key is stored as combination of HKEY_CURRENT_USER\Software\projectName\section\userName\settingName
        /// 
        /// </summary>
        /// <param name="settingName"></param>
        /// <param name="sectionName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public string GetUserRegistrySetting(string settingName, string sectionName, string value)
        {
            string AppRegistryKey = MakeUserRegistryKey(sectionName);
            string Value = (string)Registry.GetValue(AppRegistryKey, settingName, value);
            if (Value == null)
            {
                return value;
            }
            return Value;

        }

        public void SetAppRegistrySetting(string settingName, string Value)
        {
            string AppRegistryKey = MakeAppRegistryKey();
            Registry.SetValue(AppRegistryKey, settingName, Value);
        }

        /// <summary>
        /// application level registry key is stored as combination of HKEY_CURRENT_USER\Software\projectName\
        /// 
        /// </summary>
        /// <param name="settingName"></param>
        /// <returns></returns>
        public string GetAppRegistrySetting(string settingName)
        {
            string AppRegistryKey = MakeAppRegistryKey();
            string Value = (string)Registry.GetValue(AppRegistryKey, settingName, null);
            return Value;

        }

        public string MakeUserRegistryKey(string sectionName)
        {
            string key = string.Format(@"HKEY_CURRENT_USER\Software\{0}\{1}\{2}\{3}", DevCompanyName, ModuleName, GetDefaultUserId(), sectionName);
            return key;

        }

        public string MakeAppRegistryKey()
        {
            string key = string.Format(@"HKEY_CURRENT_USER\Software\{0}\{1}", DevCompanyName, ModuleName);
            return key;

        }


        #endregion Registry routines

        public static void HandleException(string ExceptionDescription, Exception ex)
        {

            if (ExceptionDescription == null) throw new ArgumentNullException(ExceptionDescription);
            if (ex == null) throw new ArgumentNullException("ex");
            //L.LogMsg(string.Format("HandleException() at [{0}] Exception [{1}]", ExceptionDescription, ex.Message));
            MessageBox.Show(string.Format("HandleException() at [{0}] Exception [{1}]", ExceptionDescription, ex.Message), "BaseProject");
        }
    }
}
