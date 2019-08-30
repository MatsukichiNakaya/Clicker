using System;
using System.Runtime.InteropServices;

namespace Project.API.Define
{
    #region Point
    /// <summary>
    /// ポイント
    /// </summary>
    /// <remarks>
    /// System.Drawingのポイントクラスを参照すればいいのだが、
    /// WPFのソリューションテンプレートではデフォルト参照していないので
    /// ここで定義する
    /// </remarks>
    public class Point
    {
        public Int32 X { get; private set; }
        public Int32 Y { get; private set; }

        public Point(Int32 x, Int32 y)
        {
            this.X = x;
            this.Y = y;
        }
    }
    #endregion

    #region	POINT
    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        public Int32 X;
        public Int32 Y;

        public POINT(Int32 x, Int32 y)
        {
            this.X = x;
            this.Y = y;
        }

        public static implicit operator Point(POINT p)
        {
            return new Point(p.X, p.Y);
        }

        public static implicit operator POINT(Point p)
        {
            return new POINT(p.X, p.Y);
        }
    }
    #endregion

    #region RECT
    [StructLayout(LayoutKind.Sequential)]
    public struct Rect
    {
        public Int32 Left;
        public Int32 Top;
        public Int32 Right;
        public Int32 Bottom;
    }
    #endregion

}
