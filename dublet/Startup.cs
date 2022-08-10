using Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dublet
{
    public partial class MainBrowserForm
    {
        private void Startup()
        {
            _settings = new dubletLib.DubletSettings();

            BaseUtils.GeometryFromString(_settings.WindowGeometry, this);

            splitContainer1.SplitterDistance = _settings.SplitterDistance1;
            splitContainer2.SplitterDistance = _settings.SplitterDistance2;
        }
    }
}
