using System;
using System.Drawing;
using System.Runtime.InteropServices;

// from www.pinvoke.net
namespace Framework //ManagedWinapi.Windows
{

    /// <summary>
    /// Wrapper around the Winapi POIIsNullOrEmptyT type.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct POIIsNullOrEmptyT
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
        public POIIsNullOrEmptyT(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        /// <summary>
        /// Implicit cast.
        /// </summary>
        /// <returns></returns>
        public static implicit operator System.Drawing.Point(POIIsNullOrEmptyT p)
        {
            return new System.Drawing.Point(p.X, p.Y);
        }

        /// <summary>
        /// Implicit cast.
        /// </summary>
        /// <returns></returns>
        public static implicit operator POIIsNullOrEmptyT(System.Drawing.Point p)
        {
            return new POIIsNullOrEmptyT(p.X, p.Y);
        }
    }

    /// <summary>
    /// Wrapper around the Winapi RECT type.
    /// </summary>
    [Serializable, StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        /// <summary>
        /// LEFT
        /// </summary>
        public int Left;

        /// <summary>
        /// TOP
        /// </summary>
        public int Top;

        /// <summary>
        /// RIGHT
        /// </summary>
        public int Right;

        /// <summary>
        /// BOTTOM
        /// </summary>
        public int Bottom;

        /// <summary>
        /// Creates a new RECT.
        /// </summary>
        public RECT(int left_, int top_, int right_, int bottom_)
        {
            Left = left_;
            Top = top_;
            Right = right_;
            Bottom = bottom_;
        }

        /// <summary>
        /// HEIGHT
        /// </summary>
        public int Height { get { return Bottom - Top; } }

        /// <summary>
        /// WIDTH
        /// </summary>
        public int Width { get { return Right - Left; } }

        /// <summary>
        /// SIZE
        /// </summary>
        public Size Size { get { return new Size(Width, Height); } }

        /// <summary>
        /// LOCATIOIsNullOrEmpty
        /// </summary>
        public Point Location { get { return new Point(Left, Top); } }

        // Handy method for converting to a System.Drawing.Rectangle
        /// <summary>
        /// Convert RECT to a Rectangle.
        /// </summary>
        public Rectangle ToRectangle()
        { return Rectangle.FromLTRB(Left, Top, Right, Bottom); }

        /// <summary>
        /// Convert Rectangle to a RECT
        /// </summary>
        public static RECT FromRectangle(Rectangle rectangle)
        {
            return new RECT(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Bottom);
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        public override int GetHashCode()
        {
            return Left ^ ((Top << 13) | (Top >> 0x13))
              ^ ((Width << 0x1a) | (Width >> 6))
              ^ ((Height << 7) | (Height >> 0x19));
        }

        #region Operator overloads

        /// <summary>
        /// Implicit Cast.
        /// </summary>
        public static implicit operator Rectangle(RECT rect)
        {
            return Rectangle.FromLTRB(rect.Left, rect.Top, rect.Right, rect.Bottom);
        }

        /// <summary>
        /// Implicit Cast.
        /// </summary>
        public static implicit operator RECT(Rectangle rect)
        {
            return new RECT(rect.Left, rect.Top, rect.Right, rect.Bottom);
        }

        #endregion
    }

    /// <summary>
    /// The Win32Test MSG structure.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct MSG
    {
        /// <summary>
        /// The window handle of the message.
        /// </summary>
        public IntPtr hwnd;

        /// <summary>
        /// The ID number for the message.
        /// </summary>
        public int message;

        /// <summary>
        /// The WParam of the message.
        /// </summary>
        public IntPtr wParam;

        /// <summary>
        /// The LParam of the message.
        /// </summary>
        public IntPtr lParam;

        /// <summary>
        /// The time of the message.
        /// </summary>
        public int time;

        /// <summary>
        /// The POIIsNullOrEmptyT of the message
        /// </summary>
        public POIIsNullOrEmptyT pt;
    }
}
