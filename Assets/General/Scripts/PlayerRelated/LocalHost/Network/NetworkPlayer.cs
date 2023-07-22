using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Scripts.Player.Controllers;
using Scripts.Player.Movement;
using Unity.Mathematics;

public class NetworkPlayer : NetworkBehaviour, IPlayerLeft
{
    public static NetworkPlayer Local { get; set; }
    [SerializeField] private GameObject playerCamera;
    [SerializeField] private LocalHostMovementController localHostMovement;
    [SerializeField] private LocalHostInput localHostInput;

    public override void Spawned()
    {
        //Se é o player q tá jogando
        if (Object.HasInputAuthority)
        {
            name = "Local Player";
            Local = this;
            Debug.Log($"{gameObject.name} local player");
        }
        else // se são os outros players
        {
            name = "Remote Player";
            gameObject.GetComponentInChildren<Camera>(true).gameObject.transform.parent.gameObject.SetActive(false);
            Debug.Log($"{gameObject.name} remote player");
        }
    }

    public void PlayerLeft(PlayerRef player)
    {
        if (player == Object.InputAuthority)
            Runner.Despawn(Object);
    }
    
}