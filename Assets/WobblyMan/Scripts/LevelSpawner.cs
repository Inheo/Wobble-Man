using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    // Выбранный уровень
    public static int CurrentLevel = 0;

    // Все уровни
    public GameObject[] Levels;

    private void Start()
    {
        // Если есть сохраненый последний уровень, то получаем иначе начинаем с 1 уровня
        CurrentLevel = PlayerPrefs.GetInt("CurrentLevel", 0);
        Spawn();
    }

    /// <summary>
    /// Спавнит уровень
    /// </summary>
    private void Spawn()
    {
        Instantiate(Levels[CurrentLevel]);
    }
}
