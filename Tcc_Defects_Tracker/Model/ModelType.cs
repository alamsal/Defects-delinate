using Tcc_Defects_Tracker.Extension;

namespace Tcc_Defects_Tracker.Model
{
    public class ModelType : BasePropertyNotifier
    {
        private int _defectId;
        private string _modelTypeName;

        public int DefectId
        {
            get { return _defectId; }
            set
            {
                _defectId = value;
                OnPropertyChanged("DefectId");
            }
        }

        public string ModelName
        {
            get { return _modelTypeName;}
            set
            {
                _modelTypeName = value;
                OnPropertyChanged("ModelName");

            }
        }



    }
}
