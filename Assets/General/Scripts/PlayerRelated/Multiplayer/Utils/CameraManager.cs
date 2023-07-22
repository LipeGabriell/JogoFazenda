using System;
using Cinemachine;
using Fusion;
using UnityEngine;

namespace Scripts.Player.Multiplayer.Utils
{
    public class CameraManager : NetworkBehaviour
    {
        public void Start()
        {
            if (HasStateAuthority)
            {
                FindObjectOfType<CinemachineVirtualCamera>().m_Follow = gameObject.transform;
                FindObjectOfType<CinemachineVirtualCamera>().m_LookAt = gameObject.transform;
            }
        }
    }
}