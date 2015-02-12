using System;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Framework;

namespace Tcc_Defects_Tracker.Extension
{
    public class ArcMapWorkspaceHelpers
    {
        public IWorkspace GetCurrentWorkspace(string gdbPath, IApplication mapApplication)
        {
            IFeatureWorkspace featureWorkspace = GetCurrentFeatureWorkspace(gdbPath, mapApplication);
            IWorkspace workspace = featureWorkspace as IWorkspace;
            return workspace;
        }

        public IFeatureWorkspace GetCurrentFeatureWorkspace(string gdbPath, IApplication mapApplication)
        {
            Type factoryType = Type.GetTypeFromProgID("esriDataSourcesGDB.FileGDBWorkspaceFactory");
            IWorkspaceFactory workspaceFactory = (IWorkspaceFactory)Activator.CreateInstance(factoryType);
            IFeatureWorkspace featureWorkspace = workspaceFactory.OpenFromFile(gdbPath, mapApplication.hWnd) as IFeatureWorkspace;

            return featureWorkspace;
        }




    }

}