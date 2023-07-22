using System;
using Fusion;
using Scripts.Player.Movement;
using UnityEngine;

namespace Scripts.Player.Controllers
{
    public class MultiplayerSpriteChanger : NetworkBehaviour
    {
        [SerializeField] private Animator movementAnimator;
        private MultiplayerInput _multiplayerInput;
        [Networked] public NetworkBool Walking { get; set; }

        public override void Spawned()
        {
            movementAnimator = GetComponent<Animator>();
            _multiplayerInput = GetComponentInParent<MultiplayerInput>();
        }

        public override void FixedUpdateNetwork()
        {
            Walking = !_multiplayerInput.InputTreatment().Equals(Vector2.zero);

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

        public void SetAnimationValue(Vector2 direction)
        {
            movementAnimator.SetFloat("directionX", direction.x);
            movementAnimator.SetFloat("directionY", direction.y);
        }
    }
}