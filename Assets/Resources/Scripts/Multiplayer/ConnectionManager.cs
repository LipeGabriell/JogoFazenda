using Photon.Pun;
using Resources.Scripts.Player.Controllers;
using Unity.Mathematics;
using UnityEngine;

public class ConnectionManager : MonoBehaviourPunCallbacks
{
    public GameObject player;
    public Transform spawn;

    private void Start()
    {
        Debug.Log("Connecting..");
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        Debug.Log("Connected");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        base.OnJoinedLobby();
        PhotonNetwork.JoinOrCreateRoom("Teste", null, null);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        GameObject _player = PhotonNetwork.Instantiate(player.name, spawn.position, quaternion.identity);
        _player.GetComponent<PlayerOnlineController>().InitiatePlayer();
    }
}