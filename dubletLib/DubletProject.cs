using Framework;

namespace dubletLib
{
    public class DubletProject : BaseProject
    {
        private DubletSettings _settings = null;
        public DubletSettings Settings { get => _settings; }

        public string ProjectName = "dublet";

        public string Version = "0.1.3";
        public DubletProject()
        {
            _settings = new DubletSettings();
        }
    }
}
