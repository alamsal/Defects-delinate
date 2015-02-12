using System;
using System.Windows.Forms;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Tcc_Defects_Tracker.FeatureClass;
using Tcc_Defects_Tracker.Model;

namespace Tcc_Defects_Tracker.GDBOperations
{
    public static class EditDefectFeatures
    {
        private static bool _editing;
        public static bool StartEditSession(IFeatureClass featureClass)
        {
            try
            {
                IWorkspaceEdit workspaceEdit = ((IDataset)featureClass).Workspace as IWorkspaceEdit;

                if (workspaceEdit != null)
                {
                    workspaceEdit.StartEditing(true);
                    workspaceEdit.StartEditOperation();
                    _editing = true;
                }
               
            }
            catch(Exception e)
            {
                MessageBox.Show("Can not edit layer " + ((IFeatureLayer)featureClass).Name + " because it is in use by another application. You may need to manually remove the lock file associated with this layer.");
                return false;
            }

            return _editing;
        }

        public static void StopEditSession(IFeatureClass featureClass)
        {
            IWorkspaceEdit workspaceEdit = ((IDataset)featureClass).Workspace as IWorkspaceEdit;
            if (workspaceEdit != null)
            {
                workspaceEdit.StopEditOperation();
                workspaceEdit.StopEditing(true);
                _editing = false;
            }
           
        }



        public static IFeature CreateFeatureInGDB(IFeatureClass featureClass, IGeometry geometry, DefectShape defectShape)
        {
            IFeature feature = featureClass.CreateFeature();
            try
            {
                feature.Shape = geometry;

                feature.set_Value(featureClass.FindField(EnumDefectAttributes.Appearance.ToString()), defectShape.Appearance);
                feature.set_Value(featureClass.FindField(EnumDefectAttributes.Defect_type.ToString()), defectShape.DefectType);
                feature.set_Value(featureClass.FindField(EnumDefectAttributes.Solution.ToString()), defectShape.Solution);
                feature.set_Value(featureClass.FindField(EnumDefectAttributes.Status.ToString()), defectShape.Status);
            }
            catch (Exception e)
            {
                MessageBox.Show("Can not create feature in GDB " + e.Message);
            }

            return feature;
        }
    }
}
