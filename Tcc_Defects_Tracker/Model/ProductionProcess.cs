using Tcc_Defects_Tracker.Extension;

namespace Tcc_Defects_Tracker.Model
{
    public class ProductionProcess : BasePropertyNotifier
    {
        public int _defectId;
        private string _processName { get; set; }

        public int DefectId
        {
            get { return _defectId; }
            set
            {
                _defectId = value;
                OnPropertyChanged("DefectId");
            }
        }

        public string ProcessName
        {
            get { return _processName; }
            set
            {
                _processName = value;
                OnPropertyChanged("ProcessName");

            }
        } 



    }
}
