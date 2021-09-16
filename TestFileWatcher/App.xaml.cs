using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace TestFileWatcher
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private const string APP_ID = "39ff347a-0ce5-48e1-9bae-40b6139d4403";
        private Mutex _mutex;

        public App()
        {
            bool created = true;

            _mutex = new Mutex(true, APP_ID, out created);
            if (created == false)
            {
                Environment.Exit(0);
            }
        } 

    }
}
