using System.Collections.Generic;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using System.Linq;
using ESRI.ArcGIS.Geodatabase;

namespace Tcc_Defects_Tracker.LayerOperations
{
    public class MapLayerHelpers
    {
        public List<string> GetAllLayersInTOC(IMxDocument mxDocument)
        {
           List<string> allMapLayers = new List<string>();
           IMap map = mxDocument.FocusMap;
           IEnumLayer enumLayer = map.Layers; ;
           ILayer layer = enumLayer.Next();

           while (layer != null)
           {
               //MessageBox.Show(layer.Name);
               allMapLayers.Add(layer.Name);
               layer = enumLayer.Next();
           }
            return allMapLayers;
        }

        public List<string> GetLayersBothExistsInGDBnTOC(List<string>layersInTOC,List<string>layersInGDB)
        {
            List<string> layersInTocAndGDB = layersInTOC.Intersect(layersInGDB).ToList();
            return layersInTocAndGDB;
        }

        public IFeatureClass GetFeatureClassFromLayer( ILayer layer)
        {
            IFeatureLayer featureLayer = (IFeatureLayer)layer; 
            IFeatureClass featureClass = featureLayer.FeatureClass;
            return featureClass;
        }

        public string GetLayerName(object item)
        {
            string name = null;
            if (item is ILayer)
            {
                ILayer layer = item as ILayer;
                name = layer.Name;
            }
            return name;
        }

        public static void RefreshMapViews(IMxDocument mxDocument, IMap map)
        {
            map.RecalcFullExtent();
            mxDocument.ActiveView.Refresh();
        }


    }
}
