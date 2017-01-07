using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.Data.Xml.Dom;
using Windows.Storage;
using Windows.UI.Notifications;
using Windows.Web.Http;

namespace RidoStatusChecker
{
    public sealed class SiteVerifierTask : IBackgroundTask
    {
        public  async void Run(IBackgroundTaskInstance taskInstance)
        {
            BackgroundTaskDeferral deferral = taskInstance.GetDeferral();

            var msg = string.Empty;
            msg = await MeasureRequestTime();
            ShowToast(msg);

            deferral.Complete();

        }

        private async Task<string> MeasureRequestTime()
        {
            string msg;
            try
            {
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
                var time = await TimeToFirstByte(url);
                msg += $"took {time.ToString()} ms";
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            return msg;
        }

        private async Task<long> TimeToFirstByte(string url)
        {
            Stopwatch clock = Stopwatch.StartNew();
            var http = new HttpClient();
            var response = await http.GetAsync(new Uri(url));
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
