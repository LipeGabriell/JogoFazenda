using System;
using UnityEngine;

namespace Resources.Scripts.Player.Controllers
{
    public class PlayerOnlineController : MonoBehaviour
    {
        [field: SerializeField] public GameObject camera { get; private set; }
        [field: SerializeField] public GameObject mouseIndicator { get; private set; }
        [field: SerializeField] public PlayerMovementController playerMovementController { get; private set; }


        public void InitiatePlayer()
        {
            camera.SetActive(true);
            mouseIndicator.SetActive(true);
            playerMovementController.enabled = true;
        }
    }
}