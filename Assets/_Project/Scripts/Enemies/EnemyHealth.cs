using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float stompBounceForce = 8f;

    // Punto en la cabeza del enemy para detectar stomp
    [SerializeField] private Transform stompDetector;

    private bool _isDead = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_isDead) return;
        if (!other.CompareTag("Player")) return;

        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
        if (playerHealth == null) return;
        if (playerHealth.IsInvincible) return;

        Rigidbody2D playerRb = other.GetComponent<Rigidbody2D>();

        // Stomp: el player cae desde arriba (velocidad Y negativa) 
        // Y su posición Y está por encima del stompDetector
        bool isFallingDown = playerRb != null && playerRb.linearVelocity.y < -0.1f;
        bool isAboveEnemy = stompDetector != null
            ? other.transform.position.y > stompDetector.position.y
            : other.transform.position.y > transform.position.y + 0.1f;

        bool isStomp = isFallingDown && isAboveEnemy;

        if (isStomp)
        {
            if (playerRb != null)
                playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, stompBounceForce);
            Die();
        }
        else
        {
            playerHealth.Die();
        }
    }

    private void Die()
    {
        _isDead = true;
        GetComponent<Collider2D>().enabled = false;
        EnemyPatrol patrol = GetComponent<EnemyPatrol>();
        if (patrol != null) patrol.enabled = false;
        Destroy(gameObject);
    }
}