using System;
using System.Windows;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Framework;
using Tcc_Defects_Tracker.Extension;
using Tcc_Defects_Tracker.GDBConnection;

namespace Tcc_Defects_Tracker.GDBQuery
{
    public class QueryHelper
    {
        private readonly IApplication _arcMapApplication;

        public QueryHelper(IApplication mApplication)
        {
            _arcMapApplication = mApplication;
        }

        public string CreateWhereClause(string queryField,string condition,string searchValue)
        {
            string whereClause = queryField.Trim()+ " " + condition.Trim() + " " +"'"+searchValue.Trim()+"'";
            return whereClause.Trim();
        }

        public ITable OpenTableToQuery(string tableName)
        {
            ITable table = null;
            try
            {
                IFeatureWorkspace featureWorkspace = new ArcMapWorkspaceHelpers().GetCurrentFeatureWorkspace(ApplicationStatusHolder.CurrentGDBPathName, _arcMapApplication);
                table= OpenTable(featureWorkspace, tableName);
            }catch(Exception e)
            {
                MessageBox.Show("Unable to open table "+ e.Message);
            }
            return table;

        }

        public ICursor QueryRowViaQueryDef(IFeatureWorkspace featureWorkspace, String tables, String subFields, String whereClause)
        {
            // Create the query definition.
            IQueryDef queryDef = featureWorkspace.CreateQueryDef();

            // Provide a list of table(s) to join.
            queryDef.Tables = tables;

            // Declare the subfields to retrieve.
            queryDef.SubFields = subFields;

            // Assign a where clause to filter the results.
            queryDef.WhereClause = whereClause;

            // Evaluate queryDef to execute a database query and return a cursor to the application.
            ICursor cursor = queryDef.Evaluate();
            return cursor;
        }

        public string GetRowCountFromTable(ITable table, string whereClause)
        {
            ITable localTable = table;
            string rowCount=null;
            try
            {
              rowCount = localTable.RowCount(
              new QueryFilter
              {
                  SubFields = "*",
                  WhereClause = whereClause
              }).ToString();
            }
            catch(Exception e)
            {
                MessageBox.Show("Cann't query row "+e.Message);
            }

            

            return rowCount;
        }

        private ITable OpenTable(IFeatureWorkspace featureWorkspace, string tableName)
        {
            ITable table = featureWorkspace.OpenTable(tableName);
            return table;
        }

    }
}
