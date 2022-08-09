using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
using System.Diagnostics;
using System;
using Framework;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace dubletLib
{
    public class Page
    {
        /// <summary>
        /// configure handler for delegate to receive status messages from this class
        /// </summary>
        /// <param name="message"></param>
        public delegate void StatusMessageDelegate(string message);
        public StatusMessageDelegate StatusMessageHandler { get; set; }

        protected WebView2 _wv;
        public bool CheckUserHTTPOverride = true;

        private string _saveFolder = "";
        /// <summary>
        /// folder where screenshots are to be saved
        /// </summary>
        public string SaveFolder { get =>_saveFolder; set => SetSaveFolder(value); } 

        private void SetSaveFolder(string value)
        {
            if (!Directory.Exists(value))
            {
                Directory.CreateDirectory(value);
                Status($"SetSaveFolder() create folder {value}");
            }
            _saveFolder = value;
        }

        
        /// <summary>
        /// find a better way to handle nullable delegate,
        /// for now set status message handler to this initially
        /// </summary>
        /// <param name="message"></param>
        protected void StupidDoNothingMethod(string message) {}

        public Page(WebView2 webView)
        {
            _wv = webView;
            StatusMessageHandler = StupidDoNothingMethod;
            Initialize(webView);
        }

        protected async void Initialize(WebView2 webview)
        {
            int step = 10;
            try
            {
                await _wv.EnsureCoreWebView2Async();
                step = 20;
                if (_wv.CoreWebView2 != null)
                {
                    step = 30;
                    _wv.CoreWebView2.NavigationStarting += EnsureHttps;
                    step = 40;
                    _wv.CoreWebView2.SourceChanged += SourceChanged;
                    step = 50;
                    _wv.CoreWebView2.ContentLoading += ContentLoading;
                    step = 60;
                    _wv.CoreWebView2.HistoryChanged += HistoryChanged;
                    step = 70;
                    _wv.CoreWebView2.NavigationCompleted += NavigationCompleted;
                }
                else
                {
                    step = 80;
                    throw new Exception("WebView.CoreWebView2 is null");
                }
            }
            catch (Exception ex)
            {
                string msg = $"WebViewLib.Page() @ [{step}] EXCEPTION {ex.Message}";
                Status(msg);
                throw new Exception(msg);
            }
        }

        private void Status(string message)
        {
            if (StatusMessageHandler != StupidDoNothingMethod)
            {
                StatusMessageHandler(message);
            }
        }

        private void SourceChanged(object? sender, CoreWebView2SourceChangedEventArgs e)
        {
            Status($"SourceChanged");
        }

        private void ContentLoading(object? sender, CoreWebView2ContentLoadingEventArgs e)
        {
            Status($"ContentLoading");

        }

        private void HistoryChanged(object? sender, object e)
        {
            Status($"HistoryChanged");
        }

        private void EnsureHttps(object? sender, CoreWebView2NavigationStartingEventArgs e)
        {
            String uri = e.Uri;
            if (!uri.StartsWith("https://"))
            {
                if (CheckUserHTTPOverride)
                {
                    if (BaseUtils.Question($"{uri} is potentially unsafe", "Allow HTTP?") == DialogResult.Yes)
                    {
                        return;
                    }
                    else
                    {
                        e.Cancel = true;
                    }
                }
                _wv.CoreWebView2.ExecuteScriptAsync($"alert('{uri} is not safe, try an https link')");
                e.Cancel = true;
            }
        }

        public void NavigateTo(string url)
        {
            _wv.CoreWebView2.Navigate(url);
            Status($"navigate to {url}");
        }

        public string DocumentTitle()
        {
            return _wv.CoreWebView2.DocumentTitle;
        }

        private void NavigationCompleted(object? sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            string err = $"{e.WebErrorStatus}";
            Status($"NavigationCompleted {err}");
        }

        public async void TakeScreenshot()
        {
            using (MemoryStream stream = new MemoryStream())
            //using (InMemoryRandomAccessStream stream = new InMemoryRandomAccessStream())
            {
                await _wv.CoreWebView2.CapturePreviewAsync(CoreWebView2CapturePreviewImageFormat.Png, stream);
                stream.Seek(0, SeekOrigin.Begin);

                // here you can add saving to a file or copying to clipboard
                string filename = Path.Combine( SaveFolder, $"{DateTime.Now.ToString("hhmmssfff")}_screenshot.png");
                using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
                {
                    stream.CopyTo(fs);
                    fs.Flush();
                }
            }
        }

        private async Task InitializeCoreWebView2Async()
        {
            //initialize CorewWebView2
            await _wv.EnsureCoreWebView2Async();
        }


        /// <summary>
        /// load index.html from resource file and open it in browser
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void LoadJavascript(object sender, EventArgs e)
        {
            //show MS Edge version -- also ensures that an exception will be raised if proper MS Edge version isn't installed
            Debug.WriteLine(CoreWebView2Environment.GetAvailableBrowserVersionString());

            //initialized CorewWebView2
            await InitializeCoreWebView2Async();

            //get HTML
            string html = HelperLoadResource.ReadResource("index.html");

            _wv.NavigateToString(html);

        }

        /// <summary>
        /// locate button with inner text match and click on it
        /// </summary>
        /// <param name="innerText"></param>
        /// <exception cref="Exception"></exception>
        public async void ClickButtonWithInnerText(string innerText)
        {
            int step = 10;
            try
            {
                string jsCode = HelperLoadResource.ReadResource("TestButtonClick.js");
                jsCode += System.Environment.NewLine;
                jsCode += "clickDesiredButtonByInnerText('" + innerText + "');";


                var result = await _wv.CoreWebView2.ExecuteScriptAsync(jsCode);

                Debug.WriteLine("result: " + result);
            }
            catch (Exception ex)
            {
                string msg = $"WebViewLib.ClickButtonWithInnerText() @ [{step}] EXCEPTION {ex.Message}";
                Status(msg);
                throw new Exception(msg);
            }
        }
    }
}