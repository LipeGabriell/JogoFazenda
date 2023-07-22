using Fusion;
using UnityEngine;

namespace Scripts.Player.Network
{
    public class PlayerInitializerMultiplayer : SimulationBehaviour, IPlayerJoined
    {
        public GameObject playerPrefab;

        public void PlayerJoined(PlayerRef player)
        {
            Debug.Log("-------- MULTIPLAYER SERVER STARTED --------");

            NetworkObject spawned = null;
            if (player == Runner.LocalPlayer)
            {
                spawned = Runner.Spawn(playerPrefab, new Vector3(0, 1, 0), Quaternion.identity, player);
            }
         
        }
    }
}