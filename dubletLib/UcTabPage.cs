using System;
using System.Windows.Forms;
using Microsoft.Web.WebView2.WinForms;
using Microsoft.Web.WebView2.Core;
using System.Threading.Tasks;

namespace dubletLib
{
    public partial class UcTabPage : UserControl
    {
        public WebView2Controller wvController = null;
        private WebView2 _wv2 = null;

        public WebView2 WV { get => wv2;  set => wv2 = value; }
        public string Url { get => textUrl2.Text; set => textUrl2.Text = value; }

        public UcTabPage(string url)
        {
            InitWebView();
            InitializeComponent();
        }

        private async Task InitWebView()
        {
            WV = new WebView2();

            await WV.EnsureCoreWebView2Async();

            return ;
        }

        public void NewPage(string uri)
        {

        }
    }
}
