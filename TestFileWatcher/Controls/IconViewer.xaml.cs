using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
using TestFileWatcher.Core;

namespace TestFileWatcher.Controls
{
    /// <summary>
    /// Interaction logic for IconViewer.xaml
    /// </summary>
    public partial class IconViewer : UserControl
    {
        private static BitmapImage _folderIcon;

        private BitmapImage GetFolderIcon()
        {
            if (_folderIcon == null)
            {
                _folderIcon = new BitmapImage();
                _folderIcon.BeginInit();
                _folderIcon.UriSource = new Uri("pack://application:,,,/TestFileWatcher;component/images/folder_icon.png");
                _folderIcon.EndInit();
            }
            return _folderIcon;
        }

        public ImageSource ImageSource
        {
            get { return (ImageSource)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }

        public string FilePath
        {
            get { return (string)GetValue(FilePathProperty); }
            set { SetValue(FilePathProperty, value); }
        }

        public static readonly DependencyProperty FilePathProperty =            DependencyProperty.Register("FilePath", typeof(string), typeof(IconViewer), new PropertyMetadata("", OnFilePathChanged));
        public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.Register("ImageSource", typeof(ImageSource), typeof(IconViewer), new PropertyMetadata(null));

        private static void OnFilePathChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var p=d as IconViewer;
            p.UpdateImageSource(e.NewValue.ToString());
        }

        public void UpdateImageSource(string path)
        {
            if (File.Exists(path))
            {
                var ico = System.Drawing.Icon.ExtractAssociatedIcon(path);
                this.ImageSource = Imaging.CreateBitmapSourceFromHIcon(ico.Handle, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            }
            else
                this.ImageSource = GetFolderIcon();
        }

        public IconViewer()
        {
            InitializeComponent();
        }
    }
}
