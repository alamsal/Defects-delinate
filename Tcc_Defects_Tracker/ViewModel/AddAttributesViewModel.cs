using System;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.Geometry;
using Tcc_Defects_Tracker.GDBOperations;
using Tcc_Defects_Tracker.Model;

namespace Tcc_Defects_Tracker.ViewModel
{
    public class AddAttributesViewModel : ViewModelBase
    {
        #region private members
        
        private AssociatedMXDs _associatedMxDs;
        private DefectShape _defectShape;
        private LandsatScenes _landsatScenes;
        private ModelType _modelType;
        private MrlcVintage _mrlcVintage;
        private ProductionProcess _productionProcess;
        private Source _source;

        private RelayCommand _saveCommand;
       
        #endregion private members
        
        #region Ctor
        
        public AddAttributesViewModel()
        {
            _associatedMxDs = new AssociatedMXDs();
            _defectShape = new DefectShape();
            _landsatScenes = new LandsatScenes();
            _modelType = new ModelType();
            _mrlcVintage = new MrlcVintage();
            _productionProcess = new ProductionProcess();
            _source = new Source();

        }
        
        #endregion Ctor

        #region properties

        public IApplication ArcMapApplication;
        public IGeometry PolygonGeometry;
        public Action CloseAction { get; set; }
 
        public AssociatedMXDs MXD
        {
            get { return _associatedMxDs; }
            set { Set("MxDs", ref _associatedMxDs, value); }
        }

        public DefectShape Defect
        {
            get { return _defectShape; }
            set { Set("Defect", ref _defectShape, value); }
        }

        public LandsatScenes Scene
        {
            get { return _landsatScenes; }
            set { Set("Scene", ref _landsatScenes,value); }
        }

        public ModelType Model
        {
            get { return _modelType; }
            set { Set("Model", ref _modelType, value); }
        }

        public MrlcVintage Mrlc
        {
            get { return _mrlcVintage; }
            set { Set("Mrlc", ref _mrlcVintage, value); }
        }

        public ProductionProcess PProcess
        {
            get { return _productionProcess; }
            set { Set("PProcess", ref _productionProcess, value); }
        }

        public Source DefectSource
        {
            get { return _source; }
            set { Set("Source", ref _source, value); }
        }

        #endregion properties

        #region commands

        public RelayCommand SaveAttributesCommand
        {
            get
            {
                if (_saveCommand != null)
                {
                    return _saveCommand;
                }
                _saveCommand = new RelayCommand(SaveDefectAttributes);
                return _saveCommand;
            }
        }

        #endregion commands



        #region helper functions

        private void SaveDefectAttributes()
        {
            IStoreAttributesInSHP storeAttributes= new StoreAttributesInSHP(ArcMapApplication,PolygonGeometry);
            storeAttributes.StoreAttribues(Defect, Scene,DefectSource,MXD,Model,Mrlc,PProcess);

            CloseWindow();
        }

        //Close the wpf window
        private void CloseWindow()
        {
            CloseAction();
        }

        #endregion helper functions





    }
}
