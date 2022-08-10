using Framework;

namespace dubletLib
{
    public class DubletSettings : Settings
    {
        public DubletSettings() : base(Settings.MakeSettingsPath("webviewtest"))
        {
        }
        
        
        /// <summary>
        /// splitterDistance1 is distance between LH WebView and RH webView
        /// </summary>
        public int SplitterDistance1 { get => GetInt("SplitterDistance1"); set => SetInt("SplitterDistance1", value); }

        /// <summary>
        /// splitterDistance2 is distance between RH WebView and textLog panel
        /// </summary>
        public int SplitterDistance2 { get => GetInt("SplitterDistance2"); set => SetInt("SplitterDistance2", value); }
    

    }
}
