using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using ESRI.ArcGIS.Framework;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Tcc_Defects_Tracker.GDBQuery;
using Tcc_Defects_Tracker.GDBTables;

namespace Tcc_Defects_Tracker.ViewModel
{
    public class QueryGDBViewModel:ViewModelBase
    {
        #region private members

        private string _selectedTableName;
        private string _selectedFieldName;
        private string _selectedQueryCondition;

        private QueryTableFields _queryTableFields;
        private RelayCommand _queryTableCommand;
        
        #endregion private members

        #region properties
        
        public IApplication ArcMapApplication { get; set; }

        public string SelectedTableName
        {
            get { return _selectedTableName; }
            set { Set("SelectedTableName", ref _selectedTableName, value); }
        }

        public string SelectedFieldName
        {
            get { return _selectedFieldName; }
            set { Set("SelectedFieldName",ref _selectedFieldName,value); }
        }

        public string SelectedQueryCondition
        {
            get { return _selectedQueryCondition; }
            set { Set("SelectedQueryCondition",ref _selectedQueryCondition,value); }
        }

        public QueryTableFields QueryFields
        {
            get { return _queryTableFields; }
            set { Set("QueryFields", ref _queryTableFields, value); }
        }
        
        #endregion properties

        #region Ctor
        
        public QueryGDBViewModel()
        {
            _queryTableFields= new QueryTableFields();
            PopulateCombosWithTableInfo();


        }

        #endregion Ctor

        #region RelayCommands
        
        public RelayCommand QueryTableCommand
        {
            get
            {
                if (_queryTableCommand != null)
                {
                    return _queryTableCommand;
                }
                _queryTableCommand = new RelayCommand(QueryStandaloneGDBTables);
                return _queryTableCommand;
            }
        }

        #endregion RelayCommands

        #region private helpers
        
        private void QueryStandaloneGDBTables()
        {
            //MessageBox.Show(_queryTableFields.SearchString);

            //_queryTableFields.ResultString = _queryTableFields.SearchString;

            QueryHandler queryHandler = new QueryHandler(ArcMapApplication);
           _queryTableFields.ResultString= queryHandler.StartQueringTable(SelectedTableName,SelectedFieldName,SelectedQueryCondition,_queryTableFields.SearchString);
            
        }
        
        private void PopulateCombosWithTableInfo()
        {
            _queryTableFields.TableNames = GetTableNames(); // new List<string> { "Table 1", "Table 2" };
            _queryTableFields.TableFields = GetFieldNames(); //new List<string> { "Field 1", "Field 2" };
            _queryTableFields.Conditions = new List<string>{"=",">"};
            
            SelectedTableName = _queryTableFields.TableNames[0];
            SelectedFieldName = _queryTableFields.TableFields[0];
            SelectedQueryCondition = _queryTableFields.Conditions[0];
        }

        private static List<string> GetTableNames()
        {
            return Enum.GetValues(typeof(EnumTableNames)).Cast<EnumTableNames>().Select(value => value.ToString()).ToList();
        }

        private static List<string> GetFieldNames()
        {
            return Enum.GetValues(typeof(EnumTableFields)).Cast<EnumTableFields>().Select(value => value.ToString()).ToList();
        }


        #endregion private helpers


    }
}
