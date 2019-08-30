using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Project.API.User.Hook;
using Project.Windows.Hook;
using Project.Serialization.Xml;

namespace Clicker
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Int32 ESC_KEY = 27;

        private LLKeyHook _Keyhook;

        private MacroSet _Macros;
        private HashSet<Int32> _KeyCodes;

        private MacroSet _MacroRecord;

        public MainWindow()
        {
            InitializeComponent();

            _Keyhook = new LLKeyHook();
            _Keyhook.SetKeyHook();
            _Keyhook.SetMouseHook();
            _Keyhook.KeyDown += new KeyEvent(KeyDownEvetAsync);
            _Keyhook.KeyUp += new KeyEvent(KeyUpEvet);
            _Keyhook.MouseMove += new MouseEvent(Mousehook_MouseMove);
            _Keyhook.MouseLeftDown += new MouseEvent(Mousehook_MouseLeftDown);

            this._MacroRecord = new MacroSet {
                Macros = new List<Macro> {
                    new Macro()
                }
            };
            this._MacroRecord.Macros[0].MacroCommands = new List<Command>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private void Mousehook_MouseMove(Int32 x, Int32 y)
        {
            var pos = NativeMethod.GetMousePos();
            this.KeyGetDispBoxX.Text = $"x = {pos.X: 0000;-0000}";
            this.KeyGetDispBoxY.Text = $"y = {pos.Y: 0000;-0000}";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private void Mousehook_MouseLeftDown(Int32 x, Int32 y)
        {
            var pos = NativeMethod.GetMousePos();
            this._MacroRecord.Macros[0].MacroCommands.Add(new Command() {
                CommandType = 1,
                X = pos.X,
                Y = pos.Y,
            });           
        }

        private void Window_Loaded(Object sender, RoutedEventArgs e)
        {
            this.Background = new SolidColorBrush(Color.FromArgb(10, 0, 0, 0));
            this.WindowState = WindowState.Maximized;

            this._Macros = XmlSerializer.Load<MacroSet>(@".\Macro.xml");
            this._KeyCodes = new HashSet<Int32>(this._Macros.Macros.Select(x => x.KeyCode));
        }

        // フックしたキーを受け取るメソッド
        public async void KeyDownEvetAsync(Int32 key)
        {
            if (key == ESC_KEY) { this.Close(); }

            if (this._KeyCodes.Contains(key)) {
                var macro = this._Macros.Macros.First(x => x.KeyCode == key);

                for (Int32 i = 0; i < macro.MacroCommands.Count; i++) {
                    switch (macro.MacroCommands[i].CommandType) {
                        case 1:
                            NativeMethod.SetCursor(macro.MacroCommands[i].X, macro.MacroCommands[i].Y);
                            break;
                        case 2:
                            NativeMethod.MouseClick();
                            break;
                        case 3:
                            await Task.Delay(macro.MacroCommands[i].Delay);
                            break;
                        case 4:
                            i -= 3;
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        public void KeyUpEvet(Int32 key) { }

        private void Window_Closing(Object sender, System.ComponentModel.CancelEventArgs e)
        {
            this._MacroRecord.Macros[0].KeyCode = 65;
            this._MacroRecord.Macros[0].Name = "RecMacro";
            XmlSerializer.Save(this._MacroRecord, @".\rec.xml");
            // キーフック解除
            _Keyhook.UnSet();
        }
    }
}