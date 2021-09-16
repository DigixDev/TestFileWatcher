using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace TestFileWatcher.Converters
{
    class FileSizeToStringConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values[0] == null || values[1] == null)
                return "";

            var isDir=System.Convert.ToBoolean(values[1]);
            if (isDir)
                return "folder";
            
            var size = System.Convert.ToInt64(values[0]);
            return size.ToString("### ### ### ### ###");

        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
