using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.Geodatabase;

namespace Tcc_Defects_Tracker.FeatureClass
{
    public interface ICreateNewFeatureClass
    {
        void CreateNewFeature(IWorkspace workspace, IApplication mApplication, string featureClsName,
                              string featureDatasetName);
    }
}
