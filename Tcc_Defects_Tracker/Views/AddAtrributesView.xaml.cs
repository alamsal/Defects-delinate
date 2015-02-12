using System;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.Geometry;
using Tcc_Defects_Tracker.ViewModel;

namespace Tcc_Defects_Tracker.Views
{
    public partial class AddAtrributesView 
    {
        public AddAtrributesView(IApplication arcMapApplication,IGeometry geometry )
        {
            InitializeComponent();
            
            AddAttributesViewModel viewModel= new AddAttributesViewModel();
            viewModel.ArcMapApplication = arcMapApplication;
            viewModel.PolygonGeometry = geometry;

            this.DataContext = viewModel;

            if (viewModel.CloseAction == null)
            {
                viewModel.CloseAction = new Action(() => this.Close());
            }
        }
    }
}
