// Coin.cs
// Va en cada prefab de moneda.
// Al recibir el trigger del player notifica al LevelManager y se destruye.

using UnityEngine;

public class Coin : MonoBehaviour
{
    // Asigná un SFX aquí cuando tengas audio (Fase 4)
    // [SerializeField] private AudioClip collectSound;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        LevelManager.Instance.CoinCollected();
        Destroy(gameObject);
    }
}