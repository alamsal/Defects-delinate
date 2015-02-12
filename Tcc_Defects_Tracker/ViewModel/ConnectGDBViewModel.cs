using System;
using System.Windows.Forms;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.Geodatabase;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Tcc_Defects_Tracker.Extension;
using Tcc_Defects_Tracker.FeatureClass;
using Tcc_Defects_Tracker.GDBConnection;

namespace Tcc_Defects_Tracker.ViewModel
{
    public class ConnectGDBViewModel : ViewModelBase
    {
        #region private members
        
        private string _selectedDatasetName;
        private string _selectedFeaturesetName;
        private const string _newField = "New..";
        private GDBHandler _gdbHandler;
        
        private RelayCommand _selectGdbPath;
        private RelayCommand _addFeaturesetCommand;
        private RelayCommand _closeWindowCommand;
        
        #endregion private members

        #region properties
        public Action CloseAction { get; set; }
        public IApplication ArcMapApplication { get; set; }
        
        public string SelectedDatasetName
        {
            get { return _selectedDatasetName; }
            set { 
                Set("SelectedDatasetName", ref _selectedDatasetName, value);
                
                PopulateFeatureCombo(GDBConnect.GDBPath,value,_newField,ArcMapApplication);
            }
        }

        public string SelectedFeaturesetName
        {
            get { return _selectedFeaturesetName; }
            set { Set("SelectedFeaturesetName", ref _selectedFeaturesetName, value);}
        }

        public GDBHandler GDBConnect
        {
            get { return _gdbHandler; }
            set { Set("GDBConnect", ref _gdbHandler, value);}
           
        }

        #endregion properties

        #region Ctor

        public ConnectGDBViewModel()
        {
            _gdbHandler = new GDBHandler();
            
        }

        #endregion Ctor

        #region relay commands
        public RelayCommand SelectGDBPathCommand
        {
            get
            {
                if (_selectGdbPath != null)
                {
                    return _selectGdbPath;
                }
                _selectGdbPath = new RelayCommand(SelectGDBPath);
                return _selectGdbPath;
            }
        }

        public RelayCommand AddFeaturesetCommand
        {
            get
            {
                if (_addFeaturesetCommand != null)
                {
                    return _addFeaturesetCommand;
                }
                _addFeaturesetCommand = new RelayCommand(AddFeatureset);
                return _addFeaturesetCommand;
            }
        }

        public RelayCommand CloseWindowCommand
        {
            get
            {
                if (_closeWindowCommand != null)
                {
                    return _closeWindowCommand;
                }
                _closeWindowCommand = new RelayCommand(CloseWindow);
                return _closeWindowCommand;
            }
        }


        #endregion realy commands

        #region private helpers
        //select GDB
        private void SelectGDBPath()
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();

            if (result == DialogResult.OK)
            {   
                GDBConnect.GDBPath = fbd.SelectedPath;
               // GDBConnect.GDBPath = @"D:\Ashis_Work\TCCDefects\SampleDatasets\NewShp\sample.gdb";

                PopulateDatasetCombo(GDBConnect.GDBPath, ArcMapApplication);
                
            }

        }
        
        //Add featuresclass/layers to Map
        private void AddFeatureset()
        {
            AddFeatureClass addFeatureClass = new AddFeatureClass();
            IWorkspace workspace = GetCurrentIWorkspace(GDBConnect.GDBPath, ArcMapApplication);
            IMxDocument mxDocument = ArcMapApplication.Document as IMxDocument;
            
            string featureToAdd = SelectedFeaturesetName;
            string datasetOnAdd = SelectedDatasetName;
            string newFeaturesetCreate = GDBConnect.NewFeaturesetName;
            
            //Fill static variable 
            ApplicationStatusHolder.CurrentGDBPathName = GDBConnect.GDBPath;
            ApplicationStatusHolder.CurrentDataSetName = datasetOnAdd;

            if (SelectedFeaturesetName == _newField && GDBConnect.NewFeaturesetName != null)
            {
                CreateFeatureClass(workspace, ArcMapApplication, GDBConnect.NewFeaturesetName, datasetOnAdd);
                addFeatureClass.AddFeatureClassToMap(workspace, mxDocument, datasetOnAdd, newFeaturesetCreate);
                ApplicationStatusHolder.CurrentFeatureClassName = newFeaturesetCreate;
            }
            else
            {
                addFeatureClass.AddFeatureClassToMap(workspace, mxDocument, datasetOnAdd, featureToAdd);
                ApplicationStatusHolder.CurrentFeatureClassName = featureToAdd;
            }
        }

        //Create new featureClass/layer
        private void CreateFeatureClass(IWorkspace workspace, IApplication mApplication,string featureClassName,string featureDatasetName)
        {
            CreateNewFeatureClass createNewFeatureClass = new CreateNewFeatureClass();
            createNewFeatureClass.CreateNewFeature(workspace,mApplication,featureClassName,featureDatasetName);            
        }

        //Return currentworkspace
        private IWorkspace GetCurrentIWorkspace(string gdbPath, IApplication mapApplication)
        {
            IWorkspace workspace = new ArcMapWorkspaceHelpers().GetCurrentWorkspace(gdbPath, mapApplication);
            return workspace;
        }

        //Return datasets inside gdb
        private IGDBDatasetsHandler GetCurrentDatasetsFromGDB(string gdbPath ,IApplication mapApplication)
        {
            IWorkspace workspace = GetCurrentIWorkspace(gdbPath,mapApplication);
            GDBDatasetsHandler gdbDatasetsHandler = new GDBDatasetsHandler(gdbPath, workspace);
            return gdbDatasetsHandler;
        }

        //Fill dataset combo
        private void PopulateDatasetCombo(string gdbPath, IApplication mapApplication)
        {
            try
            {
                IGDBDatasetsHandler gdbDSHandler = GetCurrentDatasetsFromGDB(gdbPath, mapApplication);

                GDBConnect.DatasetNameList = gdbDSHandler.GetDatasetListFromGDB();
                SelectedDatasetName = GDBConnect.DatasetNameList[0];
            }
            catch(Exception e)
            {
                MessageBox.Show("GDB Dataset Error " + e.Message);
            }

        }

        //Fill feature combo
        private void PopulateFeatureCombo(string gdbPath,string selectedDatasetName,string newField,IApplication mapApplication)
        {
            try
            {

                IGDBDatasetsHandler gdbDSHandler = GetCurrentDatasetsFromGDB(gdbPath, mapApplication);

                GDBConnect.FeaturesetNameList = gdbDSHandler.GetFeatureClassListFromDataset(selectedDatasetName);
                GDBConnect.FeaturesetNameList.Insert(0, newField);
                
                SelectedFeaturesetName = GDBConnect.FeaturesetNameList[0];
            }
            catch (Exception e)
            {
                MessageBox.Show("GDB Dataset-FeatureClass Error " + e.Message);
            }
        }

        //Close the wpf window
        private void CloseWindow()
        {
            CloseAction();
        }

        #endregion

    }
}
