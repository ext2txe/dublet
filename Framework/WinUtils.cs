using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Framework
{
    public class WinUtils
    {
        private readonly BaseUtils helpers = null; 
        public Logger Logger = null;
        public SystemWindow CurrentWindow = null;
        public Bitmap CurrentBitmap = null;

        public string Version = "0.1.3";
        public string ProjectName = "dublet";
        public WinUtils(Logger logger = null)
        {
            Logger = logger;
            helpers = new BaseUtils();
        }

#pragma warning disable IDE0060 // Remove unused parameter
        public static string GetWindowTitle(SystemWindow w)
#pragma warning restore IDE0060 // Remove unused parameter
        {
            string windowTitle = "";
            //todo ...
            return windowTitle;
        }

        public static double GetDpi(SystemWindow w)
        {
            DisplayInfo dInfo = new DisplayInfo();
            Display windowIsInDisplay = null;
            foreach (Display d in dInfo.Displays)
            {
                if (DisplayContainsWindow(d, w))
                {
                    windowIsInDisplay = d;

                    int l = (int)(w.Location.X * d.Scaling);
                    int t = (int)(w.Location.Y * d.Scaling);
                    int width = w.Size.Width;
                    int height = w.Size.Height;

                    Rectangle newWindowRectangle = new Rectangle(l, t, width, height);

                    //w.Location = new Point (l, t); //  Rectangle = newWindowRectangle;
                    //w.Size = new Size(width, height);
                    break;
                }

            }
            return windowIsInDisplay.Scaling;
        }


        public static SystemWindow[] GetVisibleWindowListAsArray()
        {
            try
            {
                List<string> skipList = WindowSkipList();
                List<SystemWindow> windows = GetVisibleWindowList(skipList);
                return windows.ToArray();
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static SystemWindow[] GetVisibleWindowListAsArray(List<string> skipList)
        {
            try
            {
                List<SystemWindow> windows = GetVisibleWindowList(skipList);
                return windows.ToArray();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static List<SystemWindow> GetVisibleWindowList(List<string> skipList)
        {
            try
            {
                List<string> skipIfStartsWith = new List<string>(StandardSkipWindows());
                List<string> skipIfEndsWith = new List<string>();
                List<string> skipIfContains = new List<string>();

                if (skipList == null) throw new Exception(string.Format("GetVisibleWindowList() - skipList cannot be null"));

                foreach (string item in skipList)
                {
                    if (item.StartsWith("*") && (item.EndsWith("*")))
                    {
                        skipIfContains.Add(item.Substring(1, item.Length - 2));
                    }
                    else if (item.StartsWith("*"))
                    {
                        skipIfStartsWith.Add(item.Substring(1));
                    }
                    else if (item.EndsWith("*"))
                    {
                        skipIfEndsWith.Add(item.Substring(0,item.Length - 1 ));
                    }
                }

                List<SystemWindow> windows = new List<SystemWindow>();
                SystemWindow[] allWindows = SystemWindow.AllToplevelWindows;
                foreach (SystemWindow w in allWindows)
                {
                    bool skipToIsNullOrEmptyext = false;
                    if (string.IsNullOrEmpty(w.Title)) continue;
                    if (w.Title.IndexOf("Main") > -1 || w.Title == "Alarms & Clock" || w.Parent.Title == "Hidden Window" )
                    {
                        //string s = "here we are";
                    }
                    if (w.Title == "Microsoft Text Input Application")
                    {
                        //string s = "here we are";
                    }
                    //Console.WriteLine(w.Title);
                    if (!w.Visible || (w.Parent).Title == "Hidden Window") // || w.Parent.Visible == false)
                    {
                        continue;
                    }
                    if (w.WindowState == FormWindowState.Minimized) continue;
                    foreach (string s in skipIfContains)
                    {
                        if (w.Title.Contains(s))
                        {
                            skipToIsNullOrEmptyext = true;
                            continue;
                        }
                    }
                    if (skipToIsNullOrEmptyext) continue;
                    foreach (string s in skipIfStartsWith)
                    {
                        if (w.Title.StartsWith(s))
                        {
                            skipToIsNullOrEmptyext = true;
                            continue;
                        }
                    }
                    if (skipToIsNullOrEmptyext) continue;
                    foreach (string s in skipIfEndsWith)
                    {
                        if (w.Title.EndsWith(s))
                        {
                            skipToIsNullOrEmptyext = true;
                            continue;
                        }
                    }
                    if (skipToIsNullOrEmptyext) continue;
                    if (skipList.Contains(w.Title))
                    {
                        skipToIsNullOrEmptyext = true;
                        continue;
                    }
                    if (skipToIsNullOrEmptyext) continue;
                    windows.Add(w);
                }
                return windows;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static bool IsCompletelyVisible(SystemWindow w)
        {
            string title = w.Title;
            if (!w.Visible) return false;
            if (w.Size.Width == 0 || w.Size.Height == 0) return false;
            //if (w.WindowAbove != null) return false;    //todo - may be an issue going forward for stacked windows
            if (w.WindowState == FormWindowState.Minimized) return false;
            if (!BaseUtils.GetDesktopBounds().Contains(w.Rectangle)) return false;
            if (title == PersistentSettings.GetProgramName()) return false;
            if (title.IndexOf("Visual Studio") > -1) return false;
            return true;
        }

        /// <summary>
        /// Looks for a window with the title, returns SystemWindow for window if found, or null if not found.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="mSecTimeout">if title window not found, will continue looking for window until mSecTimeout (default = 1second) has elapsed</param>
        /// <returns>SystemWindow - first title that matched title or null if no match found</returns>
        public static SystemWindow GetSystemWindowByTitle(string title, int mSecTimeout = 1000)
        {
            try
            {
                DateTime startAt = DateTime.Now;
                while (DateTime.Now.Subtract(startAt).TotalMilliseconds < mSecTimeout)
                {
                    List<SystemWindow> windows = new List<SystemWindow>();
                    SystemWindow[] allWindows = SystemWindow.AllToplevelWindows;
                    foreach (SystemWindow w in allWindows)
                    {
                        if (string.IsNullOrEmpty(w.Title)) continue;
                        if (!w.Visible) continue;
                        if (w.Title == title || w.Title.IndexOf(title, StringComparison.CurrentCultureIgnoreCase) > -1)
                        {
                            windows.Add(w);
                            return windows[0];
                        }
                    }
                    if (windows.Count > 0)
                    {
                        return windows[0];
                    }
                    Thread.Sleep(100);
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Looks for a window with the title, returns SystemWindow for window if found, or null if not found.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="mSecTimeout">if title window not found, will continue looking for window until mSecTimeout (default = 1second) has elapsed</param>
        /// <returns>SystemWindow - first title that matched title or null if no match found</returns>
        //public static SystemWindow GetSystemWindowByMouseOver(System.Windows.Point mouseAt)  // changed 20201111
        public static SystemWindow GetSystemWindowByMouseOver(Point mouseAt)
        {
            try
            {
                DateTime startAt = DateTime.Now;
                List<string> skipList = new List<string>(StandardSkipWindows());
                foreach (SystemWindow w in GetVisibleWindowList(skipList))
                {
                    Rectangle r = w.Rectangle;
                    Point p = new Point((int) mouseAt.X, (int) mouseAt.Y);
                    if (r.Contains(p))
                    {
                        return w;
                    }
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }


        /// <summary>
        /// Looks for a window with the title, returns SystemWindow for window if found, or null if not found.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="mSecTimeout">if title window not found, will continue looking for window until mSecTimeout (default = 1second) has elapsed</param>
        /// <returns>SystemWindow - first title that matched title or null if no match found</returns>
        public static SystemWindow GetSystemWindowByExactTitle(string title, int mSecTimeout = 1000)
        {
            try
            {
                DateTime startAt = DateTime.Now;
                while (DateTime.Now.Subtract(startAt).TotalMilliseconds < mSecTimeout)
                {
                    List<SystemWindow> windows = new List<SystemWindow>();
                    SystemWindow[] allWindows = SystemWindow.AllToplevelWindows;
                    foreach (SystemWindow w in allWindows)
                    {
                        if (string.IsNullOrEmpty(w.Title)) continue;
                        if (!w.Visible) continue;
                        if (w.Title == title )
                        {
                            windows.Add(w);
                            return windows[0];
                        }
                    }
                    if (windows.Count > 0)
                    {
                        return windows[0];
                    }
                    Thread.Sleep(100);
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static SystemWindow GetSystemWindowByHWnd(IntPtr hWnd)
        {
            try
            {
                List<SystemWindow> windows = new List<SystemWindow>();
                SystemWindow[] allWindows = SystemWindow.AllToplevelWindows;
                foreach (SystemWindow w in allWindows)
                {
                    if (w.HWnd == hWnd) return w;
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static List<SystemWindow> GetSystemWindowsByTitle(string title)
        {
            try
            {
                List<SystemWindow> windows = new List<SystemWindow>();
                SystemWindow[] allWindows = SystemWindow.AllToplevelWindows;
                foreach (SystemWindow w in allWindows)
                {
                    if (string.IsNullOrEmpty(w.Title)) continue;
                    if (!w.Visible) continue;
                    if (w.Title == title)
                    {
                            windows.Add(w);
                    }
                    else if (title.EndsWith("*"))
                    {
                        if (w.Title.StartsWith(title.Substring(0, title.Length - 1)))
                        {
                            windows.Add(w);
                        }
                    }
                    else if (title.StartsWith("*"))
                    {
                        string textToFind = title.Substring(1, title.Length - 1).Trim();
                        if (w.Title.Contains(textToFind))
                        {
                            windows.Add(w);
                        }
                    }
                }
                return windows;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static List<SystemWindow> GetSystemWindowsThatContain(string containedString)
        {
            int step = 0;
            try
            {
                step = 1;
                List<SystemWindow> windows = new List<SystemWindow>();
                step = 2;
                SystemWindow[] allWindows = SystemWindow.AllToplevelWindows;
                step = 3;
                foreach (SystemWindow w in allWindows)
                {
                    step = 4;
                    if (string.IsNullOrEmpty(w.Title)) continue;
                    step = 5;
                    if (!w.Visible) continue;
                    step = 6;
                    if (w.Title.Contains(containedString))
                    {
                        step = 7;
                        windows.Add(w);
                    }
                }
                step = 8;
                return windows;
            }
            catch (Exception ex)
            {
                string msg = string.Format($"GetSystemWindowsThatContain({containedString}) - EXCEPTION [{ex.Message}] at [{step}]");
                throw new Exception(msg);

            }
        }

        public static List<SystemWindow> GetSystemWindowWhereTitleStartsWith(string title)
        {
            try
            {
                List<SystemWindow> windows = new List<SystemWindow>();
                SystemWindow[] allWindows = SystemWindow.AllToplevelWindows;
                foreach (SystemWindow w in allWindows)
                {
                    if (string.IsNullOrEmpty(w.Title)) continue;
                    if (!w.Visible) continue;
                    if (w.Title.StartsWith(title))
                    {
                        if (w.WindowState != FormWindowState.Minimized)
                        {
                            windows.Add(w);
                        }
                    }
                }
                return windows;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static List<SystemWindow> GetSystemWindowWhereTitleEndsWith(string title)
        {
            try
            {
                string tailTitle = title;
                while (tailTitle.StartsWith("*"))  // trim leading splat
                {
                    tailTitle = tailTitle.Substring(1).Trim();
                }
                List<SystemWindow> windows = new List<SystemWindow>();
                SystemWindow[] allWindows = SystemWindow.AllToplevelWindows;
                foreach (SystemWindow w in allWindows)
                {
                    if (string.IsNullOrEmpty(w.Title)) continue;
                    if (!w.Visible) continue;
                    if (w.Title.EndsWith(tailTitle))
                    {
                        if (w.WindowState != FormWindowState.Minimized)
                        {
                            windows.Add(w);
                        }
                    }
                }
                return windows;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Takes a screenshot of the window. Downside is that the entire window
        /// must be visisble. If any windows overlap this target window their overlap
        /// will be included in the screenshot
        /// </summary>
        /// <param name="w">SystemWindow that we want a bit map for</param>
        /// <returns></returns>
        public static Bitmap WindowImageFast(SystemWindow w)
        {
            return Screenshot.TakeScreenshot(w, false, false, false);
        }

        /// <summary>
        /// Takes a screenshot of the window adjusting window rectangle for display dpi scaling.
        ///  Downside is that the entire window
        /// must be visisble. If any windows overlap this target window their overlap
        /// will be included in the screenshot
        /// </summary>
        /// <param name="w">SystemWindow that we want a bit map for</param>
        /// <returns></returns>
        public static Bitmap WindowImageFastDpi(SystemWindow w, double dpi)
        {
            try
            {
                DisplayInfo dInfo = new DisplayInfo();
                Display windowIsInDisplay = null;
                foreach (Display d in dInfo.Displays)
                {
                    if (DisplayContainsWindow(d, w))
                    {
                        windowIsInDisplay = d;

                        int l = (int)(w.Location.X * d.Scaling);
                        int t = (int)(w.Location.Y * d.Scaling);
                        int width = w.Size.Width;
                        int height = w.Size.Height;

                        Rectangle newWindowRectangle = new Rectangle(l, t, width, height);

                        //w.Location = new Point (l, t); //  Rectangle = newWindowRectangle;
                        //w.Size = new Size(width, height);
                        break;
                    }

                }

                if (dpi == 100)
                {
                    return Screenshot.TakeScreenshotDpi(w, 1.0, false);
                }
                else if (dpi == 1.0)
                {
                    return Screenshot.TakeScreenshotDpi(w, dpi, false);
                }
                else
                {
                    return Screenshot.TakeScreenshotDpi(w, dpi, false);
                }
            }
            catch (Exception ex)
            {
                _ = ex.Message;
                throw ex;
            }
        }

        public static Display GetWindowDisplay(SystemWindow w)
        {
            try
            {
                DisplayInfo dInfo = new DisplayInfo();
                //Display windowIsInDisplay = null;
                foreach (Display d in dInfo.Displays)
                {
                    if (DisplayContainsWindow(d, w))
                    {
                        return d;
                    }

                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static bool DisplayContainsWindow(Display d, SystemWindow w)
        {
            if (d.VirtualBounds.Left > w.Location.X) return false;
            if (d.VirtualBounds.Right < w.Location.X) return false;
            if (d.VirtualBounds.Top > w.Location.Y) return false;
            if (d.VirtualBounds.Bottom < w.Location.Y) return false;
            return true;
        }


        public static List<SystemWindow> GetChildren(SystemWindow wParent)
        {
            List<SystemWindow> children = new List<SystemWindow>();
            string parentTitle = wParent.Title;

            try
            {
                SystemWindow[] allWindows = SystemWindow.AllToplevelWindows;
                foreach (SystemWindow w in allWindows)
                {
                    if (string.IsNullOrEmpty(w.Title)) continue;
                    if (!w.Visible) continue;
                    if (w.Parent.Title == parentTitle)
                    {
                        //if (w.Parent.HWnd == parentHWnd)
                        //{
                        //    string s = "here we are:";
                        //}
                        //else
                        {
                            children.Add(w);
                        }
                    }
                }
                return children;
            }
            catch (Exception)
            {
                return null;
            }
        }


        public static List<SystemWindow> GetChildrenForMakeMultipleChanges()
        {
            List<SystemWindow> children = new List<SystemWindow>();

            try
            {
                SystemWindow[] allWindows = SystemWindow.AllToplevelWindows;
                foreach (SystemWindow w in allWindows)
                {
                    if (string.IsNullOrEmpty(w.Title)) continue;
                    if (!w.Visible) continue;
                    if (w.Title == "Google Ads Editor")
                    { 
                        children.Add(w);
                    }
                }
                return children;
            }
            catch (Exception)
            {
                return null;
            }
        }



        /// <summary>
        /// Returns a bitmap of the specified window. Will first take 
        /// a copy of the Image property. Advantage is that this works even
        /// when the window is overlapped by other windows.
        /// Does not work for all windows, some return solid black window.
        /// Routine detects this and falls back to grabbint a screenshot instead.
        /// Downside then is that the window must be visible, without overlapping
        /// windows.
        /// </summary>
        /// <param name="w"></param>
        /// <returns></returns>
        public static Bitmap WindowImage(SystemWindow w)
        {
            try
            {

                Bitmap windowBmp = (Bitmap)w.Image;
                if (ImageIsBlack((Bitmap)w.Image))
                {
                    // note that for this to work window must be topmost
                    windowBmp = Screenshot.TakeScreenshot(w, false, false, false);
                }
                if (w.WindowState == FormWindowState.Maximized)
                {
                    windowBmp = Screenshot.TakeScreenshot(w, false, false, false);
                }
                return windowBmp;
            }
            catch (Exception ex)
            {
                string msg = string.Format("SysWinLib.WinUtils.WindowImage() WindowTitle [{0}], HWnd [{1}], EXCEPTIOIsNullOrEmpty [{2}] ", w.Title, w.HWnd, ex.Message);
                throw new Exception(msg);
            }
        }
        #region SaveImageToFile

        protected Bitmap GetWindowImage(SystemWindow w)
        {
            try
            {
                Bitmap windowBmp = (Bitmap)w.Image;
                if (ImageIsBlack((Bitmap)w.Image))
                {
                    // note that for this to work window must be topmost
                    windowBmp = Screenshot.TakeScreenshot(w, false, false, false);
                }
                return windowBmp;
            }
            catch (Exception ex)
            {
                helpers.HandleException("ScreenShotMainForm.GetWindowImage()", ex);
                return null;
            }
        }

        public static string SaveImageToFile(Bitmap windowBmp, string saveFolder, string windowTitle)
        {
            try
            {
                string fileIsNullOrEmptyame;
                try
                {
                    fileIsNullOrEmptyame = MakeFilenameFromWindow(windowTitle);
                    if (fileIsNullOrEmptyame == "")
                    {
                        MessageBox.Show(string.Format("SaveImageToFile() error while setting filename form window [{0}]", windowTitle));
                        return "";
                    }
                    fileIsNullOrEmptyame = Path.Combine(saveFolder, fileIsNullOrEmptyame);
                    string folder = Path.GetDirectoryName(fileIsNullOrEmptyame);
                    if (!string.IsNullOrEmpty(folder) && !Directory.Exists(folder))
                    {
                        if (!string.IsNullOrEmpty(folder))
                        {
                            Directory.CreateDirectory(folder);
                        }
                    }
                    string ext = Path.GetExtension(fileIsNullOrEmptyame).ToLower();
                    switch (ext)
                    {
                        case ".bmp":
                            windowBmp.Save(fileIsNullOrEmptyame, ImageFormat.Bmp);
                            break;

                        case ".jpg":
                            windowBmp.Save(fileIsNullOrEmptyame, ImageFormat.Jpeg);
                            break;

                        case ".png":
                            windowBmp.Save(fileIsNullOrEmptyame, ImageFormat.Png);
                            break;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return fileIsNullOrEmptyame;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static bool ImageIsBlack(Bitmap bmp)
        {
            return SolidImageColor(bmp);
        }

        private unsafe static bool SolidImageColor(Bitmap bm)
        {
            try
            {
                BitmapData bmd = bm.LockBits(new System.Drawing.Rectangle((bm.Width / 2), 0, 1, bm.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, bm.PixelFormat);

                int blue;
                int green;
                int red;

                Color? lastColor = null;

                int width = bmd.Width / 2;
                for (int y = 0; y < bmd.Height; y++)
                {
                    byte* row = (byte*)bmd.Scan0 + (y * bmd.Stride);

                    blue = row[width * 3];
                    green = row[width * 2];
                    red = row[width * 1];

                    Color currentColor = Color.FromArgb(0, red, green, blue);
                    if (lastColor == null)
                    {
                        lastColor = currentColor;
                    }
                    else if (currentColor != lastColor)
                    {
                        return false;
                    }
                }
                return true;
            }
            finally
            {
                bm.Dispose();
            }
        }

        private static List<string> GetStringsToRemoveFromFilename()
        {
            string[] stringsToRemoveArray = { "...", "'", "’", "?", "," };

            List<string> stringsToRemove = new List<string>(stringsToRemoveArray);
            return stringsToRemove;
        }

        private static Dictionary<string, string> GetStringsToReplaceInFilename()
        {
            string[,] stringToReplace = { { "/", "%" }, { "|", "-" }, { " ", "_" } };

            Dictionary<string, string> stringsToReplace = new Dictionary<string, string>();
            for (int index = 0; index <= stringToReplace.GetUpperBound(0); index++)
            {
                string key = stringToReplace[index, 0];
                string value = stringToReplace[index, 1];
                stringsToReplace.Add(key, value);
            }
            return stringsToReplace;
        }

        private static string CleanupFilename(string filename)
        {
            List<string> stringsToRemoveFromFilename = GetStringsToRemoveFromFilename();
            foreach (string t in stringsToRemoveFromFilename)
            {
                filename = filename.Replace(t, "");
            }
            Dictionary<string, string> stringsToReplace = GetStringsToReplaceInFilename();
            foreach (KeyValuePair<string, string> kvp in stringsToReplace)
            {
                if (filename.Contains(kvp.Key))
                {
                    filename = filename.Replace(kvp.Key, kvp.Value);
                }
            }
            return filename;
        }

        private static string MakeFilenameFromWindow(string windowTitle, string useExtension = "bmp", bool imageWasBlack = false)
        {
            int step = 0;
            try
            {
                step = 1;
                string filenameWithoutExtension = windowTitle.Trim();
                step = 1;
                filenameWithoutExtension = CleanupFilename(filenameWithoutExtension);
                step = 1;
                if (imageWasBlack)
                {
                    step = 1;
                    filenameWithoutExtension += "!";  // indicates that image was black using w.Image
                }

                step = 1;
                if (filenameWithoutExtension.LastIndexOf(":") > 1)
                {
                    step = 1;
                    step = 809;
                    // todo check that this does what we want?!
                    filenameWithoutExtension = filenameWithoutExtension.Replace(":", "-");
                }

                step = 810;
                string filenameWithExtension = string.Format("{0}_{1}.{2}", BaseUtils.TimeStamp_ms(), filenameWithoutExtension, useExtension);

                return filenameWithExtension;
            }
            catch (Exception ex)
            {
                string s = string.Format("MakeFienameFromWindow() EXCEPTIOIsNullOrEmpty [{0}] at [{1}]", ex.Message, step);
                MessageBox.Show(s);
                return "";
            }
        }

        private long GetFileSize(string filename)
        {
            FileInfo fInfo = new FileInfo(filename);
            return fInfo.Length;
        }

        public static void SaveWindowTitle(string filename, string windowTitle, int left, int top, int width, int height)
        {
            string windowTitleLookupFile = MakeWindowTitleLookupFileIsNullOrEmptyame(filename);

            windowTitle = CleanupWindowTitle(windowTitle);
            string line = string.Format("\"{0}\",\"{1}\",\"{2}x{3}\",\"{4}x{5}\"{6}", filename, windowTitle, left, top, width, height, Environment.NewLine);

            // todo want to improve this, skip duplicates
            File.AppendAllText(windowTitleLookupFile, line);
        }

        private static string MakeWindowTitleLookupFileIsNullOrEmptyame(string referenceFilename)
        {
            //i:\gamerooms\acr\20170723\20170723110404.286_jo_acr_WindowTitle.txt
            string userId = GetUserId();
            string gameRoom = GetGameRoom(referenceFilename);
            string filename = string.Format("{0}_{1}_WindowTitle.txt", userId, gameRoom);
            //string saveFolder = Path.GetDirectoryName(referenceFilename);
            return filename;
        }

        private static string GetUserId()
        {
            return Environment.UserName.ToLower();
        }
        private static string GetGameRoom(string reference)
        {
            // todo need to remove hardcoding  
            return "acr";
        }

        private static string CleanupWindowTitle(string title)
        {
            string cleanedTitle = title;
            cleanedTitle = cleanedTitle.Replace("\"", "'");
            return cleanedTitle;
        }
        #endregion SaveImageToFile


        public static string[] StandardSkipWindows()
        {
            string[] windowTitlesToSkip = {
                ".NET - BroadcastEventWindow.4.0.0.0.3e848c8.0",
                "AXWIN Frame Window",
                "AutoCompleteProxy",
                "Alarms & Clock",
                "Battery Meter",
                "bluetoothNotificationAreaIconWindowClass",
                "CiceroUIWndFrame",
                "DDE Server Window",
                "DWM Notification Window",
                "Decklink Output",
                "Edit and Continue",
                "Ge?",
                "Hidden Window",
                "MSCTFIME UI",
                "MS_WebcheckMonitor",
                "MediaContextNotificationWindow",
                "MediaContextNotificationWindow",
                "Microsoft Text Input Application",
                "Microsoft Text Input Application",
                "Network Flyout",
                "NvContainerWindowClass00000CB0",
                "NvSvc",
                "OneDrive",
                "Output Timer",
                "Program Manager",
                "Program Manager",
                "QTrayIconMessageWindow",
                "Remux Recordings",
                "Rtc Video PnP Listener",
                "SecurityHealthSystray",
                "Settings",
                "Skype",
                "Stats",
                "SystemResourceNotifyWindow",
                "Task Host Window",
                "UxdService",
                "Visual Studio Application Management Window",
                //"Window",
                "Windows Push Notifications Platform",
                "crash service",
                "obs64"
        };
            return windowTitlesToSkip;
        }

        public static List<string> WindowSkipList()
        {
            List<string> skipList = new List<string>(StandardSkipWindows());
            return skipList;
        }


        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        
        [DllImport("user32.dll")]
        static extern IntPtr WindowFromPoint(System.Drawing.Point p);

        public static string GetActiveWindowTitle()
        {
            const int nChars = 256;
            StringBuilder Buff = new StringBuilder(nChars);
            IntPtr handle = GetForegroundWindow();

            if (GetWindowText(handle, Buff, nChars) > 0)
            {
                return Buff.ToString();
            }
            else
            {
                return null;
            }
        }


        public static bool IsActive(SystemWindow w)
        {
            IntPtr foregroundWindowHandle = GetForegroundWindow();

            bool result = (foregroundWindowHandle == w.HWnd);
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static SystemWindow GetActiveWindow()
        {
            string title = GetActiveWindowTitle();
            SystemWindow w = null;
            if (title != null)
            {
                if (title.StartsWith("h["))
                {
                    return null;
                }
                w = GetSystemWindowByExactTitle(title);
            }
            return w;
        }

        public static SystemWindow GetWindowFromPoint(Point point)
        {
            IntPtr handle = WindowFromPoint(point);
            SystemWindow w = GetSystemWindowByHWnd(handle);
            return w;
        }

        public static void ActivateWindow(SystemWindow w)
        {
            if (w == null) return;

            SetForegroundWindow(w.HWnd);
        }

        public static List<ScreenInfo> Screens()
        {
            List<ScreenInfo> screenInfo = new List<ScreenInfo>();
            int screenNumber = 0;
            foreach (var screen in Screen.AllScreens)
            {
                ScreenInfo sInfo = new ScreenInfo
                {
                    Rectangle = screen.Bounds,
                    IsPrimary = screen.Primary,
                    DisplayNo = screenNumber++,
                    BitsPerPixel = screen.BitsPerPixel,
                    DeviceName = screen.DeviceName,
                    WorkingArea = screen.WorkingArea
                };
                screenInfo.Add(sInfo);
            }
            return screenInfo;
        }


        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowThreadProcessId(IntPtr hWnd, out uint ProcessId);

        public static string GetActiveProcessFileName()
        {
            int step = 0;
            try
            {
                step = 1;
                IntPtr hwnd = GetForegroundWindow();
                step = 2;
                step = 3;
                GetWindowThreadProcessId(hwnd, out uint pid);
                step = 4;
                Process p = Process.GetProcessById((int)pid);
                step = 5;
                string processFileName = p.MainModule.FileName;
                step = 6;
                return processFileName;
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("unable to enumerate the process module"))
                {
                    return $"GetActiveProcessFileName() @[{step}] Unable to enumerate the process module";
                }
                else if (ex.Message.Contains("Access is denied"))
                {
                    return "Access is denied";
                }
                else
                {
                    return ex.Message;
                }
            }
        }


        public static IntPtr GetActiveWindowHandle()
        {
            IntPtr handle = GetForegroundWindow();
            return handle;
        }

    }

    public class ScreenInfo
    {
        public Rectangle Rectangle;
        public int DisplayNo;
        public bool IsPrimary;
        public int BitsPerPixel;
        public string DeviceName;
        public Rectangle WorkingArea;

    }
}
