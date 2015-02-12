using System;
using System.Windows.Forms;
using ESRI.ArcGIS.Framework;

namespace Tcc_Defects_Tracker.Extension
{
    class ArcMapWpfWrapper : IWin32Window
    {
        private IApplication _arcMapApplication;

        public ArcMapWpfWrapper(IApplication mApplication)
        {
            _arcMapApplication = mApplication;
        }

        public IntPtr Handle
        {
            get { return new IntPtr(_arcMapApplication.hWnd); }

        }
    }
}
