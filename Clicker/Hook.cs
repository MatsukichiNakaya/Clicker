using System;
using System.Runtime.InteropServices;
using System.Reflection;
using Project.API.User.Hook;
using Project.API.Define;
using Project.API.Define.Hook;

namespace Project.Windows.Hook
{
    #region キーフッククラス
    public class LLKeyHook : HookAPI, IDisposable
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LLKeyHook() { }

        /// <summary>
        /// デストラクタ
        /// </summary>
        ~LLKeyHook() { Dispose(); }


        ///	<summary>
        ///	コールバック用
        ///	</summary>
        private LowLevelKeyboardProc keyCallBack = null;

        private LowLevelMouseProc mouseCallBack = null;

        ///	<summary>
        ///	フックＩＤ キーボード用
        ///	</summary>
        private IntPtr hookIdK = IntPtr.Zero;

        ///	<summary>
        ///	フックＩＤ マウス用
        ///	</summary>
        private IntPtr hookIdM = IntPtr.Zero;

        ///	<summary>
        ///	キーフックを開始します。
        ///	</summary>
        ///	<returns></returns>
        public void SetKeyHook()
        {
            // コールバック用のデリゲートを作成。
            if (this.keyCallBack == null) {
                this.keyCallBack = new LowLevelKeyboardProc(this.MyKeyCallBack);
                IntPtr hMod = Marshal.GetHINSTANCE(Assembly.GetExecutingAssembly().GetModules()[0]);
                // 低レベルキーフックに登録。
                this.hookIdK = NativeMethod.SetWindowsHookEx(HookType.WH_KEYBOARD_LL, this.keyCallBack, hMod, 0);
            }
        }

        public void SetMouseHook()
        {
            if (this.mouseCallBack == null) {
                this.mouseCallBack = new LowLevelMouseProc(this.MyMouseCallBack);
                IntPtr hMod = Marshal.GetHINSTANCE(Assembly.GetExecutingAssembly().GetModules()[0]);
                // 低レベルキーフックに登録。
                this.hookIdM = NativeMethod.SetWindowsHookEx(HookType.WH_MOUSE_LL, this.mouseCallBack, hMod, 0);
            }
        }

        ///	<summary>
        ///	キーフックを終了します。
        ///	</summary>
        public void UnSet()
        {
            if (this.hookIdK != IntPtr.Zero) {
                for (Int32 i = 0; i < 5; i++) {
                    if (NativeMethod.UnhookWindowsHookEx(this.hookIdK)) {
                        break;
                    }
                }
                this.hookIdK = IntPtr.Zero;
            }

            if(this.hookIdM != IntPtr.Zero) {
                for (Int32 i = 0; i < 5; i++) {
                    if (NativeMethod.UnhookWindowsHookEx(this.hookIdM)) {
                        break;
                    }
                }
                this.hookIdM = IntPtr.Zero;
            }
        }

        #region	キーフック関連
        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        public Int32 MyKeyCallBack(Int32 code, WindowsMessages wParam, KBDLLHOOKSTRUCT lParam)
        {
            if (code < 0) {
                return NativeMethod.CallNextHookEx(IntPtr.Zero, code, wParam, lParam);
            }

            Int32 key;
            switch (wParam) {
                case WindowsMessages.WM_IME_KEYDOWN:
                case WindowsMessages.WM_KEYDOWN:
                case WindowsMessages.WM_SYSKEYDOWN:
                    key = lParam.vkCode;
                    if (KeyDown == null) { break; }
                    KeyDown(key);
                    break;
                case WindowsMessages.WM_IME_KEYUP:
                case WindowsMessages.WM_KEYUP:
                case WindowsMessages.WM_SYSKEYUP:
                    key = lParam.vkCode;
                    if(KeyUp == null) { break; }
                    KeyUp(key);
                    break;
            }
            // キーの処理を次に回して完了。
            return NativeMethod.CallNextHookEx(IntPtr.Zero, code, wParam, lParam);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        public Int32 MyMouseCallBack(Int32 code, WindowsMessages wParam, MSLLHOOKSTRUCT lParam)
        {
            if (code < 0) {
                return NativeMethod.CallNextHookEx(IntPtr.Zero, code, wParam, lParam);
            }
            switch (wParam) {
                case WindowsMessages.WM_MOUSEMOVE:
                    if (MouseMove == null) { break; }
                    MouseMove(lParam.pt.X, lParam.pt.Y);
                    break;
                case WindowsMessages.WM_LBUTTONDOWN:
                    if(MouseLeftDown == null) { break; }
                    MouseLeftDown(lParam.pt.X, lParam.pt.Y);
                    break;
            }

            // キーの処理を次に回して完了。
            return NativeMethod.CallNextHookEx(IntPtr.Zero, code, wParam, lParam);
        }
        #endregion

        /// <summary></summary>
        public event KeyEvent KeyDown;
        /// <summary></summary>
        public event KeyEvent KeyUp;
        /// <summary></summary>
        public event MouseEvent MouseMove;
        /// <summary></summary>
        public event MouseEvent MouseLeftDown;

        #region IDisposable メンバ
        /// <summary>
        /// 
        /// </summary>
        public void Dispose() { UnSet(); }
        #endregion
    }
    #endregion
}