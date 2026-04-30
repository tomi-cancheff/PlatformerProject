// Door.cs
// Se suscribe al evento OnAllCoinsCollected del LevelManager.
// Al abrirse habilita el trigger para que el player cargue el siguiente nivel.

using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private GameObject visualClosed;   // sprite door closed
    [SerializeField] private GameObject visualOpen;     // sprite door open
    [SerializeField] private Collider2D doorTrigger;    // entrance trigger

    private bool _isOpen = false;

    private void OnEnable()
    {
        LevelManager.OnAllCoinsCollected += OpenDoor;
    }

    private void OnDisable()
    {
        LevelManager.OnAllCoinsCollected -= OpenDoor;
    }

    private void Start()
    {
        // Estado inicial: cerrada
        SetDoorState(false);
    }

    private void OpenDoor()
    {
        _isOpen = true;
        SetDoorState(true);
    }

    private void SetDoorState(bool open)
    {
        if (visualClosed != null) visualClosed.SetActive(!open);
        if (visualOpen != null) visualOpen.SetActive(open);
        if (doorTrigger != null) doorTrigger.enabled = open;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!_isOpen) return;
        if (!other.CompareTag("Player")) return;

        LevelManager.Instance.LoadNextLevel();
    }
}