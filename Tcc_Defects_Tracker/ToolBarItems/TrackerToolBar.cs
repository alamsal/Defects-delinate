using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.ADF.BaseClasses;

namespace Tcc_Defects_Tracker.ToolBarItems
{
    /// <summary>
    /// Summary description for TrackerToolBar.
    /// </summary>
    [Guid("182b7818-3dd0-47b5-925f-229a07e2759f")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("Tcc_Defects_Tracker.TrackerToolBar")]
    public sealed class TrackerToolBar : BaseToolbar
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
            MxCommandBars.Register(regKey);
        }
        /// <summary>
        /// Required method for ArcGIS Component Category unregistration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryUnregistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            MxCommandBars.Unregister(regKey);
        }

        #endregion
        #endregion

        public TrackerToolBar()
        {
            BeginGroup();
            AddItem("Tcc_Defects_Tracker.ToolBarItems.ConnectGDBCommand");
            
            BeginGroup();
            AddItem("Tcc_Defects_Tracker.ToolBarItems.DefectLayerComboBoxCommand");
           
            BeginGroup();
            AddItem("Tcc_Defects_Tracker.ToolBarItems.DefectEditorTool");
            
            //BeginGroup();
            //AddItem("Tcc_Defects_Tracker.ToolBarItems.DefectSelectTool");
            
            BeginGroup();
            AddItem("Tcc_Defects_Tracker.ToolBarItems.QueryGDBCommand");
        }

        public override string Caption
        {
            get
            {
                //TODO: Replace bar caption
                return "2 TCC Defects Toolbar";
            }
        }
        public override string Name
        {
            get
            {
                //TODO: Replace bar ID
                return "TrackerToolBar";
            }
        }
    }
}