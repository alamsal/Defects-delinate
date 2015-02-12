using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.SystemUI;
using Tcc_Defects_Tracker.GDBConnection;
using Tcc_Defects_Tracker.LayerOperations;

namespace Tcc_Defects_Tracker.ToolBarItems
{
    /// <summary>
    /// Summary description for DefectLayerComboBoxCommand.
    /// </summary>
    [Guid("03886c41-58a0-4939-bfbb-96da93e6c66f")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("Tcc_Defects_Tracker.ToolBarItems.DefectLayerComboBoxCommand")]
    public sealed class DefectLayerComboBoxCommand : BaseCommand, IComboBox
    {
        #region COM Registration Function(s)
        [ComRegisterFunction()]
        [ComVisible(false)]
        static void RegisterFunction(Type registerType)
        {
            // Required for ArcGIS Component Category Registrar support
            ArcGISCategoryRegistration(registerType);

            //
            // TODO: Add any COM registration code here
            //
        }

        [ComUnregisterFunction()]
        [ComVisible(false)]
        static void UnregisterFunction(Type registerType)
        {
            // Required for ArcGIS Component Category Registrar support
            ArcGISCategoryUnregistration(registerType);

            //
            // TODO: Add any COM unregistration code here
            //
        }

        #region ArcGIS Component Category Registrar generated code
        /// <summary>
        /// Required method for ArcGIS Component Category registration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryRegistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            MxCommands.Register(regKey);

        }
        /// <summary>
        /// Required method for ArcGIS Component Category unregistration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryUnregistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            MxCommands.Unregister(regKey);

        }

        #endregion
        #endregion

        private IMxDocument m_doc;
        private int m_cookie;
        private IComboBoxHook m_comboBoxHook;
        private Dictionary<int, string> m_list;
        private MapLayerHelpers _mapLayerHelpers;
        
        public DefectLayerComboBoxCommand()
        {
            
            #region auto
            base.m_category = "Defects Tracker"; //localizable text
            base.m_caption = "Select Layer";  //localizable text
            base.m_message = "Select Layer";  //localizable text 
            base.m_toolTip = "Select Layer";  //localizable text 
            base.m_name = "Defect_Layer_ComboBox_Command";   //unique id, non-localizable (e.g. "MyCategory_ArcMapCommand")

            m_cookie = -1;

            #endregion auto

            _mapLayerHelpers = new MapLayerHelpers();

        }

        #region BaseCommand


        public override void OnCreate(object hook)
        {
            if (hook == null)
                return;

            m_comboBoxHook = hook as IComboBoxHook;

            if (m_comboBoxHook != null)
            {
                IApplication application = m_comboBoxHook.Hook as IApplication;
                if (application != null) 
                {
                    m_doc = application.Document as IMxDocument;
                   
                    IMap map = m_doc.FocusMap;
                    IActiveViewEvents_Event activeViewEvents = map as IActiveViewEvents_Event;

                    activeViewEvents.ItemAdded += new IActiveViewEvents_ItemAddedEventHandler(activeViewEvents_ItemUpdate);
                    activeViewEvents.ItemDeleted += new IActiveViewEvents_ItemDeletedEventHandler(activeViewEvents_ItemUpdate);
                }
            }

            //Disable if it is not ArcMap
            if (m_comboBoxHook != null && m_comboBoxHook.Hook is IMxApplication)
                base.m_enabled = true;
            else
                base.m_enabled = false;

            // Populate combobox 
            m_list = new Dictionary<int, string>();

        
        }

        void activeViewEvents_ItemUpdate(object item)
        {
            UpdateLayers();
        }

        public override void OnClick()
        {
            
        }

        #endregion BaseCommand

        #region IComboBox Members

        public int DropDownHeight
        {
            get { return 4; }
        }

        public string DropDownWidth
        {
            get { return "WWWWWWWWWWWWWWWWW"; }
        }

        public bool Editable
        {
            get { return true; }
        }

        public string HintText
        {
            get { return "Select a layer to edit.."; }
        }

        public void OnEditChange(string editString)
        {
        }

        public void OnEnter()
        {
        }

        public void OnFocus(bool set)
        {
        }

        public void OnSelChange(int cookie)
        {
            bool exitloop = false;
            if (cookie == -1)
                return;

            foreach (KeyValuePair<int, string> item in m_list)
            {
                //All feature layers are selectable if "Select All" is selected;
                //otherwise, only the selected layer is selectable.

                //string fl = item.Value;
                //if (fl == null)
                //  continue;

                if (cookie == item.Key)
                {
                    MessageBox.Show(item.Value);
                    ApplicationStatusHolder.CurrentFeatureClassName = item.Value;
                    break;

                }

            }

            //Fire ContentsChanged event to cause TOC to refresh with new selected layers.
            m_doc.ActiveView.ContentsChanged(); ;

        }

        public bool ShowCaption
        {
            get { return true; }
        }

        public string Width
        {
            get { return "WWWWWWWWWWWWWX"; }
        }

        #endregion

        #region private helpers
       
        private void UpdateLayers()
        {
            m_comboBoxHook.Clear();
            m_list.Clear();

            List<string> layersName = GetLayersFromTOC(m_doc);
            foreach (string name in layersName)
            {
                m_cookie = m_comboBoxHook.Add(name);
                m_list.Add(m_cookie, name);
            }
        }

        private List<string> GetLayersFromTOC(IMxDocument mxDocument)
        {
            List<string> layersName = new MapLayerHelpers().GetAllLayersInTOC(mxDocument);
            return layersName;
        }

        #endregion private helpers


    }
}
