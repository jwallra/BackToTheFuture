using Microsoft.HockeyApp;
using System;
using System.Reflection;
using System.Windows;
using Windows.Data.Xml.Dom;
using Windows.Storage;
using Windows.UI.Notifications;

namespace RidoClassicWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            
            HockeyClient.Current.TrackPageView("MainPage");
            InitializeComponent();

        }

        private void TabItem_Loaded(object sender, RoutedEventArgs e)
        {
            
            var a = Assembly.GetExecutingAssembly();
            var v = a.GetName().Version;
            labelPath.Text = a.CodeBase;           
            labelVersion.Text = $"{v.Major}.{v.Minor}.{v.Build}.{v.Revision} running on {a.ImageRuntimeVersion}"; 
            labelAppx.Text = GetPackageNameIfAvailable();
            HockeyClient.Current.TrackEvent("RunTimeInfo:" + labelAppx.Text);
        }

        string GetPackageNameIfAvailable()
        {
            string pfn = string.Empty;
            try
            {
                var id = Windows.ApplicationModel.Package.Current.Id;
                var v = id.Version;
                pfn = $"{id.FamilyName} ";
                pfn += $"{v.Major}.{v.Minor}.{v.Build}.{v.Revision}";
            }
            catch (Exception ex)
            {
                pfn = ex.Message;
            }
            return pfn;
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
            HockeyClient.Current.TrackEvent("COMInfo: " + result);
        }

        private void buttonShowToast_Click(object sender, RoutedEventArgs e)
        {
            var o = new ClassicCOM.MyClassClass();
            var result = o.Salute("UWP");
            ShowToast(result);
            HockeyClient.Current.TrackEvent("ShowToast: " + result);
            
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
                (HockeyClient.Current as HockeyClient).HandleException(ex);
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
                (HockeyClient.Current as HockeyClient).HandleException(ex);
            }
        }

        private void UnhandledCOMException_Click(object sender, RoutedEventArgs e)
        {
            var o = new ClassicCOM.MyClassClass();
            o.MakeMeFail();
        }

        private void buttonAddUrl_Click(object sender, RoutedEventArgs e)
        {
            ApplicationData.Current.LocalSettings.Values["UrlToVerify"] = UrlToTest.Text;
        }

        private void TabItem_Loaded_2(object sender, RoutedEventArgs e)
        {
            UrlToTest.Text = ApplicationData.Current.LocalSettings.Values["UrlToVerify"] as string;
        }
    }
}
