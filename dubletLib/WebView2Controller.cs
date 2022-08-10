using Microsoft.Web.WebView2.WinForms;
using Microsoft.Web.WebView2.Core;
using System;
using System.Windows.Forms;

namespace dubletLib
{
    public class WebView2Controller
    {
        public Label StatusTextBox = null;
        public UcTabPage TabParent = null;

        public string Name = null;
        public WebView2Controller(WebView2 wv)
        {
            SetWebView2EventHandlers(wv);
        }


        /// <summary>
        /// set event handlers for WebView2
        /// </summary>
        /// <param name="wv"></param>
        /// <exception cref="Exception"></exception>
        private void SetWebView2EventHandlers(WebView2 wv)
        {
            string senderName = wv.Name;
            //            LogMsg("SetWebView2EventHandlers() called");

            int step = 10;
            try
            {
                step = 20;
                if (wv.CoreWebView2 != null)
                {
                    step = 30;
                    wv.CoreWebView2.NavigationStarting += EnsureHttps;
                    step = 40;
                    wv.CoreWebView2.SourceChanged += SourceChanged;
                    step = 50;
                    //wv.CoreWebView2.ContentLoading += ContentLoading;
                    step = 60;
                    //wv.CoreWebView2.HistoryChanged += HistoryChanged;
                    step = 70;
                    wv.CoreWebView2.NavigationCompleted += NavigationCompleted;
                    step = 80;
                    wv.CoreWebView2.NewWindowRequested += NewWindowRequested;
                    step = 90;
                    //wv.CoreWebView2.ClientCertificateRequested += ClientCertificateRequested;
                    step = 100;
                    wv.CoreWebView2.NavigationStarting += NavigationStarting;
                    step = 110;
                    //wv.CoreWebView2InitializationCompleted += CoreWebView2InitializationCompleted;
                    step = 120;
                    //wv.CoreWebView2.WebResourceResponseReceived += CoreWebView2_WebResourceResponseReceived;
                    step = 130;
                    //wv.CoreWebView2.ProcessFailed += ProcessFailed;
                    step = 140;
                    //wv.CoreWebView2.WebResourceRequested += WebResourceRequested;

                    // should be called by CoreWebView2InitializationCompleted
                    step = 150;

                    //wv.CoreWebView2.WebMessageReceived += CoreWebView2_WebMessageReceived;

                    //LogMsg($"SetWebView2EventHandlers() WebMessageReceived handler set to [wv.CoreWebView2.WebMessageReceived]");
                    step = 160;
                    //SetUserAgent(wv);

                }
                else
                {
                    step = 80;
                    throw new Exception("WebView2 is null");
                }
            }
            catch (Exception ex)
            {
                string msg = $"InitWebView2.Page() @ [{step}] EXCEPTION {ex.Message}";
                HandleException(msg);
            }

        }

        private void LogMsg(string msg)
        {
            string m = $"{DateTime.Now.ToString("hh:MM:ss.fff")} - {msg}" + Environment.NewLine;
            if (StatusTextBox != null)
            {
                StatusTextBox.Text = m;
            }
        }

        private void NavigationStarting(object sender, CoreWebView2NavigationStartingEventArgs e)
        {
            LogMsg($"NavigationStarting() [{e.Uri}]");
        }

        private void SourceChanged(object sender, CoreWebView2SourceChangedEventArgs e)
        {

        }

        private void EnsureHttps(object sender, CoreWebView2NavigationStartingEventArgs e)
        {
            LogMsg($"{Name} : {e.Uri}");

        }

        private void NavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs e)
        {

            //textLog.AppendText(e.)
        }

        private void NewWindowRequested(object sender, CoreWebView2NewWindowRequestedEventArgs e)
        {
            e.Handled = true;

            //e.NewWindow = null;            
            TabParent.NewPage(e.Uri);
            //wv3.Source = new Uri(e.Uri);
            //wv3.Refresh();
        }

        private void HandleException(string msg)
        {
            throw new NotImplementedException();
        }

    }
}
