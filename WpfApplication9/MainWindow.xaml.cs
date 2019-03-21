using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApplication9
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(MainWindow_Loaded);
            SourceInitialized += window_SourceInitialized;

            SourceInitialized += delegate
            {
                var helper = new WindowInteropHelper(this);
                IntPtr windowHandle = helper.Handle; //Get the handle of this window
                IntPtr hmenu = GetSystemMenu(windowHandle, 0);

                //remove the menu item
                RemoveMenu(hmenu, 0, MF_DISABLED | MF_BYPOSITION);
                RemoveMenu(hmenu, 1, MF_DISABLED | MF_BYPOSITION);
                RemoveMenu(hmenu, 2, MF_DISABLED | MF_BYPOSITION);
                RemoveMenu(hmenu, 3, MF_DISABLED | MF_BYPOSITION);
                RemoveMenu(hmenu, 4, MF_DISABLED | MF_BYPOSITION);
                RemoveMenu(hmenu, 5, MF_DISABLED | MF_BYPOSITION);

                DrawMenuBar(windowHandle); //Redraw the menu bar
            };
        }

        [DllImport("user32.dll", EntryPoint = "GetSystemMenu")]
        private static extern IntPtr GetSystemMenu(IntPtr hwnd, int revert);

        [DllImport("user32.dll", EntryPoint = "GetMenuItemCount")]
        private static extern int GetMenuItemCount(IntPtr hmenu);

        [DllImport("user32.dll", EntryPoint = "RemoveMenu")]
        private static extern int RemoveMenu(IntPtr hmenu, int npos, int wflags);

        [DllImport("user32.dll", EntryPoint = "DrawMenuBar")]
        private static extern int DrawMenuBar(IntPtr hwnd);

        private const int MF_BYPOSITION = 0x0400;
        private const int MF_DISABLED = 0x0002;


        static void window_SourceInitialized(object sender, EventArgs e)
        {
            var window = (Window)sender;

            var helper = new WindowInteropHelper(window);
            IntPtr windowHandle = helper.Handle; //Get the handle of this window
            IntPtr hmenu = GetSystemMenu(windowHandle, 0);
            int cnt = GetMenuItemCount(hmenu);

            for (int i = cnt - 1; i >= 0; i--)
            {
                RemoveMenu(hmenu, i, MF_DISABLED | MF_BYPOSITION);
            }
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {

            Style _style = null;
            if (Microsoft.Windows.Shell.SystemParameters2.Current.IsGlassEnabled == true)
            {
                _style = (Style)Resources["GadgetStyle"];
            }
            this.Style = _style;
        }
    }
}
