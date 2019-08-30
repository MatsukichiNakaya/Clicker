using System;
using System.Runtime.InteropServices;

namespace Project
{
    /// <summary>Win32 API クラス</summary>
    internal class NativeMethod
    {
        /// <summary>ウインドウスタイルの取得</summary>
        [DllImport("user32.dll")]
        public static extern UInt32 GetWindowLong(IntPtr hWnd, GWL index);

        /// <summary>ウインドウスタイルの設定</summary>
        [DllImport("user32.dll")]
        public static extern UInt32 SetWindowLong(IntPtr hWnd, GWL index, UInt32 newLong);

        /// <summary>ウインドウスタイルの設定</summary>
        [DllImport("user32.dll")]
        public static extern UInt32 SetWindowLong(IntPtr hWnd, GWL index, IntPtr newLong);
    }

    /// <summary>ウインドウスタイルのインデックス</summary>
    internal enum GWL : Int32
    {
        /// <summary>
        /// Sets a new address for the window procedure.
        /// You cannot change this attribute if the window 
        /// does not belong to the same process as the calling thread.
        /// </summary>
        WNDPROC = -4,

        /// <summary>Sets a new application instance handle.</summary>
        HINSTANCE = -6,

        /// <summary>Retrieves a handle to the parent window, if any.</summary>
        HWNDPARENT = -8,

        /// <summary>
        /// Sets a new identifier of the child window.
        /// The window cannot be a top-level window.
        /// </summary>
        ID = -12,

        /// <summary>Sets a new window style.</summary>
        STYLE = -16,

        /// <summary>Sets a new extended window style.</summary>
        EXSTYLE = -20,

        /// <summary>
        /// Sets the user data associated with the window.
        /// This data is intended for use by the application that created the window.
        /// Its value is initially zero.
        /// </summary>
        USERDATA = -21,
    }

    /// <summary>定数定義</summary>
    internal class WS
    {
        public const UInt32 SYSMENU             = 0x00080000;

        /// <summary>このスタイルで作成したウィンドウがドラッグ アンド ドロップ ファイルを受け入れることを示します。</summary>
        public const UInt32 EX_ACCEPTFILES      = 0x00000010;
        /// <summary>ウィンドウが表示されているときはトップレベル ウィンドウをタスク バーに強制的に表示します。</summary>
        public const UInt32 EX_APPWINDOW        = 0x00040000;
        /// <summary>ウィンドウの外観を 3D 表示にします。つまり、くぼみ線で縁取りします。</summary>
        public const UInt32 EX_CLIENTEDGE       = 0x00000200;
        /// <summary>ウィンドウのタイトル バーに疑問符を含めます。 ユーザーがこの疑問符をクリックすると、カーソルは、疑問符付きのポインターに変化します。 その後、ユーザーが子ウィンドウをクリックすると、子は WM_HELP メッセージを受信します。</summary>
        public const UInt32 EX_CONTEXTHELP      = 0x00000400;
        /// <summary>ウィンドウに対する複数の子ウィンドウがある場合は、ユーザーは Tab キーを使用して子ウィンドウ間を移動できます。</summary>
        public const UInt32 EX_CONTROLPARENT    = 0x00010000;
        /// <summary>WS_CAPTION スタイル フラグを dwStyle パラメーターの中で指定した場合に、タイトル バーと共に二重境界線を持つウィンドウを(オプションで) 作成することを示します。</summary>
        public const UInt32 EX_DLGMODALFRAME    = 0x00000001;
        /// <summary>ウィンドウは、レイヤー化ウィンドウです。 このスタイルは、ウィンドウのクラス スタイルが CS_OWNDC または CS_CLASSDC のどちらかである場合は使用できません。 ただし、Windows 8 は子ウィンドウに対して WS_EX_LAYERED スタイルをサポートします。これは、従来のバージョンの Windows がトップレベル ウィンドウでのみサポートしていたスタイルです。</summary>
        public const UInt32 EX_LAYERED          = 0x00080000;
        /// <summary>ウィンドウに汎用の左揃えプロパティを提供します。 これは、既定の設定です。</summary>
        public const UInt32 EX_LEFT             = 0x00000000;
        /// <summary>クライアント領域の左側に垂直スクロール バーを配置します。</summary>
        public const UInt32 EX_LEFTSCROLLBAR    = 0x00004000;
        /// <summary>左から右への読み取り順序プロパティを使用してウィンドウのテキストを表示します。 これは、既定の設定です。</summary>
        public const UInt32 EX_LTRREADING       = 0x00000000;
        /// <summary>MDI 子ウィンドウを作成します。</summary>
        public const UInt32 EX_MDICHILD         = 0x00000040;
        /// <summary>このスタイルで作成された子ウィンドウが作成または破棄されたときに、子ウィンドウが自らの親ウィンドウに WM_PARENTNOTIFY メッセージを送信しないように指定します。</summary>
        public const UInt32 EX_NOPARENTNOTIFY   = 0x00000004;
        /// <summary>WS_EX_CLIENTEDGE および WS_EX_WINDOWEDGE スタイルを結合します。</summary>
        public const UInt32 EX_OVERLAPPEDWINDOW = EX_WINDOWEDGE | EX_CLIENTEDGE;
        /// <summary>WS_EX_WINDOWEDGEおよび WS_EX_TOPMOST スタイルを結合します。</summary>
        public const UInt32 EX_PALETTEWINDOW    = EX_WINDOWEDGE | EX_TOOLWINDOW | EX_TOPMOST;
        /// <summary>ウィンドウに汎用の右揃えプロパティを提供します。 これはウィンドウ クラスに依存します。</summary>
        public const UInt32 EX_RIGHT            = 0x00001000;
        /// <summary>クライアント領域の右側に垂直スクロール バーを配置します。 これは、既定の設定です。</summary>
        public const UInt32 EX_RIGHTSCROLLBAR   = 0x00000000;
        /// <summary>右から左への読み取り順序プロパティを使用してウィンドウのテキストを表示します。</summary>
        public const UInt32 EX_RTLREADING       = 0x00002000;
        /// <summary>ユーザー入力を受け取らない項目で使用する、3D 境界線スタイルを持つウィンドウを作成します。</summary>
        public const UInt32 EX_STATICEDGE       = 0x00020000;
        /// <summary>フローティング ツール バーとして使用することを意図したツール ウィンドウを作成します。 ツール ウィンドウには、通常のタイトル バーより短いタイトル バーがあり、ウィンドウ タイトルは小さいフォントを使用して描画されます。 ツール ウィンドウは、タスク バーの中、またはユーザーが Alt + Tab キーを押したときに表示されるウィンドウには表示されません。</summary>
        public const UInt32 EX_TOOLWINDOW       = 0x00000080;
        /// <summary>ウィンドウが非アクティブになっているときも含め、このスタイルで作成するウィンドウが、最前面以外の他のすべてのウィンドウより手前にとどまって表示されることを指定します。 アプリケーションは SetWindowPos のメンバー関数を使用して、この属性を追加または削除することができます。</summary>
        public const UInt32 EX_TOPMOST          = 0x00000008;
        /// <summary>このスタイルで作成されるウィンドウが透明であることを示します。 つまり、このウィンドウより奥にあるすべてのウィンドウは、このウィンドウによって隠されることはありません。 このスタイルで作成したウィンドウは、自らより奥にあるすべての兄弟ウィンドウが更新された後でのみ、WM_PAINT メッセージを受信します。</summary>
        public const UInt32 EX_TRANSPARENT      = 0x00000020;
        /// <summary>ウィンドウが、浮き出し効果のあるボーダーを持つことを示します。	</summary>
        public const UInt32 EX_WINDOWEDGE       = 0x00000100;
    }

}
