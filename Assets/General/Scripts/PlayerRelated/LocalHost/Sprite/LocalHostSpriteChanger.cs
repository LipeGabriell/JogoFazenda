using System;
using Fusion;
using UnityEngine;

namespace Scripts.Player.Controllers
{
    public class LocalHostSpriteChanger : NetworkBehaviour
    {
        [SerializeField] private Animator movementAnimator;
        [Networked] public NetworkBool Walking { get; set; }

        private void Awake()
        {
            movementAnimator = GetComponent<Animator>();
        }

        public void Update()
        {
            GetInput(out PlayerNetworkData data);
            Walking = !data.Direction.Equals(Vector2.zero);


            if (Walking)
            {
                movementAnimator.ResetTrigger("idle");
                movementAnimator.SetTrigger("walk");
            }
            else
            {
                movementAnimator.ResetTrigger("walk");
                movementAnimator.SetTrigger("idle");
            }
        }

        [Rpc(RpcSources.InputAuthority, RpcTargets.StateAuthority)]
        public void RPC_SetAnimationValue(Vector2 direction, RpcInfo info = default)
        {
            movementAnimator.SetFloat("directionX", direction.x);
            movementAnimator.SetFloat("directionY", direction.y);
        }
    }
}