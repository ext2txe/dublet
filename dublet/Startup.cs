using Framework;

namespace dublet
{
    public partial class MainBrowserForm
    {
        private void Startup()
        {
            _project = new dubletLib.DubletProject();
            _settings = _project.Settings;

            BaseUtils.GeometryFromString(_settings.WindowGeometry, this);

            splitContainer1.SplitterDistance = _settings.SplitterDistance1;
            splitContainer2.SplitterDistance = _settings.SplitterDistance2;
            Text = $"{_project.ProjectName} v{_project.Version}";
        }
    }
}
