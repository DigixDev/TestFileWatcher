using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using TestFileWatcher.Core;

namespace TestFileWatcher.Models
{
    public class DataModel: BindableObject
    {
        private ImageSource _icon;

        public string Name { get; set; }
        public string Path { get; set; }
        public long Size { get; set; }
        public DateTime Date { get; set; }
        public bool IsDir { get; set; }
        public ImageSource Icon
        {
            get => _icon;
            set
            {
                if (_icon != value)
                {
                    _icon = value;
                    RaisePropertyChanged();
                }
            }
        }
    }
}
