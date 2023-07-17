using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Resources.Scripts.GridSystem
{
    public class GridController : MonoBehaviour
    {
        private PlayerInputActions _playerInputActions;
        private InputAction _playerMousePos;
        private Camera _mainCamera;
        private Grid _grid;


        private void Awake()
        {
            _playerInputActions = new PlayerInputActions();
            _grid = GetComponent<Grid>();
            _mainCamera = Camera.main;
        }

        private void Update()
        {
            var mousePos = _mainCamera.ScreenToWorldPoint(_playerMousePos.ReadValue<Vector2>());
            var gridInWorld = _grid.WorldToCell(mousePos);
        }

        private void OnEnable()
        {
            _playerInputActions.Enable();
            _playerMousePos = _playerInputActions.UI.Point;
            _playerMousePos.Enable();
        }

        private void OnDisable()
        {
            _playerMousePos.Disable();
            _playerInputActions.Disable();
        }
    }
}