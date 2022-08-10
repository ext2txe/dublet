using System;
using System.Windows.Forms;
using dubletLib;
using Microsoft.Web.WebView2.WinForms;

namespace dubletLib
{
    public partial class UcTabPage : UserControl
    {
        public WebView2Controller wvController = null;
        private WebView2 _wv2 = null;

        public UcTabPage(WebView2 wv2)
        {
            _wv2 = wv2;
            InitializeComponent();
        }

        public void NewPage(string uri)
        {

        }
    }
}
