using System;
using Cinemachine;
using Fusion;
using UnityEngine;

namespace Scripts.Player.Multiplayer.Utils
{
    public class HitboxManager : NetworkBehaviour
    {
        [SerializeField] private GameObject hits;

        public void Start()
        {
            if (HasStateAuthority)
            {
                hits.SetActive(true);
            }
        }
    }
}