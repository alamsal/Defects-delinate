using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Geodatabase;

namespace Tcc_Defects_Tracker.FeatureClass
{
   public interface IAddFeatureClass
   {
       void AddFeatureClassToMap(IWorkspace workspace, IMxDocument mxDocument, string datasetName,
                                 string featureClassName);
   }
}
