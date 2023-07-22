using System;
using System.Linq;
using Scripts.Player.Controllers;
using UnityEngine;

namespace Scripts.Player.Movement
{
    public class LocalHostInput : MonoBehaviour
    {
        private PlayerInput _playerInput;
        private Vector2 _input;

        private LocalHostMovementController _localHostMovementController;
        private LocalHostSpriteChanger _localHostSpriteChanger;

        public void Awake()
        {
            _playerInput = new PlayerInput();
            _localHostMovementController = GetComponent<LocalHostMovementController>();
            _localHostSpriteChanger = GetComponentInChildren<LocalHostSpriteChanger>();
        }

        private void Update()
        {
            _input = _playerInput.Player.Move.ReadValue<Vector2>();
        }

        public PlayerNetworkData GetInput()
        {
            var playerNetworkData = new PlayerNetworkData
            {
                Direction = InputTreatment(_input)
            };
            return playerNetworkData;
        }

        private Vector2 InputTreatment(Vector2 direction)
        {
            if (Mathf.Abs(direction.x) > 0 && Mathf.Abs(direction.y) > 0) return Vector2.zero;

            switch (direction)
            {
                case var v when v.Equals(Vector2.left):
                    if (!_localHostMovementController.LevelObstacles.Any(tilemapCollider2D =>
                            _localHostMovementController.LeftCollider.IsTouching(tilemapCollider2D))) return v;
                    _localHostSpriteChanger.RPC_SetAnimationValue(v);
                    break;

                case var v when v.Equals(Vector2.right):
                    if (!_localHostMovementController.LevelObstacles.Any(tilemapCollider2D =>
                            _localHostMovementController.RightCollider.IsTouching(tilemapCollider2D)))
                        return v;
                    _localHostSpriteChanger.RPC_SetAnimationValue(v);
                    break;

                case var v when v.Equals(Vector2.up):
                    if (!_localHostMovementController.LevelObstacles.Any(tilemapCollider2D =>
                            _localHostMovementController.UpCollider.IsTouching(tilemapCollider2D))) return v;
                    _localHostSpriteChanger.RPC_SetAnimationValue(v);
                    break;

                case var v when v.Equals(Vector2.down):
                    if (!_localHostMovementController.LevelObstacles.Any(tilemapCollider2D =>
                            _localHostMovementController.DownCollider.IsTouching(tilemapCollider2D))) return v;
                    _localHostSpriteChanger.RPC_SetAnimationValue(v);
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