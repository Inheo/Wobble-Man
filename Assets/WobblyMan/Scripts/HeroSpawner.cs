using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroSpawner : MonoBehaviour
{
    [SerializeField] private GameObject hero;


    void Start()
    {
        // отключаем вначале игры видимость игрока
        hero.SetActive(false);
        // включаем видимость через 1 сек
        Invoke(nameof(Spawn), 1f);
    }

    private void Spawn()
    {
        hero.SetActive(true);
    }
}