//using Microsoft.HockeyApp;
//using Microsoft.AppCenter.Analytics;
using System;
using System.Reflection;
using System.Windows;
using Windows.ApplicationModel.Background;
using Windows.Data.Xml.Dom;
using Windows.Storage;
using Windows.UI.Notifications;

namespace BackToTheFuture
{

    class Analytics
    {
        internal static void TrackEvent(string s) { }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        UWPBackgroundTaskCatalog catalog = new UWPBackgroundTaskCatalog();
        public MainWindow()
        {
            Analytics.TrackEvent("MainPage");
            //HockeyClient.Current.TrackPageView("MainPage");
            InitializeComponent();
            Analytics.TrackEvent("MainPage");

        }

        private void TabItem_Loaded(object sender, RoutedEventArgs e)
        {
            var a = Assembly.GetExecutingAssembly();
            var v = a.GetName().Version;
            labelPath.Text = a.CodeBase;           
            labelVersion.Text = $"AssemblyVersion {v.Major}.{v.Minor}.{v.Build}.{v.Revision} running on {a.ImageRuntimeVersion} RuntimeVersion";
            labelAppx.Text = ExecutionMode.PFN;
            Analytics.TrackEvent("RunTimeInfo:" + labelAppx.Text);
        }

      

        private void TabItem_Loaded_1(object sender, RoutedEventArgs e)
        {
            string result = string.Empty;
            try
            {
                var o = new ClassicCOM.MyClassClass();
                result = o.GetInfo();
            }
            catch(Exception ex)
            {
                result = ex.Message;
            }
            labelCOMInfo.Text = result;
            Analytics.TrackEvent("COMInfo: " + result);
        }

        private void buttonShowToast_Click(object sender, RoutedEventArgs e)
        {
            //var o = new ClassicCOM.MyClassClass();
            //var result = o.Salute("UWP");
            var result = "Toast from WPF";
            if (ExecutionMode.IsAppx)
            {
                ShowToast(result);
            }
            Analytics.TrackEvent("ShowToast: " + result);
            
        }

        private void ShowToast(string msg)
        {
            ToastTemplateType toastTemplate = ToastTemplateType.ToastText02;
            XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);
            XmlNodeList toastTextElements = toastXml.GetElementsByTagName("text");
            toastTextElements[0].AppendChild(toastXml.CreateTextNode(msg));
            toastTextElements[1].AppendChild(toastXml.CreateTextNode(DateTime.Now.ToString()));
            ToastNotification toast = new ToastNotification(toastXml);
            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }

        private void MethodFailing()
        {
            throw new InvalidOperationException("Fake IVO exception");
        }

        private void UnhandledEx_Click(object sender, RoutedEventArgs e)
        {
            MethodFailing();
        }

        private void buttonHandledException_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MethodFailing();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Analytics.TrackEvent(ex.ToString());
                // Environment.Exit(-1);
            }
        }

        private void COMException_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var o = new ClassicCOM.MyClassClass();
                o.MakeMeFail();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.HResult.ToString());
                Analytics.TrackEvent(ex.ToString());
            }
        }

        private void UnhandledCOMException_Click(object sender, RoutedEventArgs e)
        {
            var o = new ClassicCOM.MyClassClass();
            o.MakeMeFail();
        }

        private void LoadBackgroundTasks()
        {
            var registeredTasks = catalog.GetRegisteredTasks();
            if (registeredTasks.Count == 0)
            {
                InfoBGTask.Text = "No BG Tasks Registered";
                RegisterButton.IsEnabled = true;
                UnregisterButton.IsEnabled = false;
                return;
            }
            RegisterButton.IsEnabled = false;
            UnregisterButton.IsEnabled = true;
            InfoBGTask.Text = string.Empty;
            foreach (var item in registeredTasks)
            {
                InfoBGTask.Text += item;
            }
            var url = ApplicationData.Current.LocalSettings.Values["UrlToVerify"].ToString();
            if (!string.IsNullOrEmpty(url))
            {
                UrlToTest.Text = url;
            }
        }


        private void TabItem_Loaded_2(object sender, RoutedEventArgs e)
        {
            if (ExecutionMode.IsAppx)
            {
                LoadBackgroundTasks();
            }
                
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            ApplicationData.Current.LocalSettings.Values["UrlToVerify"] = UrlToTest.Text;
            catalog.RegisterBackgroundTask("MySampleTask");
            LoadBackgroundTasks();

        }

        private void UnregisterButton_Click(object sender, RoutedEventArgs e)
        {
            catalog.Unregister("MySampleTask");
            LoadBackgroundTasks();
        }
    }
}
