using Tcc_Defects_Tracker.Extension;

namespace Tcc_Defects_Tracker.Model
{
    public class MrlcVintage : BasePropertyNotifier
    {
        private int _defectId;
        private string _productYear;


        public int DefectId
        {
            get { return _defectId; }
            set
            {
                _defectId = value;
                OnPropertyChanged("DefectId");
            }
        }

        public string ProductYear
        {
            get { return _productYear; }
            set
            {
                _productYear = value;
                OnPropertyChanged("ProductYear");

            }
        }
    }
}
