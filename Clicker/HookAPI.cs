using System;
using System.Runtime.InteropServices;
using Project.API.Define;
using Project.API.Define.Hook;

namespace Project.API.Define.Hook
{
    #region	HookType
    /// <summary></summary>
    public enum HookType : Int32
    {
        WH_JOURNALRECORD = 0,
        WH_JOURNALPLAYBACK = 1,
        WH_KEYBOARD = 2,
        WH_GETMESSAGE = 3,
        WH_CALLWNDPROC = 4,
        WH_CBT = 5,
        WH_SYSMSGFILTER = 6,
        WH_MOUSE = 7,
        WH_HARDWARE = 8,
        WH_DEBUG = 9,
        WH_SHELL = 10,
        WH_FOREGROUNDIDLE = 11,
        WH_CALLWNDPROCRET = 12,
        WH_KEYBOARD_LL = 13,
        WH_MOUSE_LL = 14
    }
    #endregion

    #region	MSLLHOOKSTRUCT
    [StructLayout(LayoutKind.Sequential)]
    public class MSLLHOOKSTRUCT
    {
        public POINT pt;
        public Int32 mouseData;
        public Int32 flags;
        public Int32 time;
        public IntPtr dwExtraInfo;
    }
    #endregion

    #region	KBDLLHOOKSTRUCT
    [StructLayout(LayoutKind.Sequential)]
    public class KBDLLHOOKSTRUCT
    {
        public Int32 vkCode;
        public Int32 scanCode;
        public Int32 flags;
        public Int32 time;
        public IntPtr dwExtraInfo;
    }
    #endregion
}

namespace Project.API.User.Hook
{
    public delegate void KeyEvent(Int32 key);

    public delegate void MouseEvent(Int32 x, Int32 y);

    /// <summary>
    /// 
    /// </summary>
    public class HookAPI
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        public delegate IntPtr HookProc(Int32 code, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// (Delegate) Key hook function
        /// </summary>
        /// <param name="code"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        public delegate Int32 LowLevelKeyboardProc(Int32 code, WindowsMessages wParam, [In] KBDLLHOOKSTRUCT lParam);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        public delegate Int32 LowLevelMouseProc(Int32 code, WindowsMessages wParam, [In] MSLLHOOKSTRUCT lParam);

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="code"></param>
        ///// <param name="wParam"></param>
        ///// <param name="lParam"></param>
        ///// <returns></returns>
        //public delegate Int32 MessageProc(Int32 code, WindowsMessages wParam, [In] IntPtr lParam);

        /// <summary></summary>
        protected class NativeMethod
        {
            #region Windows API
            /// <summary>
            /// 
            /// </summary>
            /// <param name="hook"></param>
            /// <param name="callback"></param>
            /// <param name="hMod"></param>
            /// <param name="dwThreadId"></param>
            /// <returns></returns>
            [DllImport("user32.dll")]
            public static extern IntPtr SetWindowsHookEx(HookType hook, HookProc callbackFunc, IntPtr hWnd, UInt32 threadId);

            /// <summary>
            /// 
            /// </summary>
            /// <param name="hookCode"></param>
            /// <param name="callbackFunc"></param>
            /// <param name="hWnd"></param>
            /// <param name="threadId"></param>
            /// <returns></returns>
            [DllImport("user32.dll")]
            public static extern IntPtr SetWindowsHookEx(HookType hook, LowLevelKeyboardProc callbackFunc, IntPtr hWnd, UInt32 threadId);

            /// <summary>
            /// 
            /// </summary>
            /// <param name="code"></param>
            /// <param name="func"></param>
            /// <param name="hInstance"></param>
            /// <param name="threadID"></param>
            /// <returns></returns>
            [DllImport("user32.dll")]
            public static extern IntPtr SetWindowsHookEx(HookType hook, LowLevelMouseProc callbackFunc, IntPtr hWnd, Int32 threadId);


            /// <summary>
            /// 
            /// </summary>
            /// <param name="hWnd"></param>
            /// <returns></returns>
            [DllImport("user32.dll")]
            public static extern Boolean UnhookWindowsHookEx(IntPtr hWnd);

            /// <summary>
            /// 
            /// </summary>
            /// <param name="hookHandle"></param>
            /// <param name="msg"></param>
            /// <param name="wParam"></param>
            /// <param name="lParam"></param>
            /// <returns></returns>
            [DllImport("user32.dll")]
            public static extern IntPtr CallNextHookEx(IntPtr hookHandle, Int32 msg, Int32 wParam, IntPtr lParam);

            /// <summary>
            /// 
            /// </summary>
            /// <param name="hhk"></param>
            /// <param name="nCode"></param>
            /// <param name="wParam"></param>
            /// <param name="lParam"></param>
            /// <returns></returns>
            [DllImport("user32.dll")]
            public static extern Int32 CallNextHookEx(IntPtr hhk, Int32 nCode, WindowsMessages wParam, [In]KBDLLHOOKSTRUCT lParam);

            /// <summary>
            /// 
            /// </summary>
            /// <param name="hhk"></param>
            /// <param name="nCode"></param>
            /// <param name="wParam"></param>
            /// <param name="lParam"></param>
            /// <returns></returns>
            [DllImport("user32.dll")]
            public static extern Int32 CallNextHookEx(IntPtr hhk, Int32 nCode, WindowsMessages wParam, [In]MSLLHOOKSTRUCT lParam);
            #endregion
        }
    }
}