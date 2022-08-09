using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Framework
{
    public class SystemWindow
    {
        private static readonly Predicate<SystemWindow> ALL = delegate { return true; };

        private IntPtr _hwnd;

        /// <summary>
        /// Wrapper around the Winapi POIIsNullOrEmptyT type.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            /// <summary>
            /// The X Coordinate.
            /// </summary>
            public int X;

            /// <summary>
            /// The Y Coordinate.
            /// </summary>
            public int Y;

            /// <summary>
            /// Creates a new POIIsNullOrEmptyT.
            /// </summary>
            /// <param name="x"></param>
            /// <param name="y"></param>
            public POINT(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }

            /// <summary>
            /// Implicit cast.
            /// </summary>
            /// <returns></returns>
            public static implicit operator System.Drawing.Point(POINT p)
            {
                return new System.Drawing.Point(p.X, p.Y);
            }

            /// <summary>
            /// Implicit cast.
            /// </summary>
            /// <returns></returns>
            public static implicit operator POINT(System.Drawing.Point p)
            {
                return new POINT(p.X, p.Y);
            }
        }


        [StructLayout(LayoutKind.Sequential)]
        private struct WINDOWPLACEMENT
        {
            public int length;
            public int flags;
            public int showCmd;
            public POINT ptMinPosition;
            public POINT ptMaxPosition;
            public RECT rcvalueormalPosition;
        }

        [DllImport("user32.dll")]
        static extern bool GetWindowPlacement(IntPtr hWnd,
           ref WINDOWPLACEMENT lpwndpl);

        [DllImport("user32.dll")]
        static extern bool SetWindowPlacement(IntPtr hWnd,
           [In] ref WINDOWPLACEMENT lpwndpl);


        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);


        [DllImport("user32.dll")]
        static extern int GetClientRect(IntPtr hWnd, out RECT lpRect);


        [DllImport("user32.dll")]
        static extern int ClientToScreen(IntPtr hWnd, ref Point lpPoint);

        /// <summary>
        /// The window's position inside its parent or on the screen.
        /// </summary>
        public RECT Position
        {
            get
            {
                WINDOWPLACEMENT wp = new WINDOWPLACEMENT();
                wp.length = Marshal.SizeOf(wp);
                GetWindowPlacement(_hwnd, ref wp);
                return wp.rcvalueormalPosition;
            }

            set
            {
                WINDOWPLACEMENT wp = new WINDOWPLACEMENT();
                wp.length = Marshal.SizeOf(wp);
                GetWindowPlacement(_hwnd, ref wp);
                wp.rcvalueormalPosition = value;
                SetWindowPlacement(_hwnd, ref wp);
            }
        }

        /// <summary>
        /// The window's size.
        /// </summary>
        public Size Size
        {
            get
            {
                return Position.Size;
            }

            set
            {
                WINDOWPLACEMENT wp = new WINDOWPLACEMENT();
                wp.length = Marshal.SizeOf(wp);
                GetWindowPlacement(_hwnd, ref wp);
                wp.rcvalueormalPosition.Right = wp.rcvalueormalPosition.Left + value.Width;
                wp.rcvalueormalPosition.Bottom = wp.rcvalueormalPosition.Top + value.Height;
                SetWindowPlacement(_hwnd, ref wp);
            }
        }
    

        /// <summary>
        /// The window's position in absolute screen coordinates. Use 
        /// <see cref="Position"/> if you want to use the relative position.
        /// </summary>
        public RECT Rectangle
        {
            get
            {
                RECT r = new RECT();
                GetWindowRect(_hwnd, out r);
                return r;
            }
        }

        [DllImport("user32.dll")]
        static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);


        [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        private static extern IntPtr GetParent(IntPtr hWnd);


        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        //private static extern bool SetForegroundWindow(IntPtr hWnd);
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern int GetWindowText(IntPtr hWnd, [Out] StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("user32.dll")]
        static extern bool SetWindowText(IntPtr hWnd, string lpString);

        /// <summary>
        /// The position of the window's contents in absolute screen coordinates. Use 
        /// <see cref="Rectangle"/> if you want to include the title bar etc.
        /// </summary>
        public RECT ClientRectangle
        {
            get
            {
                RECT r = new RECT();
                ApiHelper.FailIfZero(GetClientRect(_hwnd, out r));
                Point p = new Point();
                p.X = p.Y = 0;
                ApiHelper.FailIfZero(ClientToScreen(_hwnd, ref p));
                Rectangle result = r;
                result.Location = p;
                return result;
            }
        }


        /// <summary>
        /// Create a new SystemWindow instance from a window handle.
        /// </summary>
        /// <param name="HWnd">The window handle.</param>
        public SystemWindow(IntPtr HWnd)
        {
            _hwnd = HWnd;
        }

        [DllImport("user32.dll")]
        private static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);

        private delegate int EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

        /// <summary>
        /// Returns all available toplevel windows.
        /// </summary>
        public static SystemWindow[] AllToplevelWindows
        {
            get
            {
                return FilterToplevelWindows(new Predicate<SystemWindow>(ALL));
            }
        }

        /// <summary>
        /// Returns all toplevel windows that match the given predicate.
        /// </summary>
        /// <param name="predicate">The predicate to filter.</param>
        /// <returns>The filtered toplevel windows</returns>
        public static SystemWindow[] FilterToplevelWindows(Predicate<SystemWindow> predicate)
        {
            List<SystemWindow> wnds = new List<SystemWindow>();
            EnumWindows(new EnumWindowsProc(delegate (IntPtr hwnd, IntPtr lParam)
            {
                SystemWindow tmp = new SystemWindow(hwnd);
                if (predicate(tmp))
                    wnds.Add(tmp);
                return 1;
            }), new IntPtr(0));
            return wnds.ToArray();
        }


        public bool Visible { get => IsWindowVisible(_hwnd); }

        public Image Image;
        public Region Region;
        public Point Location;

        /// <summary>
        /// Whether this window is minimized or maximized.
        /// </summary>
        public FormWindowState WindowState
        {
            get
            {
                WINDOWPLACEMENT wp = new WINDOWPLACEMENT();
                wp.length = Marshal.SizeOf(wp);
                GetWindowPlacement(_hwnd, ref wp);
                switch (wp.showCmd % 4)
                {
                    case 2: return FormWindowState.Minimized;
                    case 3: return FormWindowState.Maximized;
                    default: return FormWindowState.Normal;
                }
            }
            set
            {
                int showCommand;
                switch (value)
                {
                    case FormWindowState.Normal:
                        showCommand = 1;
                        break;
                    case FormWindowState.Minimized:
                        showCommand = 2;
                        break;
                    case FormWindowState.Maximized:
                        showCommand = 3;
                        break;
                    default: return;
                }
                ShowWindow(_hwnd, showCommand);
            }
        }
        /// <summary>
        /// The Window handle of this window.
        /// </summary>
        public IntPtr HWnd { get { return _hwnd; } }

        /// <summary>
        /// The title of this window (by the <c>GetWindowText</c> API function).
        /// </summary>
        public string Title
        {
            get
            {
                StringBuilder sb = new StringBuilder(GetWindowTextLength(_hwnd) + 1);
                GetWindowText(_hwnd, sb, sb.Capacity);
                return sb.ToString();
            }

            set
            {
                SetWindowText(_hwnd, value);
            }
        }


        /// <summary>
        /// This window's parent. A dialog's parent is its owner, a component's parent is
        /// the window that contains it.
        /// </summary>
        public SystemWindow Parent
        {
            get
            {
                return new SystemWindow(GetParent(_hwnd));
            }
        }


    }
}
