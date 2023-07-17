using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpritesController : MonoBehaviour
{
    public bool canChangeSprite = true;
    private PlayerInputActions _playerInputActions;
    private InputAction _playerMovementInput;
    private SpriteRenderer _spriteRenderer;

    [SerializeField] private Sprite upSprite;
    [SerializeField] private Sprite downSprite;
    [SerializeField] private Sprite sideSprite;

    private void Awake()
    {
        _playerInputActions = new PlayerInputActions();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void UpdateSprites(Vector2 direction)
    {
        switch (direction)
        {
            case var v when v.Equals(Vector2.zero):
                return;
            case var v when v.Equals(Vector2.left):
                _spriteRenderer.sprite = sideSprite;
                _spriteRenderer.flipX = true;
                break;
            case var v when v.Equals(Vector2.right):
                _spriteRenderer.sprite = sideSprite;
                _spriteRenderer.flipX = false;
                break;
            case var v when v.Equals(Vector2.up):
                _spriteRenderer.sprite = upSprite;
                _spriteRenderer.flipX = false;
                break;
            case var v when v.Equals(Vector2.down):
                _spriteRenderer.sprite = downSprite;
                _spriteRenderer.flipX = false;
                break;
        }
    }


    private void OnEnable()
    {
        _playerInputActions.Enable();
        _playerMovementInput = _playerInputActions.Player.Move;
        _playerMovementInput.Enable();
    }

    private void OnDisable()
    {
        _playerInputActions.Disable();
        _playerMovementInput.Disable();
    }
}