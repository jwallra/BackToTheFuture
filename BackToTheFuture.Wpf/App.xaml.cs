//using Microsoft.AppCenter;
//using Microsoft.AppCenter.Analytics;
//using Microsoft.AppCenter.Crashes;
//using Microsoft.HockeyApp;
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
          
            //HockeyClient.Current.Configure("fcc565b312a544479688a3e34afeab1c");
            //await HockeyClient.Current.SendCrashesAsync();
            //AppCenter.Start("5a6499b7-add4-4c26-a16b-909950329f63", typeof(Analytics), typeof(Crashes));
        }

       

    }
}
