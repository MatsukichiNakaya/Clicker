using System;
using System.Runtime.InteropServices;

namespace Clicker
{
    internal class NativeMethod
    {
        [DllImport("USER32.dll", CallingConvention = CallingConvention.StdCall)]
        private static extern void SetCursorPos(Int32 X, Int32 Y);

        [DllImport("USER32.dll", CallingConvention = CallingConvention.StdCall)]
        private static extern void mouse_event(Int32 dwFlags, Int32 dx, Int32 dy, Int32 cButtons, Int32 dwExtraInfo);

        [DllImport("USER32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern Boolean GetCursorPos(ref Project.API.Define.POINT pt);

        private static readonly Int32 MOUSEEVENTF_LEFTDOWN = 0x2;
        private static readonly Int32 MOUSEEVENTF_LEFTUP = 0x4;

        public static void SetCursor(Int32 X, Int32 Y)
        {
            SetCursorPos(X, Y);
        }

        public static void MouseClick()
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }

        public static Project.API.Define.POINT GetMousePos()
        {
            var point = new Project.API.Define.POINT();
            GetCursorPos(ref point);
            return new Project.API.Define.POINT(point.X, point.Y);
        }
    }
}
