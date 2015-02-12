using Tcc_Defects_Tracker.Extension;

namespace Tcc_Defects_Tracker.Model
{
    public class DefectShape : BasePropertyNotifier
    {
        private int _defectId;
        private string _defectType;
        private string _appearance;
        private string _status;
        private string _solution;

        public int DefectId
        {
            get { return _defectId; }
            set
            {
                _defectId = value;
                OnPropertyChanged("DefectId");
            }
        }

        public string DefectType
        {
            get { return _defectType; }
            set
            {
                _defectType = value;
                OnPropertyChanged("DefectType");
            }
        }

        public string Appearance
        {
            get { return _appearance; }
            set
            {
                _appearance = value;
                OnPropertyChanged("Appearance");
            }
        }

        public string Status
        {
            get { return _status; }
            set
            {
                _status = value;
                OnPropertyChanged("Status");
            }
        }

        public string Solution
        {
            get { return _solution; }
            set
            {
                _solution = value;
                OnPropertyChanged("Solution");
            }
        }


    }
}
