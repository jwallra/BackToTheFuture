using Microsoft.HockeyApp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace RidoClassicWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            HockeyClient.Current.Configure("fcc565b312a544479688a3e34afeab1c");
        }
    }
}
