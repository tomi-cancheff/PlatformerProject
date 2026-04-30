// LevelDataSO.cs
// ScriptableObject que define los datos de cada nivel.
// Creá uno por nivel: LevelData_01, LevelData_02, etc.

using UnityEngine;

[CreateAssetMenu(menuName = "MiniPlatformer/LevelData")]
public class LevelDataSO : ScriptableObject
{
    [Header("Level Info")]
    public string levelName = "Level 01";
    public int totalCoins = 5;

    [Header("Next Level")]
    // Nombre exacto de la escena a cargar al completar el nivel
    public string nextSceneName = "Level_02";
}