using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private SpawnPoint spawnPoint;
    [SerializeField] private float respawnInvincibilityTime = 1.5f;

    private bool _isDead = false;
    private bool _isInvincible = false;
    private Rigidbody2D _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    public void Die()
    {
        if (_isDead || _isInvincible) return;
        _isDead = true;
        StartCoroutine(RespawnRoutine());
    }

    private IEnumerator RespawnRoutine()
    {
        // Paramos al player inmediatamente
        _rb.linearVelocity = Vector2.zero;
        GetComponent<PlayerInputHandler>().enabled = false;

        // Respawn casi instantáneo — sin delay largo
        yield return new WaitForSeconds(0.1f);

        transform.position = spawnPoint.transform.position;
        _rb.linearVelocity = Vector2.zero;

        GetComponent<PlayerInputHandler>().enabled = true;
        _isDead = false;

        // Invencibilidad post-respawn
        _isInvincible = true;
        yield return new WaitForSeconds(respawnInvincibilityTime);
        _isInvincible = false;
    }

    public bool IsInvincible => _isInvincible;
}