using Tcc_Defects_Tracker.Extension;

namespace Tcc_Defects_Tracker.Model
{
    public class LandsatScenes : BasePropertyNotifier
   {
       private int _defectId;
       private string _sceneId;
       
        public int DefectId
       {
           get { return _defectId; }
           set
           {
               _defectId = value;
               OnPropertyChanged("DefectId");
           }
       }
        public string SceneId
        {
            get { return _sceneId; }
            set
            {
                _sceneId = value;
                OnPropertyChanged("SceneId");
            }
        }
    }
}
