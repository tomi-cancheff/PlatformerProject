using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerInputHandler))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerConfigSO config;
    [SerializeField] private Transform groundCheckPoint;

    private Rigidbody2D _rb;
    private PlayerInputHandler _input;
    private bool _isGrounded;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _input = GetComponent<PlayerInputHandler>();
    }

    private void Update()
    {
        CheckGround();
        HandleJump();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void CheckGround()
    {
        _isGrounded = Physics2D.OverlapCircle(
            groundCheckPoint.position,
            config.groundCheckRadius,
            config.groundLayer
        );
    }

    private void HandleMovement()
    {
        _rb.linearVelocity = new Vector2(
            _input.HorizontalInput * config.moveSpeed,
            _rb.linearVelocity.y
        );

        if (_input.HorizontalInput != 0)
        {
            transform.localScale = new Vector3(
                Mathf.Sign(_input.HorizontalInput), 1f, 1f
            );
        }
    }

    private void HandleJump()
    {
        if (_input.JumpPressed && _isGrounded)
        {
            _rb.AddForce(Vector2.up * config.jumpForce, ForceMode2D.Impulse);
        }
        _input.ConsumeJump();
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheckPoint == null || config == null) return;
        Gizmos.color = _isGrounded ? Color.green : Color.red;
        Gizmos.DrawWireSphere(groundCheckPoint.position, config.groundCheckRadius);
    }
}
