using System;
using UnityEngine;

namespace Scripts.Farm
{
    [Serializable]
    // ReSharper disable InconsistentNaming
    public class FarmData
    {
        [field: SerializeField] public string FarmName;

        public FarmData()
        {
            FarmName = "Default";
        }
    }
}