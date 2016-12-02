using RidoClassicApp.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RidoClassicApp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            SetInstalledDate();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
            
        }
        

        private static void SetInstalledDate()
        {
            Settings appSettings = Settings.Default;
            if (appSettings.InstalledOn==DateTime.MinValue)
            {
                appSettings.InstalledOn = DateTime.Now;
            }
            appSettings.LastOpened = DateTime.Now;
            appSettings.TimesOpened++;
            appSettings.Save();
        }
    }
}
