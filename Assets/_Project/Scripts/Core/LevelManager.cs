// LevelManager.cs
// Singleton de escena. Lleva la cuenta de monedas recogidas.
// Cuando llegan a 0 dispara el evento OnAllCoinsCollected.
// La Door escucha ese evento para abrirse.

using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    [SerializeField] private LevelDataSO levelData;

    // Door y cualquier otro sistema se suscriben a este evento
    public static event Action OnAllCoinsCollected;

    private int _coinsRemaining;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        _coinsRemaining = levelData.totalCoins;
        HUDController.UpdateCoins(_coinsRemaining, levelData.totalCoins);
    }

    // Llamado por Coin.cs al ser recogida
    public void CoinCollected()
    {
        _coinsRemaining--;
        HUDController.UpdateCoins(_coinsRemaining, levelData.totalCoins);

        if (_coinsRemaining <= 0)
        {
            OnAllCoinsCollected?.Invoke();
        }
    }

    // Llamado por Door.cs cuando el player entra al trigger
    public void LoadNextLevel()
    {
        SceneManager.LoadScene(levelData.nextSceneName);
    }
}