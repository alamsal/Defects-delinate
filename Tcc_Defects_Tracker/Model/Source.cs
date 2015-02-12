using Tcc_Defects_Tracker.Extension;

namespace Tcc_Defects_Tracker.Model
{
    public class Source : BasePropertyNotifier
    {
      private int _defectId;
      private string _sourceName;

      public int DefectId
      {
          get { return _defectId; }
          set
          {
              _defectId = value;
              OnPropertyChanged("DefectId");
          }
      }

      public string SourceName
      {
          get { return _sourceName; }
          set
          {
              _sourceName = value;
              OnPropertyChanged("SourceName");
          }
      }
    }
}
