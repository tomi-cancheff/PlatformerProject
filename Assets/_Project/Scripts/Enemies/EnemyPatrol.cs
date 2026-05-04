// EnemyPatrol.cs
// Patrulla entre PointA y PointB. Ambos deben ser GameObjects independientes
// en la escena, NO hijos del Enemy.

using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float waitTime = 0.5f;

    private Transform _currentTarget;
    private float _waitTimer = 0f;
    private bool _waiting = false;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _currentTarget = pointB;
    }

    private void Update()
    {
        if (_waiting)
        {
            _waitTimer -= Time.deltaTime;
            if (_waitTimer <= 0f) _waiting = false;
            return;
        }

        MoveTowardsTarget();
        CheckIfReached();
    }

    private void MoveTowardsTarget()
    {
        // Movemos solo en X para evitar que el enemigo se mueva en Y
        float newX = Mathf.MoveTowards(
            transform.position.x,
            _currentTarget.position.x,
            speed * Time.deltaTime
        );

        transform.position = new Vector2(newX, transform.position.y);

        // Flip según dirección
        if (_spriteRenderer != null)
            _spriteRenderer.flipX = _currentTarget == pointA;
    }

    private void CheckIfReached()
    {
        // Comparamos solo X para evitar falsos positivos por diferencia en Y
        float distanceX = Mathf.Abs(transform.position.x - _currentTarget.position.x);

        if (distanceX < 0.05f)
        {
            _currentTarget = (_currentTarget == pointB) ? pointA : pointB;
            _waiting = true;
            _waitTimer = waitTime;
        }
    }

    private void OnDrawGizmos()
    {
        if (pointA == null || pointB == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(pointA.position, 0.15f);
        Gizmos.DrawWireSphere(pointB.position, 0.15f);
        Gizmos.DrawLine(pointA.position, pointB.position);
    }
}