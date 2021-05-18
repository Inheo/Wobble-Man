using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Hero : MonoBehaviour
{
    private HeroMovement heroMovement;

    public static Hero Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        heroMovement = GetComponent<HeroMovement>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag.Equals("Ground"))
        {
            // Анимация прыжка при выигрыше
            // Если выиграли можно попрыгать:)
            if (Game.Instance.Win)
            {
                heroMovement.Jump();
            }
        }
    }
}
