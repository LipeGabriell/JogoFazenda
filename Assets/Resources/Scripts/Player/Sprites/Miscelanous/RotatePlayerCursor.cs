using UnityEngine;
using UnityEngine.InputSystem;

// ReSharper disable once Unity.InefficientPropertyAccess

public class RotatePlayerCursor : MonoBehaviour
{
    private PlayerInputActions _playerInput;
    private InputAction _pointerInput;
    [field: SerializeField] public Camera playerCamera { get; private set; }

    private void Awake()
    {
        _playerInput = new PlayerInputActions();
    }

    // Update is called once per frame
    private void Update()
    {
        var pointerPosition = GetPointerInput(); //posição do mouse no mundo
        var direction = (pointerPosition - (Vector2)transform.position).normalized;
        transform.right = direction;
        transform.Rotate(0f, 0f, -90f);

        var scale = transform.localScale;
        scale.x = direction.x switch
        {
            < 0 => -1,
            > 0 => 1,
            _ => scale.x
        };

        transform.localScale = scale;
    }

    private Vector2 GetPointerInput()
    {
        Vector3 mousePos = _pointerInput.ReadValue<Vector2>();
        mousePos.z = playerCamera.nearClipPlane;
        return playerCamera.ScreenToWorldPoint(mousePos);
    }

    private void OnEnable()
    {
        _playerInput.Enable();
        _pointerInput = _playerInput.UI.Point;
        _pointerInput.Enable();
    }

    private void OnDisable()
    {
        _pointerInput.Disable();
        _playerInput.Disable();
    }
}