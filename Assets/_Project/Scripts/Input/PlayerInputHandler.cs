using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public float HorizontalInput { get; private set; }
    public bool JumpPressed { get; private set; }

    private PlayerInputActions _inputActions;

    private void Awake()
    {
        _inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        _inputActions.Enable();
        _inputActions.Player.Jump.performed += OnJumpPerformed;
    }

    private void OnDisable()
    {
        _inputActions.Player.Jump.performed -= OnJumpPerformed;
        _inputActions.Disable();
    }

    private void Update()
    {
        HorizontalInput = _inputActions.Player.Move.ReadValue<Vector2>().x;
    }

    private void OnJumpPerformed(InputAction.CallbackContext ctx)
    {
        JumpPressed = true;
    }

    public void ConsumeJump() => JumpPressed = false;

    // Android-ready — hookear desde on-screen joystick cuando llegue la fase mobile
    public void SetMobileHorizontal(float value) => HorizontalInput = value;
    public void SetMobileJump() => JumpPressed = true;
}
