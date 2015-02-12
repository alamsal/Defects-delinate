using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.ArcMapUI;
using Tcc_Defects_Tracker.Views;

namespace Tcc_Defects_Tracker.ToolBarItems
{
    /// <summary>
    /// Summary description for DefectEditorTool.
    /// </summary>
    [Guid("814c7417-e40b-4f5b-9668-b6edea8cb93f")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("Tcc_Defects_Tracker.ToolBarItems.DefectEditorTool")]
    public sealed class DefectEditorTool : BaseTool
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
        public DefectEditorTool()
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = "Defects Tracker"; //localizable text 
            base.m_caption = "Defect Editor Tool";  //localizable text 
            base.m_message = "Defect Editor Tool";  //localizable text
            base.m_toolTip = "Defect Editor Tool";  //localizable text
            base.m_name = "Defect_Editor_Tool";   //unique id, non-localizable (e.g. "MyCategory_ArcMapTool")
            try
            {
                //
                // TODO: change resource name if necessary
                //
                string bitmapResourceName = GetType().Name + ".png";
                base.m_bitmap = new Bitmap(GetType(), bitmapResourceName);
                base.m_cursor = new System.Windows.Forms.Cursor(GetType(), GetType().Name + ".cur");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap");
            }
        }

        #region Overridden Class Methods

        /// <summary>
        /// Occurs when this tool is created
        /// </summary>
        /// <param name="hook">Instance of the application</param>
        public override void OnCreate(object hook)
        {
            m_application = hook as IApplication;

            //Disable if it is not ArcMap
            if (hook is IMxApplication)
                base.m_enabled = true;
            else
                base.m_enabled = false;

            // TODO:  Add other initialization code
        }

        /// <summary>
        /// Occurs when this tool is clicked
        /// </summary>
        public override void OnClick()
        {
            
        }

        public override void OnMouseDown(int Button, int Shift, int X, int Y)
        {
            //Now editing starts

            //Access Map layer
            IMxDocument doc = m_application.Document as IMxDocument;

            //create geometery

            if (doc != null)
            {
                ESRI.ArcGIS.Display.IScreenDisplay screenDisplay = doc.ActiveView.ScreenDisplay;
            
                // Constant
                screenDisplay.StartDrawing(screenDisplay.hDC, (System.Int16)ESRI.ArcGIS.Display.esriScreenCache.esriNoScreenCache); // Explicit Cast
                ESRI.ArcGIS.Display.IRgbColor rgbColor = new ESRI.ArcGIS.Display.RgbColorClass();
                rgbColor.Red = 255;
                rgbColor.Transparency = 75;

                ESRI.ArcGIS.Display.IColor color = rgbColor; // Implicit Cast
                ESRI.ArcGIS.Display.ISimpleFillSymbol simpleFillSymbol = new ESRI.ArcGIS.Display.SimpleFillSymbolClass();
                simpleFillSymbol.Color = color;

                //Draw geometry
                ESRI.ArcGIS.Display.ISymbol symbol = simpleFillSymbol as ESRI.ArcGIS.Display.ISymbol; // Dynamic Cast
                ESRI.ArcGIS.Display.IRubberBand rubberBand = new ESRI.ArcGIS.Display.RubberPolygonClass();
                ESRI.ArcGIS.Geometry.IGeometry geometry = rubberBand.TrackNew(screenDisplay, symbol);

                //check for valid geometery
                if(geometry!=null)
                {
                    screenDisplay.SetSymbol(symbol);
                    screenDisplay.DrawPolygon(geometry);
                    screenDisplay.FinishDrawing();

                    //Open addattributes wpf form
                    AddAtrributesView addAtrributesView = new AddAtrributesView(m_application, geometry);
                    addAtrributesView.ShowInTaskbar = false;
                   // addAtrributesView.Show();
                    addAtrributesView.ShowDialog();
                }


            }


        }

        public override void OnMouseMove(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add DefectEditorTool.OnMouseMove implementation
        }

        public override void OnMouseUp(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add DefectEditorTool.OnMouseUp implementation
        }
        #endregion
    }
}
