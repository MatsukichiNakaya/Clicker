using System;
using System.Windows;
using System.Windows.Interop;
using static Project.Windows.WindowStyle;
using static Project.Windows.WindowStyleAPI;

namespace Clicker
{
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(Object sender, StartupEventArgs e)
        {
            var main = new MainWindow();
            ToSlipThrough(main);
            main.Topmost = true;
            main.Show();
        }

        /// <summary>
        /// ウインドウをクリックしても下のウインドウへイベントがすり抜ける
        /// </summary>
        /// <param name="window"></param>
        private void ToSlipThrough(Window window)
        {
            // 初期化イベントでウインドウスタイルを設定するように関数を設定
            window.SourceInitialized += ((sender, e) => {
                var handle = new WindowInteropHelper(window).Handle;
                UInt32 style = GetLong(handle, GWL.EXSTYLE);

                SetLong(handle, GWL.EXSTYLE, style | WS.EX_TRANSPARENT);
            });
        }
    }
}
