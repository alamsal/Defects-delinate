using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.esriSystem;

namespace Tcc_Defects_Tracker.GDBTables
{
    public class TablesBuilder : ITablesBuilder
    {
        #region private members
        private readonly IWorkspace _workspace;

        #endregion private members

        #region Ctor
        public TablesBuilder(IWorkspace workspace)
        {
            _workspace = workspace;
        }

        #endregion Ctor

        public void CreateTablesIfNotExist()
        {
            //Check existance of table
            IWorkspace2 ws2 = _workspace as IWorkspace2;
            var FieldName = Enum.GetValues(typeof(EnumTableFields));

            int index = 0;

            foreach (EnumTableNames tableName in Enum.GetValues(typeof(EnumTableNames)))
            {

                if (!ws2.get_NameExists(esriDatasetType.esriDTTable, tableName.ToString()))
                {
                    List<string> attribute = new AttributesHelper().GetAttributesList(FieldName.GetValue(0).ToString(), FieldName.GetValue(index + 1).ToString());
                    BuildTable(attribute, _workspace, tableName.ToString());
                }

                index++;
            }

        }



        #region private helpers
       
        // Build tables inside GDB
        private void BuildTable(List<string> attributeFields, IWorkspace workspace, String tableName)
        {
            try
            {
                //Check existance of table
                IWorkspace2 ws2 = workspace as IWorkspace2;

                // Cast the workspace to the IFeatureWorkspace interface.
                IFeatureWorkspace featureWorkspace = (IFeatureWorkspace)workspace;

                // Create the fields collection.
                IFields fields = new FieldsClass();
                IFieldsEdit fieldsEdit = (IFieldsEdit)fields;

                // Add the ObjectID field.

                IField oidField = new FieldClass();
                IFieldEdit oidFieldEdit = (IFieldEdit)oidField;
                oidFieldEdit.Name_2 = "OID";
                oidFieldEdit.Type_2 = esriFieldType.esriFieldTypeOID;
                fieldsEdit.AddField(oidField);


                // Add the text field.
                foreach (string attributeField in attributeFields)
                {
                    IField nameField = new FieldClass();
                    IFieldEdit nameFieldEdit = (IFieldEdit)nameField;
                    nameFieldEdit.Name_2 = attributeField;
                    nameFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
                    fieldsEdit.AddField(nameField);
                }

                // Use IFieldChecker to create a validated fields collection.
                IFieldChecker fieldChecker = new FieldCheckerClass();
                IEnumFieldError enumFieldError = null;
                IFields validatedFields = null;
                //fieldChecker.ValidateWorkspace = workspace;
                fieldChecker.Validate(fields, out enumFieldError, out validatedFields);

                // The enumFieldError enumerator can be inspected at this point to determine 
                // which fields were modified during validation.

                // Create a UID for the CLSID parameter of CreateTable. For a regular object class,
                // you should use esriGeodatabase.Object.
                UIDClass instanceUID = new UIDClass();
                instanceUID.Value = "esriGeodatabase.Object";

                featureWorkspace.CreateTable(tableName, validatedFields, instanceUID, null, "");

                // ITable table = featureWorkspace.CreateTable(tableName, validatedFields, instanceUID, null, "");
               
            }
            catch(Exception e)
            {
                MessageBox.Show("Error in creating tables in GDB "+e.Message);
            }
            finally
            {
                GC.Collect();
            }


        }

        #endregion private helpers






    }
}
