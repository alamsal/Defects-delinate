using Tcc_Defects_Tracker.Model;

namespace Tcc_Defects_Tracker.GDBOperations
{
    public interface IStoreAttributesInSHP
    {
        void StoreAttribues(DefectShape defectShape, LandsatScenes scene, Source defectSource, AssociatedMXDs mxDs,
                            ModelType modelType, MrlcVintage mrlcVintage, ProductionProcess productionProcess);
    }
}
