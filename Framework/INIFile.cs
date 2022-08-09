using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;

namespace Framework
{
    public class INIFile
    {
        #region "Declarations"

        public string AppName;  // from TMLib conversion 20180930

        // *** Lock for thread-safe access to file and local cache ***
        private readonly object _mLock = new object();

        // *** File name ***
        private string _mFileName;
        public string FileName
        {
            get
            {
                return _mFileName;
            }
        }

        // *** Lazy loading flag ***
        private bool _mLazy;

        // *** Local cache ***
        private readonly Dictionary<string, Dictionary<string, string>> _mSections = new Dictionary<string, Dictionary<string, string>>();

        // *** Local cache modified flag ***
        private bool _mCacheModified;

        public string SectionName = "";

        #endregion

        #region "Methods"

        // *** Constructor ***
        public INIFile(string fileName)
        {
            int step = 10;
            try 
            { 
            if (fileName == null) throw new ArgumentNullException("fileName");
            Initialize(fileName, false);
            }
            catch (Exception ex)
            {
                string s = ex.Message;
                Console.WriteLine($"BaseLib.INIFile.INIFile() @[{step}] - [{ex.Message}]");
                throw ex;
            }

        }

        public INIFile(string fileName, bool lazy)
        {
            Initialize(fileName, lazy);
        }

        // *** Initialization ***
        private void Initialize(string fileName, bool lazy)
        {
            int step = 10;
            try
            {
                if (fileName == null) throw new ArgumentNullException("fileName");

                string folder = Path.GetDirectoryName(fileName);
                if (folder != null && folder != "" && !Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
                SectionName = "general";
                _mFileName = fileName;
                _mLazy = lazy;
                if (!_mLazy) Refresh();
            }
            catch (Exception ex)
            {
                string s = ex.Message;
                Console.WriteLine($"BaseLib.INIFile.Initialize() @[{step}] - [{ex.Message}]");
                throw ex;
            }
        }

        // *** Read file contents into local cache ***
        public void Refresh()
        {
            lock (_mLock)
            {
                StreamReader sr = null;
                int step = 10;
                try
                {
                    // *** Clear local cache ***
                    _mSections.Clear();

                    // *** Open the INI file ***
                    try
                    {
                        sr = new StreamReader(_mFileName);
                    }
                    catch (FileNotFoundException)
                    {
                        return;
                    }

                    // *** Read up the file content ***
                    Dictionary<string, string> currentSection = null;
                    string s;
                    while ((s = sr.ReadLine()) != null)
                    {
                        s = s.Trim();

                        // *** Check for section names ***
                        if (s.StartsWith("[") && s.EndsWith("]"))
                        {
                            if (s.Length > 2)
                            {
                                string sectionName = s.Substring(1, s.Length - 2);

                                // *** Only first occurrence of a section is loaded ***
                                if (_mSections.ContainsKey(sectionName))
                                {
                                    currentSection = null;
                                }
                                else
                                {
                                    currentSection = new Dictionary<string, string>();
                                    _mSections.Add(sectionName, currentSection);
                                }
                            }
                        }
                        else if (currentSection != null)
                        {
                            // *** Check for key+Value pair ***
                            int i;
                            if ((i = s.IndexOf('=')) > 0)
                            {
                                int j = s.Length - i - 1;
                                string key = s.Substring(0, i).Trim();
                                if (key.Length > 0)
                                {
                                    // *** Only first occurrence of a key is loaded ***
                                    if (!currentSection.ContainsKey(key))
                                    {
                                        string Value = (j > 0) ? (s.Substring(i + 1, j).Trim()) : ("");
                                        currentSection.Add(key, Value);
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                     string msg = $"BaseLib.INIFile.Refresh() @[{step}] - [{ex.Message}]";
                    Console.WriteLine(msg);
                }
                finally
                {
                    // *** Cleanup: close file ***
                    if (sr != null) sr.Close();
                }
            }
        }

        // *** Flush local cache content ***
        public void Flush()
        {
            int step = 10;
            //if (!_mCacheModified) return;
            if (!_mCacheModified) return;  // moved here from 30, closing the file without any changes flushes contents

            step = 15;
            // *** Open the file ***
            StreamWriter sw = new StreamWriter(_mFileName);
            try
            {
                step = 20;
                Debug.Assert(_mLock != null, "_mLock != null");
                step = 30;
                // *** If local cache was not modified, exit ***
                step = 40;
                lock (_mLock)
                step = 50;
                _mCacheModified = false;

                // *** Cycle on all sections ***
                step = 60;
                bool first = false;
                step = 70;
                if (_mSections != null)
                {
                    step = 80;
                    foreach (KeyValuePair<string, Dictionary<string, string>> sectionPair in _mSections)
                    {
                        step = 90;
                        if (sectionPair.Value == null) continue;

                        step = 20;
                        Dictionary<string, string> section = sectionPair.Value;
                        if (first) sw.WriteLine();
                        first = true;

                        // *** Write the section name ***
                        sw.Write('[');
                        sw.Write(sectionPair.Key);
                        sw.WriteLine(']');

                        // *** Cycle on all key+Value pairs in the section ***
                        foreach (KeyValuePair<string, string> NPair in section)
                        {
                            // *** Write the key+Value pair ***
                            sw.Write(NPair.Key);
                            sw.Write('=');
                            sw.WriteLine(NPair.Value);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"BaseLib.INIFile.Flush() @ [{step}] - [{ex.Message}]");
            }
            finally
            {
                // *** Cleanup: close file ***
                sw.Close();
            }
        }

        // *** Read a Value from local cache ***
        public string GetValue(string sectionName, string key, string value)
        {
            if (sectionName == null) throw new ArgumentNullException("sectionName");
            if (key == null) throw new ArgumentNullException("key");
            if (value == null) throw new ArgumentNullException("value");
            // *** Lazy loading ***
            if (_mLazy)
            {
                _mLazy = false;
                Refresh();
            }

            lock (_mLock)
            {
                // *** Check if the section exists ***
                Dictionary<string, string> section;
                if (!_mSections.TryGetValue(sectionName, out section)) return value;

                // *** Check if the key exists ***
                string Value;
                if (!section.TryGetValue(key, out Value)) return value;

                // *** Return the found Value ***
                return Value;
            }
        }

        // *** Insert or modify a Value in local cache ***
        public void SetValue(string sectionName, string key, string Value)
        {
            if (sectionName == null) throw new ArgumentNullException("sectionName");
            if (key == null) throw new ArgumentNullException("key");
            if (Value == null) throw new ArgumentNullException("Value");

            // *** Lazy loading ***
            if (_mLazy)
            {
                _mLazy = false;
                Refresh();
            }

            lock (_mLock)
            {
                // *** Flag local cache modification ***
                _mCacheModified = true;

                // *** Check if the section exists ***
                Dictionary<string, string> section;
                if (!_mSections.TryGetValue(sectionName, out section))
                {
                    // *** If it doesn't, add it ***
                    section = new Dictionary<string, string>();
                    _mSections.Add(sectionName, section);
                }

                // *** Modify the Value ***
                if (section.ContainsKey(key)) section.Remove(key);
                section.Add(key, Value);
            }
        }

        // *** Encode byte array ***
// ReSharper disable ParameterTypeCanBeEnumerable.Local
        private string EncodeByteArray(byte[] Value)
// ReSharper restore ParameterTypeCanBeEnumerable.Local
        {
            if (Value == null) return null;

            StringBuilder sb = new StringBuilder();
            foreach (byte b in Value)
            {
                string hex = Convert.ToString(b, 16);
                int l = hex.Length;
                if (l > 2)
                {
                    sb.Append(hex.Substring(l - 2, 2));
                }
                else
                {
                    if (l < 2) sb.Append("0");
                    sb.Append(hex);
                }
            }
            return sb.ToString();
        }

        // *** Decode byte array ***
        private byte[] DecodeByteArray(string Value)
        {
            if (Value == null) return null;

            int l = Value.Length;
            if (l < 2) return new byte[] { };

            l /= 2;
            byte[] result = new byte[l];
            for (int i = 0; i < l; i++) result[i] = Convert.ToByte(Value.Substring(i * 2, 2), 16);
            return result;
        }

        // *** Getters for various types ***
        public bool GetValue(string sectionName, string key, bool value)
        {
            if (sectionName == null) throw new ArgumentNullException("sectionName");
            if (key == null) throw new ArgumentNullException("key");

            string stringN = GetValue(sectionName, key, value.ToString(CultureInfo.InvariantCulture));
            if (stringN == null)
            {
                return value;
            }
            int Value;
            if (int.TryParse(stringN, out Value)) return (Value != 0);
            return value;
        }

        public int GetValue(string sectionName, string key, int value)
        {
            if (sectionName == null) throw new ArgumentNullException("sectionName");
            if (key == null) throw new ArgumentNullException("key");
            string stringN = GetValue(sectionName, key, value.ToString(CultureInfo.InvariantCulture));
            if (stringN == null)
            {
                return value;
            }
            int Value;
            return int.TryParse(stringN, NumberStyles.Any, CultureInfo.InvariantCulture, out Value) ? Value : value;
        }

        public double GetValue(string sectionName, string key, double value)
        {
            if (sectionName == null) throw new ArgumentNullException("sectionName");
            if (key == null) throw new ArgumentNullException("key");
            string stringN = GetValue(sectionName, key, value.ToString(CultureInfo.InvariantCulture));
            if (stringN == null)
            {
                return value;
            }
            double Value;
            return double.TryParse(stringN, NumberStyles.Any, CultureInfo.InvariantCulture, out Value) ? Value : value;
        }

        public byte[] GetValue(string sectionName, string key, byte[] value)
        {
            if (sectionName == null) throw new ArgumentNullException("sectionName");
            if (key == null) throw new ArgumentNullException("key");

            string stringN = GetValue(sectionName, key, EncodeByteArray(value));
            if (stringN == null)
            {
                return null;
            }
            try
            {
                return DecodeByteArray(stringN);
            }
            catch (FormatException)
            {
                return value;
            }
        }

        // *** Setters for various types ***
        public void SetValue(string sectionName, string key, bool Value)
        {
            if (sectionName == null) throw new ArgumentNullException("sectionName");
            if (key == null) throw new ArgumentNullException("key");
            SetValue(sectionName, key, (Value) ? ("1") : ("0"));
        }

        public void SetValue(string sectionName, string key, int Value)
        {
            if (sectionName == null) throw new ArgumentNullException("sectionName");
            if (key == null) throw new ArgumentNullException("key");
            SetValue(sectionName, key, Value.ToString(CultureInfo.InvariantCulture));
        }

        public void SetValue(string sectionName, string key, double Value)
        {
            if (sectionName == null) throw new ArgumentNullException("sectionName");
            if (key == null) throw new ArgumentNullException("key");

            SetValue(sectionName, key, Value.ToString(CultureInfo.InvariantCulture));
        }

        public void SetValue(string sectionName, string key, byte[] Value)
        {
            if (sectionName == null) throw new ArgumentNullException("sectionName");
            if (key == null) throw new ArgumentNullException("key");
            if (Value == null) throw new ArgumentNullException("Value");

            SetValue(sectionName, key, EncodeByteArray(Value));
        }

        #endregion

        #region property helpers
        public string GetStringValue(string keyName)
        {
            if (keyName == null) throw new ArgumentNullException("keyName");
            return GetValue(SectionName, keyName, "");
        }

        public string GetStringValue(string keyName, string value)
        {
            if (value == null) throw new ArgumentNullException("value");
            return GetValue(SectionName, keyName, value);
        }

        public void SetStringValue(string keyName, string Value)
        {
            if (keyName == null) throw new ArgumentNullException("keyName");
            SetValue(SectionName, keyName, Value);
            Flush();
        }


        public int GetIntegerValue(string keyName)
        {
            // always need to check for the Value in the file. not what is cached
            Refresh();
            string temp = GetValue(SectionName, keyName, 0.ToString());
            int Value;
            if (!int.TryParse(temp, out Value))
            {
                Value = 0;
            }
            return Value;
        }


        public int GetIntegerValue(string keyName, int value)
        {
            if (keyName == null) throw new ArgumentNullException("keyName");
            // always need to check for the Value in the file. not what is cached
            Refresh();
            string temp = GetValue(SectionName, keyName, value.ToString());
            int Value;
            if (!int.TryParse(temp, out Value))
            {
                Value = 0;
            }
            return Value;
        }


        public void SetIntegerValue(string keyName, int Value)
        {
            if (keyName == null) throw new ArgumentNullException("keyName");
            SetValue(SectionName, keyName, Value);
            Flush();
        }


        public bool GetBoolean(string keyName)
        {
            if (keyName == null) throw new ArgumentNullException("keyName");
            string temp = GetValue(SectionName, keyName, false.ToString());
            return (true.ToString() == temp );
        }

        public bool GetBoolean(string keyName, bool value)
        {
            if (keyName == null) throw new ArgumentNullException("keyName");
            string temp = GetValue(SectionName, keyName, value.ToString());
            return (true.ToString().ToLower() == temp.ToLower());
        }

        public void SetBoolean(string keyName, bool Value)
        {
            if (keyName == null) throw new ArgumentNullException("keyName");
            SetValue(SectionName, keyName, Value.ToString());
            Flush();
        }

        #endregion property helpers

    }
}

