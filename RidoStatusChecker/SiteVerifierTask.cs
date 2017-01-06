using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.Data.Xml.Dom;
using Windows.Storage;
using Windows.UI.Notifications;

namespace RidoStatusChecker
{
    public sealed class SiteVerifierTask : IBackgroundTask
    {
        public  void Run(IBackgroundTaskInstance taskInstance)
        {
            var msg = string.Empty;
            var url = ApplicationData.Current.LocalSettings.Values["UrlToVerify"] as string;
            if (string.IsNullOrEmpty(url))
            {
                url = "http://dev.windows.com";
                msg = "Using default URL. " + url;
            }
            else
            {
                msg = "Using URL. " + url;
            }
            var time = TimeToFirstByte(url);
            msg += $"took {time.ToString()} ms";
            ShowToast(msg);
        }

        private long TimeToFirstByte(string url)
        {
            Stopwatch clock = Stopwatch.StartNew();
            var http = new HttpClient();
            var response = http.GetAsync(url).GetAwaiter().GetResult();
            response.EnsureSuccessStatusCode();
            var elapsed = clock.ElapsedMilliseconds;
            clock.Stop();
            return elapsed;

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
    }
}
