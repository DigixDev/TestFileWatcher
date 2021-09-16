using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TestFileWatcher.Core
{
    //https://www.meziantou.net/check-if-the-current-user-is-an-administrator.htm

    public static class AdminTools
    {
        public static bool IsRunningAdministrator()
        {
            using (var identity = System.Security.Principal.WindowsIdentity.GetCurrent())
            {
                var principal = new System.Security.Principal.WindowsPrincipal(identity);
                return principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator);
            }
        }

        public static void RestartAsAdmin()
        {
            var appPath = Assembly.GetExecutingAssembly().Location;
            var psi = new ProcessStartInfo
            {
                FileName = appPath,
                UseShellExecute = true,
                Verb = "runas",    
            };

            Process.Start(psi);  
            Application.Current.Shutdown();
        }
    }
}
