using System;
using Newtonsoft.Json;
using Scripts.Farm;
using UnityEngine;

namespace Scripts.Player
    // ReSharper disable InconsistentNaming
{
    [JsonObject(MemberSerialization.Fields)]
    [Serializable]
    public class PlayerData
    {
        [SerializeField] public string PlayerNick;

        [SerializeField] public string UUID;

        [SerializeField] public float Coins;

        [SerializeField] public Vector2 Position;

        [SerializeField] public FarmData FarmData;

        public PlayerData(string nick)
        {
            PlayerNick = nick;
            UUID = SystemInfo.deviceUniqueIdentifier;
            FarmData = new FarmData();
        }
    }
}