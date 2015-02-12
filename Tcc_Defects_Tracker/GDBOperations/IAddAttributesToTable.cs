using System.Collections.Generic;
using ESRI.ArcGIS.Geodatabase;

namespace Tcc_Defects_Tracker.GDBOperations
{
    public interface IAddAttributesToTable
    {
        void StoreAttributesInTableRow(ITable table, List<string> fields, List<string> values);
    }
}
