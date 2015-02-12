using System;
using ESRI.ArcGIS.Framework;
using Tcc_Defects_Tracker.ViewModel;

namespace Tcc_Defects_Tracker.Views
{
    /// <summary>
    /// Interaction logic for ConnectGDBView.xaml
    /// </summary>
    public partial class ConnectGDBView
    {
        public ConnectGDBView(IApplication arcMap)
        {
                InitializeComponent();

                ConnectGDBViewModel viewModel = new ConnectGDBViewModel();
                viewModel.ArcMapApplication = arcMap;
                DataContext = viewModel;
                
                if (viewModel.CloseAction == null)
                {
                    viewModel.CloseAction = new Action(() => this.Close());
                }
            

        }
    }
}
