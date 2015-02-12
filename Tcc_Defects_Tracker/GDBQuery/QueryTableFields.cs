using System.Collections.Generic;
using Tcc_Defects_Tracker.Extension;

namespace Tcc_Defects_Tracker.GDBQuery
{
    public class QueryTableFields : BasePropertyNotifier
    {
        private List<string> _tableNames;
        private List<string> _conditions;
        private List<string> _tableFields;
        private string _searchString;
        private string _resultString;

        public List<string> TableNames
        {
            get { return _tableNames; }
            set
            {
                _tableNames = value;
                OnPropertyChanged("TableNames");
            }
        }

        public List<string> Conditions
        {
            get { return _conditions; }
            set
            {
                _conditions = value;
                OnPropertyChanged("Conditions");
            }
        }
        
        public List<string> TableFields
        {
            get { return _tableFields; }
            set
            {
                _tableFields = value;
                OnPropertyChanged("TableFields");
            }
        }

        public string SearchString
        {
            get { return _searchString; }
            set
            {
                _searchString = value;
                OnPropertyChanged("SearchString");
            }
        }

        public string ResultString
        {
            get { return _resultString; }
            set 
            { 
                _resultString = value;
                OnPropertyChanged("ResultString");
            }
        }


    }
}
