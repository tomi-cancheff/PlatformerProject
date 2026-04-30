// HUDController.cs
// Muestra el contador de monedas en pantalla.
// Usa métodos estáticos para que LevelManager lo llame sin referencia directa.

using UnityEngine;
using TMPro;

public class HUDController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinText;

    private static HUDController _instance;

    private void Awake()
    {
        _instance = this;
    }

    public static void UpdateCoins(int remaining, int total)
    {
        if (_instance == null || _instance.coinText == null) return;
        _instance.coinText.text = $"Coins: {remaining}/{total}";
    }
}