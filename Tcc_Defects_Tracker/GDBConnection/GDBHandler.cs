using System.Collections.Generic;
using ESRI.ArcGIS.Geodatabase;
using Tcc_Defects_Tracker.Extension;

namespace Tcc_Defects_Tracker.GDBConnection
{
   public class GDBHandler:BasePropertyNotifier
   {
       private string _gdbPath;
       private string _datasetName;
       private string _featuresetName;
       private string _newFeaturesetName;

       private List<string> _datasetNameList;
       private List<string> _featuresetNameList;
       
       public string GDBPath
       {
           get { return _gdbPath; }
           set 
           {
               _gdbPath = value;
               OnPropertyChanged("GDBPath");
           }
       }

       public string DatasetName
       {
           get { return _datasetName; }
           set
           {
               _datasetName = value;
               OnPropertyChanged("DatasetName");
           }
       }


       public string FeaturesetName
       {
           get { return _featuresetName; }
           set
           {
               _featuresetName = value;
               OnPropertyChanged("FeaturesetName");
           }
       }

       public string NewFeaturesetName
       {
           get { return _newFeaturesetName; }
           set
           {
               _newFeaturesetName = value;
               OnPropertyChanged("NewFeaturesetName");
           }
       }

       public List<string> DatasetNameList
       {
           get { return _datasetNameList; }
           set
           {
               _datasetNameList = value;
               OnPropertyChanged("DatasetNameList");
           }
       }

       public List<string> FeaturesetNameList
       {
           get { return _featuresetNameList; }
           set
           {
               _featuresetNameList = value;
               OnPropertyChanged("FeaturesetNameList");
           }
       }
   }
}
