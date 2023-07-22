using System;
using System.Linq;
using Fusion;
using Scripts.Player.Controllers;
using UnityEngine;

namespace Scripts.Player.Movement
{
    public class MultiplayerInput : NetworkBehaviour
    {
        private PlayerInput _playerInput;
        private Vector2 _input;

        [SerializeField] private MultiplayerMovementController multiplayerMovementController;
        [SerializeField] private MultiplayerSpriteChanger multiplayerSpriteChanger;

        public void Awake()
        {
            _playerInput = new PlayerInput();
        }

        public Vector2 InputTreatment()
        {
            var direction = _playerInput.Player.Move.ReadValue<Vector2>();

            if (Mathf.Abs(direction.x) > 0 && Mathf.Abs(direction.y) > 0) return Vector2.zero;

            switch (direction)
            {
                case var v when v.Equals(Vector2.left):
                    if (!multiplayerMovementController.LevelObstacles.Any(tilemapCollider2D =>
                            multiplayerMovementController.LeftCollider.IsTouching(tilemapCollider2D))) return v;
                    multiplayerSpriteChanger.SetAnimationValue(v);
                    break;

                case var v when v.Equals(Vector2.right):
                    if (!multiplayerMovementController.LevelObstacles.Any(tilemapCollider2D =>
                            multiplayerMovementController.RightCollider.IsTouching(tilemapCollider2D)))
                        return v;
                    multiplayerSpriteChanger.SetAnimationValue(v);
                    break;

                case var v when v.Equals(Vector2.up):
                    if (!multiplayerMovementController.LevelObstacles.Any(tilemapCollider2D =>
                            multiplayerMovementController.UpCollider.IsTouching(tilemapCollider2D))) return v;
                    multiplayerSpriteChanger.SetAnimationValue(v);
                    break;

                case var v when v.Equals(Vector2.down):
                    if (!multiplayerMovementController.LevelObstacles.Any(tilemapCollider2D =>
                            multiplayerMovementController.DownCollider.IsTouching(tilemapCollider2D))) return v;
                    multiplayerSpriteChanger.SetAnimationValue(v);
                    break;
            }

            return Vector2.zero;
        }

        private void OnEnable()
        {
            _playerInput.Player.Enable();
        }

        private void OnDisable()
        {
            _playerInput.Player.Disable();
        }
    }
}