// SpawnPoint.cs
// Marca el punto de inicio/respawn del player en la escena.
// No tiene lógica propia — es un marcador que PlayerHealth usa para reposicionar al player.

using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    // Gizmo visible en Scene view para ubicarlo fácilmente
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, 0.3f);
        Gizmos.DrawLine(transform.position, transform.position + Vector3.up * 0.5f);
    }
}