using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;

namespace Tcc_Defects_Tracker.GDBConnection
{
    public class GDBDatasetsHandler : IGDBDatasetsHandler
    {
       #region private members

        private readonly IWorkspace _workspace;
        private readonly string _gdppath;
       
       #endregion private members

        #region Ctor

        public GDBDatasetsHandler(string gdbPath, IWorkspace workspace )
        {
           _gdppath = gdbPath;
           _workspace = workspace;
        }
        
        #endregion Ctor

        #region public members

        public List<string> GetDatasetListFromGDB()
        {
            List<string> featureDatasetList = new List<string>();

            if (CheckExistanceOfGDB(_gdppath))
            {
                try
                {
                    featureDatasetList = GetDatasetListFromGDB(_workspace);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }

            }
            return featureDatasetList;
        }

        
        public List<string> GetFeatureClassListFromDataset(string featureDatasetName)
        {
            List<string> featureClassList=GetFeatureClassListFromDataset(_workspace, featureDatasetName);
            return featureClassList;
        }

        
        #endregion public members

        #region private helpers

        private bool CheckExistanceOfGDB(string gdbPath)
        {
            bool existance = false;

            existance = Directory.Exists(gdbPath);

            return existance;
        }

        private List<string> GetDatasetListFromGDB(IWorkspace workspace)
        {
            List<string> datasetNameList = new List<string>();

            IEnumDataset dataset = workspace.get_Datasets(esriDatasetType.esriDTFeatureDataset);

            if (dataset == null)
                return datasetNameList;

            dataset.Reset();

            IDataset dsn;

            while ((dsn = dataset.Next()) != null)
            {
                datasetNameList.Add(dsn.Name);

            }

            return datasetNameList;

        }


        private List<string> GetFeatureClassListFromDataset(IWorkspace workspace, string featureDatasetName)
        {
             List<string> featureClassList = new List<string>();

            IEnumDatasetName enumDS = workspace.get_DatasetNames(esriDatasetType.esriDTFeatureDataset);

            //first FeatureDataset name
            IDatasetName featureDataSet = enumDS.Next();

            while (featureDataSet != null)
            {
                if (featureDataSet.Name == featureDatasetName)
                {
                    //get all FeatureClasses name inside a FeatureDataset
                    IEnumDatasetName featureClassesInFDS = featureDataSet.SubsetNames;
                    IDatasetName singleFeatureClassAsDataset = featureClassesInFDS.Next();

                    while (singleFeatureClassAsDataset != null)
                    {
                        if (singleFeatureClassAsDataset is IFeatureClassName)
                        {
                            featureClassList.Add(singleFeatureClassAsDataset.Name);
                        }
                        singleFeatureClassAsDataset = featureClassesInFDS.Next();
                    }

                }
                featureDataSet = enumDS.Next();
            }

            return featureClassList;
        }


        #endregion private helpers




    }
}
