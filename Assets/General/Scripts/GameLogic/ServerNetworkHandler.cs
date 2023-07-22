using System;
using System.Linq;
using System.Threading.Tasks;
using Fusion;
using Fusion.Sockets;
using Scripts.Player.Network;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.GameLogic.Menu
{
    public class ServerNetworkHandler : MonoBehaviour
    {
        [SerializeField] private NetworkRunner autoHost;
        [SerializeField] private NetworkRunner shared;

        public bool onlineServer;
        private NetworkRunner _server;

        private void Start()
        {
            if (onlineServer)
            {
                _server = Instantiate(shared);
                _server.name = "--- MULTIPLAYER SERVER ---";

                var startServer = InitializeGameNetworkController(_server, GameMode.Shared,
                    NetAddress.Any(), SceneManager.GetActiveScene().buildIndex, null);

            }
            else
            {
                _server = Instantiate(autoHost);
                _server.name = "--- LOCAL SERVER ---";
                var startServer = InitializeGameNetworkController(_server, GameMode.AutoHostOrClient,
                    NetAddress.Any(), SceneManager.GetActiveScene().buildIndex, null);

            }
        }

        protected virtual Task InitializeGameNetworkController(NetworkRunner runner,
            GameMode gameMode, NetAddress netAddress, SceneRef scene, Action<NetworkRunner> initialized)
        {
            var sceneManager = runner.GetComponents(typeof(MonoBehaviour)).OfType<INetworkSceneManager>()
                .FirstOrDefault() ?? runner.gameObject.AddComponent<NetworkSceneManagerDefault>();

            runner.ProvideInput = true;
            return runner.StartGame(new StartGameArgs()
            {
                GameMode = gameMode,
                Address = netAddress,
                SessionName = null,
                Initialized = initialized,
                SceneManager = sceneManager
            });
        }
    }
}