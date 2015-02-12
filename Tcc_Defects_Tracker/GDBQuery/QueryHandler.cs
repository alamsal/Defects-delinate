using System;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.Geodatabase;

namespace Tcc_Defects_Tracker.GDBQuery
{
    public class QueryHandler
    {

        private IApplication _arcMapApplication;
        public QueryHandler(IApplication mapApplication)
        {
            _arcMapApplication = mapApplication;
        }

        //SelectedTableName,SelectedFieldName,SelectedQueryCondition,_queryTableFields.SearchString
        public string StartQueringTable(string tableName,string fieldName,string queryCondition,string searchValue )
        {
            QueryHelper queryHelper = new QueryHelper(_arcMapApplication);
            string rowCount=null;

            string clause= queryHelper.CreateWhereClause(fieldName, queryCondition, searchValue);
            ITable table= queryHelper.OpenTableToQuery(tableName);
            if(table!=null)
            {
                rowCount = queryHelper.GetRowCountFromTable(table, clause);
            }
           

           return rowCount;

        }






    }
}
