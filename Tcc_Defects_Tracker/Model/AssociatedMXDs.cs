using Tcc_Defects_Tracker.Extension;

namespace Tcc_Defects_Tracker.Model
{
    public class AssociatedMXDs : BasePropertyNotifier
    {
        private string _mxdId;
        private int _defectId;
        public int DefectId
        {
            get { return _defectId; }
            set
            {
                _defectId = value;
                OnPropertyChanged("DefectId");
            }
        }

        public string MxdId
        {
            get { return _mxdId; }
            set
            {
                _mxdId = value;
                OnPropertyChanged("MxdId");
            }
        }
    }
}
