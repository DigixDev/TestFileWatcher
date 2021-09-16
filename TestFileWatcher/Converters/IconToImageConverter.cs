using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TestFileWatcher.Core;

namespace TestFileWatcher.Converters
{
    public class IconToImageConverter : IMultiValueConverter
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

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] == null || values[1] == null)
                return null;

            var path = values[0].ToString();
            var isDir = System.Convert.ToBoolean(values[1]);


            if (isDir)
                return GetFolderIcon();
            else
            {
                var ico = System.Drawing.Icon.ExtractAssociatedIcon(path);
                return Imaging.CreateBitmapSourceFromHIcon(ico.Handle, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
