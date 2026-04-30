using UnityEngine;

[CreateAssetMenu(menuName = "MiniPlatformer/PlayerConfig")]
public class PlayerConfigSO : ScriptableObject
{
   [Header("Movement")]
    public float moveSpeed = 5f;

    [Header("Jump")]
    public float jumpForce = 10f;

    [Header("Ground Detection")]
    public float groundCheckRadius = 0.08f;
    public LayerMask groundLayer;
    
}
