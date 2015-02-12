using ESRI.ArcGIS.Framework;
using Tcc_Defects_Tracker.ViewModel;

namespace Tcc_Defects_Tracker.Views
{
    /// <summary>
    /// Interaction logic for QueryGDBView.xaml
    /// </summary>
    public partial class QueryGDBView 
    {
        public QueryGDBView(IApplication arcMap)
        {
            InitializeComponent();

            QueryGDBViewModel queryGdbViewModel = new QueryGDBViewModel();
            queryGdbViewModel.ArcMapApplication = arcMap;

            DataContext = queryGdbViewModel;



        }

    }
}
