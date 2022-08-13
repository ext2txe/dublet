using Microsoft.Web.WebView2.WinForms;
using System.Drawing;
using System.Windows.Forms;

namespace dubletLib
{
    /// <summary>
    /// Creates a Windows Forms TabPage that contains controls and 
    /// functionality to support a browser tab
    /// </summary>
    public class BrowserTab : TabPage
    {
        private TabPage _tab;
        public TabPage Tab = null;
        private WebView2 _wv2 = null;

        private UcTabPage _ucTab;

        public BrowserTab(string tabTitle)
        {
            Tab = new TabPage();
            Tab.BackColor = Color.LimeGreen;
            Tab.Text = tabTitle;
            //_wv2 = new WebView2();
            //_wv2.Visible = true;
            //_wv2.BackColor = SystemColors.Control;
            //_wv2.Refresh();
            _ucTab = new UcTabPage("about:blank");
            Tab.Controls.Add(_ucTab);
            _ucTab.WV.Source = new System.Uri("https://ideatect.com");
            _ucTab.Dock = DockStyle.Fill;
            Tab.Refresh();
        }

    }
}
