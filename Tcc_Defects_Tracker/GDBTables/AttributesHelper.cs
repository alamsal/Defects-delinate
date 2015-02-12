using System.Collections.Generic;

namespace Tcc_Defects_Tracker.GDBTables
{
    public class AttributesHelper
    {
        public List<string> GetAttributesList(string field1, string field2)
        {
            var attributes = new List<string>() { field1, field2 };
            return attributes;
        }
    }
}
