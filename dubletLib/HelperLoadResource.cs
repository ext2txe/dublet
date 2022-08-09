using System.Reflection;
using System.Text;
using System;
using System.IO;

namespace dubletLib
{
    public static class HelperLoadResource
    {
        public static string ReadResource(string filename)
        {
            //use UTF8 encoding as the default encoding
            return ReadResource(filename, Encoding.UTF8);
        }

        public static string ReadResource(string filename, Encoding fileEncoding)
        {
            string fqResourceName = string.Empty;
            string result = string.Empty;

            //get executing assembly
            Assembly execAssembly = Assembly.GetExecutingAssembly();

            //get resource names
            string[] resourceNames = execAssembly.GetManifestResourceNames();

            if (resourceNames != null && resourceNames.Length > 0)
            {
                foreach (string rName in resourceNames)
                {
                    if (rName.EndsWith(filename))
                    {

                        //set value to 1st match
                        //if the same filename exists in different folders,
                        //the filename can be specified as <folder name>.<filename>
                        //or <namespace>.<folder name>.<filename>
                        fqResourceName = rName;

                        //exit loop
                        break;
                    }
                }

                //if not found, throw exception
                if (String.IsNullOrEmpty(fqResourceName))
                {
                    throw new Exception($"Resource '{filename}' not found.");
                }

                //get file text
                using (Stream s = execAssembly.GetManifestResourceStream(fqResourceName))
                {
                    using (StreamReader reader = new StreamReader(s, fileEncoding))
                    {
                        //get text
                        result = reader.ReadToEnd();
                    }
                }
            }

            return result;
        }

    }
}
