using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.ArcMapUI;
using Tcc_Defects_Tracker.Extension;
using Tcc_Defects_Tracker.Views;

namespace Tcc_Defects_Tracker.ToolBarItems
{
    /// <summary>
    /// Summary description for QueryGDBCommand.
    /// </summary>
    [Guid("6351a824-702e-48e8-bb99-af9e2b870bbb")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("Tcc_Defects_Tracker.ToolBarItems.QueryGDBCommand")]
    public sealed class QueryGDBCommand : BaseCommand
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

        private IApplication m_application;
        public QueryGDBCommand()
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = "Defects Tracker"; //localizable text
            base.m_caption = "Query Geodatabase";  //localizable text
            base.m_message = "Query Geodatabase";  //localizable text 
            base.m_toolTip = "Query Geodatabase";  //localizable text 
            base.m_name = "Query_GDB_Command";   //unique id, non-localizable (e.g. "MyCategory_ArcMapCommand")

            try
            {
                //
                // TODO: change bitmap name if necessary
                //
                string bitmapResourceName = GetType().Name + ".png";
                base.m_bitmap = new Bitmap(GetType(), bitmapResourceName);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Image");
            }
        }

        #region Overridden Class Methods

        /// <summary>
        /// Occurs when this command is created
        /// </summary>
        /// <param name="hook">Instance of the application</param>
        public override void OnCreate(object hook)
        {
            if (hook == null)
                return;

            m_application = hook as IApplication;

            //Disable if it is not ArcMap
            if (hook is IMxApplication)
                base.m_enabled = true;
            else
                base.m_enabled = false;

            // TODO:  Add other initialization code
        }

        /// <summary>
        /// Occurs when this command is clicked
        /// </summary>
        public override void OnClick()
        {
            QueryGDBView queryGdbView = new QueryGDBView(m_application);
            ArcMapWpfWrapper wrapper = new ArcMapWpfWrapper(m_application);
            var helper = new WindowInteropHelper(queryGdbView);
            helper.Owner = wrapper.Handle;
            queryGdbView.ShowInTaskbar = false;
            queryGdbView.Show();
        }

        #endregion
    }
}
