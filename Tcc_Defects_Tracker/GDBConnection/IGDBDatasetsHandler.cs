using System.Collections.Generic;

namespace Tcc_Defects_Tracker.GDBConnection
{
    public interface IGDBDatasetsHandler
    {
        List<string> GetDatasetListFromGDB();
        List<string> GetFeatureClassListFromDataset(string featureDatasetName);
    }
}
