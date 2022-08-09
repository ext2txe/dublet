using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Framework
{
    public class DisplayInfo
    {
        const int ENUM_CURRENT_SETTINGS = -1;

        public List<Display> Displays = new List<Display>();


        public Rectangle DesktopBounds;

        public DisplayInfo()
        {
            Displays.Clear();
            foreach (Screen screen in Screen.AllScreens)
            {
                DEVMODE dm = new DEVMODE();
                dm.dmSize = (short)Marshal.SizeOf(typeof(DEVMODE));
                EnumDisplaySettings(screen.DeviceName, ENUM_CURRENT_SETTINGS, ref dm);

                Size actualSize = new System.Drawing.Size(dm.dmPelsWidth, dm.dmPelsHeight);
                Size virtualSize = new System.Drawing.Size(screen.Bounds.Width, screen.Bounds.Height);

                double scaling =  (double) (actualSize.Width / (double) virtualSize.Width);

                Display display = new Display()
                {
                    Name = screen.DeviceName,
                    VirtualBounds = screen.Bounds,
                    ActualBounds = new Rectangle(screen.Bounds.Left, screen.Bounds.Top, actualSize.Width, actualSize.Height),
                    ActualResolution = actualSize,
                    VirtualResolution = virtualSize,
                    Scaling = scaling
                };

                Displays.Add(display);
            }
            SortDisplaysByBounds();
            DesktopBounds = GetDesktopBounds(Displays);
        }

        public Rectangle GetDesktopBounds(List<Display> displays)
        {
            int left = int.MaxValue;
            int right = int.MinValue;
            int top = int.MaxValue;
            int bottom = int.MinValue;

            foreach (Display d in displays)
            {
                if (left > d.VirtualBounds.Left)
                {
                    left = d.VirtualBounds.Left;
                }
                if (right < d.VirtualBounds.Right)
                {
                    right = d.VirtualBounds.Right;
                }
                if (top > d.VirtualBounds.Top)
                {
                    top = d.VirtualBounds.Top;
                }
                if (bottom < d.VirtualBounds.Bottom)
                {
                    bottom = d.VirtualBounds.Bottom;
                }
            }

            return new Rectangle(left, top, right - left, bottom - top);
        }


        /// <summary>
        /// Sort Display so that bounds are contiguous from left most to right.
        /// </summary>
        private void SortDisplaysByBounds()
        {
            if (Displays.Count == 1) return;

            List<Display> tempList = new List<Display>();

            while (Displays.Count > 0)
            {
                int leftMost = int.MaxValue;
                int leftMostIndex = -1;
                int i = 0;
                foreach (Display d in Displays)
                {
                    if (d.VirtualBounds.Left < leftMost)
                    {
                        leftMost = d.VirtualBounds.Left;
                        leftMostIndex = i;
                    }
                    i++;
                }
                tempList.Add(Displays[leftMostIndex]);
                Displays.RemoveAt(leftMostIndex);
            }
            Displays = tempList;
        }

        public static Display GetWindowDisplay(SystemWindow w)
        {
            try
            { 
                if (w.Rectangle.Top < 0)
                {
                    string s = string.Format($"here we are Top = {w.Rectangle.Top}");
                    //if (w.WindowState != FormWindowState.Maximized)
                    //{
                    //    throw new Exception($"GetWindowDisplay(w) window is out of bounds! ({w.Rectangle.Left}, {w.Rectangle.Top}, {w.Rectangle.Width}, {w.Rectangle.Height}) Check if Teamviewer is not maximized.");
                    //}
                }
                DisplayInfo dInfo = new DisplayInfo();

                Point topLeft = new Point(w.Rectangle.Left, w.Rectangle.Top);
                if (topLeft.Y < 0)
                {
                    topLeft.Y = 0;
                }
                if (w.WindowState == FormWindowState.Maximized)
                {
                    topLeft = new Point(w.Rectangle.Left + 8, w.Rectangle.Top + 8);
                }
                foreach (Display d in dInfo.Displays)
                {
                    if (d.VirtualBounds.Contains(topLeft))
                    {
                        return d;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                string s = ex.Message;
                throw ex;
            }

}


/// <summary>
/// issue - when displays of different resolutions combine to define 'dead spots' where there
/// is no display area, then the mouse takes the upper co-ord of the left hand display just exited 
/// displays a virtual y co-ordinate.
/// 
/// For this case there is no valid display, 
/// 
/// </summary>
/// <param name="p"></param>
/// <returns></returns>
public static int GetDisplayForPoint(Point p)
        {
            DisplayInfo dInfo = new DisplayInfo();

            Point topLeft = new Point(p.X, p.Y);
            int index = 0;
            foreach (Display d in dInfo.Displays)
            {
                if (d.VirtualBounds.Contains(topLeft))
                {
                    return index;
                }
                else if (d.VirtualBounds.Left + d.VirtualBounds.Width < p.X 
                    &&
                    d.ActualBounds.Left + d.ActualBounds.Width > p.X)
                {
                    return index;  // cursor is in an area of the desktop that does not
                                // have a physical representation
                }
                else if (d.VirtualBounds.Left > p.X && d.VirtualBounds.Top + d.VirtualBounds.Height < p.Y)
                {
                    return index - 1;
                }
                index++;
            }
            return -1;

        }

        public static double GetDPIForDisplay(int display)
        {
            try
            {
                DisplayInfo dInfo = new DisplayInfo();

                Display d = dInfo.Displays[display];
                return d.Scaling;
            }
            catch (Exception ex)
            {
                string s = ex.Message;
                throw ex;
            }
        }

        #region pInvoke stuff
        [DllImport("user32.dll")]
        public static extern bool EnumDisplaySettings(string lpszDeviceName, int iModeNum, ref DEVMODE lpDevMode);

        [StructLayout(LayoutKind.Sequential)]
        public struct DEVMODE
        {
            private const int CCHDEVICENAME = 0x20;
            private const int CCHFORMNAME = 0x20;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
            public string dmDeviceName;
            public short dmSpecVersion;
            public short dmDriverVersion;
            public short dmSize;
            public short dmDriverExtra;
            public int dmFields;
            public int dmPositionX;
            public int dmPositionY;
            public ScreenOrientation dmDisplayOrientation;
            public int dmDisplayFixedOutput;
            public short dmColor;
            public short dmDuplex;
            public short dmYResolution;
            public short dmTTOption;
            public short dmCollate;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
            public string dmFormName;
            public short dmLogPixels;
            public int dmBitsPerPel;
            public int dmPelsWidth;
            public int dmPelsHeight;
            public int dmDisplayFlags;
            public int dmDisplayFrequency;
            public int dmICMMethod;
            public int dmICMIntent;
            public int dmMediaType;
            public int dmDitherType;
            public int dmReserved1;
            public int dmReserved2;
            public int dmPanningWidth;
            public int dmPanningHeight;
        }
        #endregion pInvoke stuff
    }
}
