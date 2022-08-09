using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Framework
{
    public class Screenshot
    {
        public bool SS_ImageSaveMethod1 = true;

        private SystemWindow _selectedWindow = null;
        //private Bitmap _bitmap;

        /// <summary>
        /// Take a screenshot of an arbitrary rectangle on the screen. Optionally a region
        /// can be used for clipping the rectangle. The mouse cursor, if included,
        /// is not affected by clipping.
        /// </summary>
        /// <param name="rect">Rectangle to include.</param>
        /// <param name="includeCursor">Whether to include the mouse cursor.</param>
        /// <param name="shape">Shape (region) used for clipping.</param>

        /// <summary>
        /// Take a screenshot of a given window or object.
        /// </summary>
        /// <param name="window">Window to take the screenshot from.</param>
        /// <param name="clientAreaOnly">Whether to include only the client area or also the decoration (title bar).</param>
        /// <param name="includeCursor">Whether to include the mouse cursor.</param>
        /// <param name="keepShape">Whether to keep the shape (transparency region) of the window.</param>
        public static Bitmap TakeScreenshot(SystemWindow window, bool clientAreaOnly, bool includeCursor, bool keepShape)
        {
            int step = 10;
            try
            {
                if (window == null)
                {
                    throw new Exception("window is null");
                }
                if (window == null)
                {
                    string msg = string.Format("SysWin.Screenshots(,,,) window argument is null");
                    throw new Exception(msg);
                }
                Region shape = null;
                if (keepShape)
                {
                    shape = window.Region;
                    if (shape != null && clientAreaOnly)
                    {
                        shape.Translate(window.Rectangle.Left - window.ClientRectangle.Left, window.Rectangle.Top - window.ClientRectangle.Top);
                    }
                }
                int l = (int)(window.Location.X);
                int t = (int)(window.Location.Y);
                int w = (int)(window.Size.Width);
                int h = (int)(window.Size.Height);
                if (window.WindowState == FormWindowState.Maximized)
                {
                    l = (int)(window.Rectangle.Location.X);
                    t = (int)(window.Rectangle.Location.Y);
                    w = (int)(window.Rectangle.Size.Width);
                    h = (int)(window.Rectangle.Size.Height);
                }
                Rectangle r = new Rectangle(l, t, w, h);
                return TakeScreenshot(r, includeCursor, shape);
            }
            catch (Exception ex)
            {
                string s = $"TakeScreenshot() @[{step}] = [{ex.Message}]";
                throw new Exception(s);
            }
        }



        /// <summary>
        /// Take a screenshot of an arbitrary rectangle on the screen. Optionally a region
        /// can be used for clipping the rectangle. The mouse cursor, if included,
        /// is not affected by clipping.
        /// </summary>
        /// <param name="rect">Rectangle to include.</param>
        /// <param name="includeCursor">Whether to include the mouse cursor.</param>
        /// <param name="shape">Shape (region) used for clipping.</param>
        public static Bitmap TakeScreenshot(Rectangle rect, bool includeCursor)
        {
            Bitmap returnedImage = null;
            int step = 0;
            try
            {
                step = 10;

                using (Bitmap result = new Bitmap(rect.Width, rect.Height))
                {

                    step = 20;
                    using (Graphics g = Graphics.FromImage(result))
                    {
                        step = 30;
                        try
                        {
                            g.CopyFromScreen(rect.Location, Point.Empty, rect.Size);
                        }
                        catch (Exception ex)
                        {
                            string s1 = ex.Message;
                        }
                    }
                    step = 40;
                    returnedImage = new Bitmap(result);
                    step = 50;
                    result.Dispose();
                    step = 60;
                    if (includeCursor)
                    {
                        step = 70;
                        // Cursors may use XOR operations http://support.microsoft.com/kb/311221
                        CURSORINFO ci;
                        step = 80;
                        ci.cbSize = Marshal.SizeOf(typeof(CURSORINFO));
                        step = 90;
                        ApiHelper.FailIfZero(GetCursorInfo(out ci));
                        step = 100;
                        if ((ci.flags & CURSOR_SHOWING) != 0)
                        {
                            step = 110;
                            using (Cursor c = new Cursor(ci.hCursor))
                            {
                                step = 120;
                                Point cursorLocation = new Point(ci.ptScreenPos.X - rect.X - c.HotSpot.X, ci.ptScreenPos.Y - rect.Y - c.HotSpot.Y);
                                // c.Draw() does not work with XOR cursors (like the default text cursor)
                                step = 130;
                                DrawCursor(ref returnedImage, c, cursorLocation);
                            }
                        }
                    }
                    step = 140;
                }

                return returnedImage;
            }
            catch (Exception ex)
            {
                string s = string.Format("EXCEPTIOIsNullOrEmpty at step {0} [{1}]", step, ex.Message);
                throw ex;
            }
        }



        public static Bitmap TakeScreenshot(Rectangle rect, bool includeCursor, Region shape)
        {
            Bitmap returnedImage = null;
            int step = 0;
            try
            {
                step = 1;

                using (Bitmap result = new Bitmap(rect.Width, rect.Height))
                {

                    step = 2;
                    using (Graphics g = Graphics.FromImage(result))
                    {
                        step = 3;
                        try
                        {
                            g.CopyFromScreen(rect.Location, Point.Empty, rect.Size);
                        }
                        catch (Exception ex)
                        {
                            string s1 = ex.Message;
                        }
                    }
                    step = 4;
                    if (shape != null)
                    {
                        step = 5;
                        for (int i = 0; i < result.Width; i++)
                        {
                            step = 6;
                            for (int j = 0; j < result.Height; j++)
                            {
                                step = 7;
                                if (!shape.IsVisible(new Point(i, j)))
                                {
                                    step = 8;
                                    result.SetPixel(i, j, Color.Transparent);
                                }
                            }
                        }
                    }
                    step = 9;
                    returnedImage = new Bitmap(result);
                    result.Dispose();
                    /*
                    if (includeCursor)
                    {
                        step = 10;
                        // Cursors may use XOR operations http://support.microsoft.com/kb/311221
                        CURSORIIsNullOrEmptyFO ci;
                        step = 11;
                        ci.cbSize = Marshal.SizeOf(typeof(CURSORIIsNullOrEmptyFO));
                        step = 12;
                        ApiHelper.FailIfZero(GetCursorInfo(out ci));
                        step = 13;
                        if ((ci.flags & CURSOR_SHOWIIsNullOrEmptyG) != 0)
                        {
                            step = 14;
                            using (Cursor c = new Cursor(ci.hCursor))
                            {
                                step = 15;
                                Point cursorLocation = new Point(ci.ptScreenPos.X - rect.X - c.HotSpot.X, ci.ptScreenPos.Y - rect.Y - c.HotSpot.Y);
                                // c.Draw() does not work with XOR cursors (like the default text cursor)
                                step = 16;
                                DrawCursor(ref returnedImage, c, cursorLocation);
                            }
                        }
                    }

                    step = 17;
                    */
                }

                return returnedImage;
            }
            catch (Exception ex)
            {
                string s = string.Format("EXCEPTIOIsNullOrEmpty at step {0} [{1}]", step, ex.Message);
                throw ex;
            }
        }

        protected Bitmap GetWindowImage(SystemWindow w)
        {
            int step = 10;
            try
            {
                if (w == null) return null;

                step = 20;
                Bitmap windowBmp = null;
                //Bitmap windowBmp = (Bitmap)w.Image;

                step = 30;
                if (w.WindowState == FormWindowState.Maximized)
                {
                    step = 50;
                    windowBmp = TakeScreenshot(w.Rectangle, false, null);
                }
                else if (SS_ImageSaveMethod1)  // todo resolve which way this works
                {
                    step = 60;
                    windowBmp = (Bitmap)w.Image.Clone();  // do not access w.Image directly if possible                                                              // each reference causes image to flicker
                }
                else
                {
                    step = 70;
                    windowBmp = TakeScreenshot(w, false, false, false);
                }


                step = 80;

                return windowBmp;
            }
            catch (Exception ex)
            {
                HandleException("ScreenShotMainForm.GetWindowImage()", ex, step);
                return null;
            }
        }


        private void TakeScreenshot()
        {
            int step = 10;
            try
            {
                if (_selectedWindow == null)
                {
                    step = 20;
                    throw new Exception("Selected Window is null");
                }
                step = 40;
                if (!_selectedWindow.Visible)
                {
                    step = 50;
                    throw new Exception("Selected Window is NOT visible");
                }

                step = 60;
                using (Bitmap bmp = GetWindowImage(_selectedWindow))
                { //Bitmap bmp = WinUtils.WindowImageFast(_selectedWindow);
                    step = 70;
                    if (bmp == null)
                    {
                        return;
                    }
                    step = 80;
                    string screenshotFileName = MakeScreenshotFilename(_selectedWindow);
                    step = 90;
                    string ext = Path.GetExtension(screenshotFileName).ToLower();
                    step = 100;
                    step = 120;
                    switch (ext)
                    {
                        case ".bmp":
                            step = 130;
                            bmp.Save(screenshotFileName, ImageFormat.Bmp);
                            step = 140;
                            break;

                        case ".jpg":
                            step = 160;
                            bmp.Save(screenshotFileName, ImageFormat.Jpeg);
                            step = 170;
                            break;

                        case ".png":
                            step = 190;
                            bmp.Save(screenshotFileName, ImageFormat.Png);
                            step = 200;
                            break;
                    }
                    step = 220;
                    //lblScreenCaptureCount.Text = screenCaptureCount.ToString();
                    step = 230;
                    //DisplayImageThumbnail(bmp);
                }
            }
            catch (Exception ex)
            {
                HandleException("TakeScreenshot().1", ex, step);

                //chkEnableTimer.Checked = false;
                string s = ex.Message;
            }

        }

        /// <summary>
        /// Take a screenshot of a given window or object adjusting window rectangle for dpi
        /// </summary>
        /// <param name="window">Window to take the screenshot from.</param>
        /// <param name="dpi">Display scaling (1.0, 1.25, 1.5)</param>
        /// <param name="clientAreaOnly">Whether to include only the client area or also the decoration (title bar).</param>
        /// <param name="includeCursor">Whether to include the mouse cursor.</param>
        /// <param name="keepShape">Whether to keep the shape (transparency region) of the window.</param>
        public static Bitmap TakeScreenshotDpi(SystemWindow window, double dpi, bool includeCursor)
        {
            try
            {
                Display display = WinUtils.GetWindowDisplay(window);
                if (window == null)
                {
                    string msg = string.Format("SysWin.Screenshots(,,,) window argument is null");
                    throw new Exception(msg);
                }
                Rectangle r = window.Rectangle;

                if (dpi == 1.0)
                {

                }
                else if (dpi == 100) //(dpi != 1.0)
                {
                    throw new NotImplementedException();
                }
                else
                {
                    //List<ScreenInfo> sInfo = WinUtils.Screens();

                    int l = (int)(r.Left); // * dpi);
                    int t = (int)(r.Top); // * dpi);

                    int w = (int)window.Size.Width; // (r.Width * dpi);
                    int h = (int)window.Size.Height; // (r.Height * dpi);
                    if (r.Left < 0)
                    {

                    }

                    //l = (int) (window.Position. Left / dpi);
                    //t = (int)(window.Position.Top / dpi);
                    //w = (int)(window.Position.Width / dpi);
                    //h = (int)(window.Position.Height / dpi);

                    //if display scaling == 100%. then need to normalize dimensions by dividing by scaling dpi

                    double displayScaling = dpi; // / 100.0; //GetDisplayScaling(window);
                                                 //displayScaling = getScalingFactorW(window);
                                                 //if (displayScaling != dpi)
                    {
                        // ? use displayScaling instead of dpi 
                        int leftOffset = r.Left - display.VirtualBounds.Left;
                        l = (int)(display.VirtualBounds.Left + (leftOffset * dpi));
                        int topOffset = r.Top - display.VirtualBounds.Top;
                        t = (int)(display.VirtualBounds.Top + (topOffset * dpi));

                        w = (int)(w * displayScaling); // dpi);
                        h = (int)(h * displayScaling); // dpi);
                    }


                    //l = 7840; t = 58; w = 1280; h = 916; //

                    // 20191021-1744
                    r = new Rectangle(l, t, w, h);
                }
                return Screenshot.TakeScreenshot(r, includeCursor);
            }
            catch (Exception ex)
            {
                string s = ex.Message;
                throw ex;
            }
        }

        private string MakeScreenshotFilename(SystemWindow selectedWindow)
        {
            throw new NotImplementedException();
        }
        private void HandleException(string caller, Exception ex, int step, string tag = "")
        {
            string msg = $"{caller}() @[{step}] [{tag}]{Environment.NewLine}EXCEPTION [{ex}]";
            if (tag == "")
            {
                msg = $"{caller}() @[{step}]{Environment.NewLine}EXCEPTION [{ex}]";
            }
            throw new Exception(msg);
        }


        private static void DrawCursor(ref Bitmap bitmap, Cursor cursor, Point cursorLocation)
        {
            // http://support.microsoft.com/kb/311221
            // http://social.msdn.microsoft.com/Forums/en-US/csharpgeneral/thread/291990e0-fb68-4e0a-ae12-835d43b9275b/

            IntPtr compatibleHDC;
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                IntPtr hDC = g.GetHdc();
                compatibleHDC = CreateCompatibleDC(hDC);
                g.ReleaseHdc();
            }
            IntPtr hBmp = bitmap.GetHbitmap();
            SelectObject(compatibleHDC, hBmp);
            DrawIcon(compatibleHDC, cursorLocation.X, cursorLocation.Y, cursor.Handle);
            bitmap.Dispose();
            bitmap = Image.FromHbitmap(hBmp);
            DeleteObject(hBmp);
        }


        #region PInvoke Declarations

        [StructLayout(LayoutKind.Sequential)]
        struct CURSORINFO
        {
            public Int32 cbSize;
            public Int32 flags;
            public IntPtr hCursor;
            public POIIsNullOrEmptyT ptScreenPos;
        }

        [DllImport("user32.dll")]
        static extern int GetCursorInfo(out CURSORINFO pci);

        private const Int32 CURSOR_SHOWING = 0x00000001;

        [DllImport("user32.dll")]
        static extern int DrawIcon(IntPtr hDC, int X, int Y, IntPtr hIcon);

        [DllImport("gdi32.dll", SetLastError = true)]
        static extern IntPtr CreateCompatibleDC(IntPtr hdc);

        [DllImport("gdi32.dll", ExactSpelling = true, PreserveSig = true, SetLastError = true)]
        static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

        [DllImport("gdi32.dll")]
        static extern bool DeleteObject(IntPtr hObject);

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool PrintWindow(IntPtr hwnd, IntPtr hDC, uint nFlags);

        #endregion

    }
}