using CI.QuickSave;
using UnityEngine;

namespace Scripts.Farm
{
    public class FarmManagement : MonoBehaviour
    {
        public FarmData FarmData { get; private set; }

        private FarmData NewFarmData()
        {
            var writer = QuickSaveWriter.Create("FarmData");
            var farmData = new FarmData();
            writer.Write("FarmData", FarmData).TryCommit();
            return farmData;
        }


        private FarmData LoadFarmData()
        {
            var reader = QuickSaveReader.Create("FarmData");
            reader.Reload();
            reader.TryRead<FarmData>("FarmData", out var farmData);
            return farmData;
        }
    }
}