using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Fusion;
using Fusion.Sockets;
using Scripts.Player;
using Scripts.Player.Controllers;
using Scripts.Player.Movement;
using Unity.Mathematics;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Scripts.GameLogic.Menu
{
    public class GameNetworkController : MonoBehaviour, INetworkRunnerCallbacks
    {
        private LocalHostInput _localHostInput;
        public NetworkPlayer playerPrefab;

        private PlayerData _playerDataTransport;

        public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
        {
            Debug.Log("Server: Spawnando player");
            runner.Spawn(playerPrefab, Vector3.zero, quaternion.identity, player)
                .GetComponent<NetworkObject>();
        }

        public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
        {
        }

        public void OnInput(NetworkRunner runner, NetworkInput input)
        {
            if (_localHostInput == null && NetworkPlayer.Local != null)
                _localHostInput = NetworkPlayer.Local.GetComponent<LocalHostInput>();

            if (_localHostInput != null)
            {
                var data = _localHostInput.GetInput();
                input.Set(data);
            }
        }

        public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
        {
        }

        public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
        {
        }

        public void OnConnectedToServer(NetworkRunner runner)
        {
            runner.JoinSessionLobby(SessionLobby.Shared);
        }

        public void OnDisconnectedFromServer(NetworkRunner runner)
        {
        }

        public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request,
            byte[] token)
        {
        }

        public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
        {
        }

        public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
        {
        }

        public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
        {
        }

        public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
        {
        }

        public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
        {
        }

        public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data)
        {
        }

        public void OnSceneLoadDone(NetworkRunner runner)
        {
        }

        public void OnSceneLoadStart(NetworkRunner runner)
        {
        }
    }
}