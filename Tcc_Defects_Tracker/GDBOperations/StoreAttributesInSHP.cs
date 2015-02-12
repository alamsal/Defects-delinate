using System;
using System.Windows.Forms;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.Geometry;
using Tcc_Defects_Tracker.Extension;
using Tcc_Defects_Tracker.FeatureClass;
using Tcc_Defects_Tracker.GDBConnection;
using Tcc_Defects_Tracker.GDBTables;
using Tcc_Defects_Tracker.LayerOperations;
using Tcc_Defects_Tracker.Model;

namespace Tcc_Defects_Tracker.GDBOperations
{
    public class StoreAttributesInSHP : IStoreAttributesInSHP
    {
        private IApplication _arcMapApplication;
        private IGeometry _polygonGeometry;
        
        #region Ctor
        public StoreAttributesInSHP(IApplication arcMap, IGeometry polyGeometry)
        {
            _arcMapApplication = arcMap;
            _polygonGeometry = polyGeometry;
            

        }
        #endregion Ctor

        public void StoreAttribues(DefectShape defectShape, LandsatScenes scene, Source defectSource,AssociatedMXDs mxDs,ModelType modelType,MrlcVintage mrlcVintage, ProductionProcess productionProcess)
        {
           //Store into shp
           IFeature newFeature= StartStoringAttributesInSHP(defectShape);
           
            //Store into tables
           if(newFeature!=null)
           {
               IWorkspace workspace = new ArcMapWorkspaceHelpers().GetCurrentWorkspace(ApplicationStatusHolder.CurrentGDBPathName, _arcMapApplication);
               AddAttributesToTable addAttributesToTable= new AddAttributesToTable(scene, defectSource,mxDs,modelType,mrlcVintage,productionProcess,workspace);
               addAttributesToTable.ShapeFeatureID = newFeature.OID.ToString();
               addAttributesToTable.storeAttributesInTables();
           }

        }
        
        #region private helpers
        private IFeature StartStoringAttributesInSHP(DefectShape defectShape)
        {
            IFeature newFeature = null;
            try
            {
                IMxDocument mxDocument = _arcMapApplication.Document as IMxDocument;
                IMap map = mxDocument.FocusMap;
                
                //Get feature dataset
                IFeatureWorkspace featureWorkspace = new ArcMapWorkspaceHelpers().GetCurrentFeatureWorkspace(ApplicationStatusHolder.CurrentGDBPathName, _arcMapApplication);
                IFeatureClass featureClass = featureWorkspace.OpenFeatureClass(ApplicationStatusHolder.CurrentFeatureClassName);

                bool startEdit=EditDefectFeatures.StartEditSession(featureClass);
                
                if(startEdit)
                {
                    newFeature = EditDefectFeatures.CreateFeatureInGDB(featureClass, _polygonGeometry, defectShape);
                    newFeature.Store();
                    EditDefectFeatures.StopEditSession(featureClass);
                }
                
               // map.RecalcFullExtent();
               // mxDocument.ActiveView.Refresh();
               MapLayerHelpers.RefreshMapViews(mxDocument, map);

            }
            catch(Exception e)
            {
                MessageBox.Show("Cann't create a feature.."+ e.Message);
            }
            finally
            {
                GC.Collect();
            }

            return newFeature;

        }

        

        #endregion private helpers




    }
}
