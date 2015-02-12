using System;
using System.Windows.Forms;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;

namespace Tcc_Defects_Tracker.FeatureClass
{
    public class AddFeatureClass : IAddFeatureClass
    {
       public void AddFeatureClassToMap(IWorkspace workspace,IMxDocument mxDocument, string datasetName, string featureClassName)
       {
           try
           {   
               IEnumDataset enumDS = workspace.get_Datasets(esriDatasetType.esriDTFeatureDataset);
               IDataset featureDataSet = enumDS.Next();

               while (featureDataSet != null)
               {
                   if (featureDataSet.Name == datasetName)
                   {
                       IEnumDataset featureClassesInFDS = featureDataSet.Subsets;
                       IDataset uniqueFeatrueClassAsDataSet = featureClassesInFDS.Next();

                       while (uniqueFeatrueClassAsDataSet != null)
                       {
                           if (uniqueFeatrueClassAsDataSet is IFeatureClass && uniqueFeatrueClassAsDataSet.Name == featureClassName)
                           {
                               IFeatureClass uniqueFeatureClass = uniqueFeatrueClassAsDataSet as IFeatureClass;
                               IFeatureLayer uniqueFeatrueLayer = new FeatureLayerClass();
                               uniqueFeatrueLayer.Name = uniqueFeatrueClassAsDataSet.Name;
                               uniqueFeatrueLayer.FeatureClass = uniqueFeatureClass;
                               mxDocument.AddLayer(uniqueFeatrueLayer);
                           }
                           uniqueFeatrueClassAsDataSet = featureClassesInFDS.Next();
                       }
                   }

                   featureDataSet = enumDS.Next();

               }
               mxDocument.ActiveView.Refresh();

           }
           catch (Exception e)
           {
               MessageBox.Show(" Unable to add feature class into ArcMap- " + e.ToString());
           }

       }
    }
}
