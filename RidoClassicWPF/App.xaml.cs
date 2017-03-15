using Microsoft.HockeyApp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Windows.ApplicationModel.Background;

namespace RidoClassicWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
          
            HockeyClient.Current.Configure("fcc565b312a544479688a3e34afeab1c");
            //await HockeyClient.Current.SendCrashesAsync();
        }

       

    }
}
