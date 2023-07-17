using System;
using System.Collections;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using Vector2 = UnityEngine.Vector2;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private BoxCollider2D leftCollider;
    [SerializeField] private BoxCollider2D rightCollider;
    [SerializeField] private BoxCollider2D upCollider;
    [SerializeField] private BoxCollider2D downCollider;
    private const float MovementGridAdjust = .6449951f;

    private Grid _grid;
    private TilemapCollider2D _tilemapCollider2D;
    private bool _isMoving;
    [SerializeField] private float moveSpeed = 2;
    private Vector2 _finalTransform;

    #region Scripts

    private PlayerSpritesController _playerSpritesController;

    private PlayerInputActions _playerInput;
    private InputAction _playerWalk;
    private InputAction _playerDash;

    #endregion

    private void Awake()
    {
        _playerInput = new PlayerInputActions();
        _playerSpritesController = FindFirstObjectByType<PlayerSpritesController>();
        _grid = FindFirstObjectByType<Grid>();
        _tilemapCollider2D = FindFirstObjectByType<TilemapCollider2D>();
    }

    private void Update()
    {
        Vector2 position = transform.position;
        transform.position = Vector2.MoveTowards(position, _finalTransform, Time.deltaTime * moveSpeed);
        if (_isMoving) return;
        switch (_playerWalk.ReadValue<Vector2>())
        {
            case var v when v.Equals(Vector2.zero):
                return;
            case var v when v.Equals(Vector2.left):
                _playerSpritesController.UpdateSprites(v);
                if (leftCollider.IsTouching(_tilemapCollider2D)) return;
                _finalTransform = position + (v * MovementGridAdjust);

                break;
            case var v when v.Equals(Vector2.right):
                _playerSpritesController.UpdateSprites(v);
                if (rightCollider.IsTouching(_tilemapCollider2D)) return;
                _finalTransform = position + (v * MovementGridAdjust);

                break;
            case var v when v.Equals(Vector2.up):
                _playerSpritesController.UpdateSprites(v);
                if (upCollider.IsTouching(_tilemapCollider2D)) return;
                _finalTransform = position + (v * MovementGridAdjust);

                break;
            case var v when v.Equals(Vector2.down):
                _playerSpritesController.UpdateSprites(v);
                if (downCollider.IsTouching(_tilemapCollider2D)) return;
                _finalTransform = position + (v * MovementGridAdjust);

                break;
        }

        StartCoroutine(GridMoveCooldown());
    }


    private IEnumerator GridMoveCooldown()
    {
        _playerSpritesController.canChangeSprite = false;
        _isMoving = true;
        yield return new WaitUntil(() => (Vector2)transform.position == _finalTransform);
        Debug.Log("end walk");
        _playerSpritesController.canChangeSprite = true;
        _isMoving = false;
    }

    private void OnEnable()
    {
        _playerWalk = _playerInput.Player.Move;
        _playerWalk.Enable();
    }

    private void OnDisable()
    {
        _playerWalk.Disable();
    }
}