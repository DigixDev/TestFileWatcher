using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using Forms = System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TestFileWatcher.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Forms.NotifyIcon _notifyIcon;

        public MainWindow()
        {
            InitializeComponent();

            var menu = new Forms.ContextMenu();
            var exitItem = new Forms.MenuItem("Exit");
            exitItem.Click += (s, e) =>  Application.Current.Shutdown();

            _notifyIcon = new Forms.NotifyIcon();
            _notifyIcon.ContextMenu = menu;
            _notifyIcon.Text = "Test File Watcher";
            _notifyIcon.Icon = Properties.Resources.Gakuseisean_Aire_Download_Folder;
            _notifyIcon.Visible = true;
        }
    }
}
