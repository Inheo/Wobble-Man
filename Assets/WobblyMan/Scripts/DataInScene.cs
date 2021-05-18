using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Класс для хранения данных, которые нужны для заспавленных объектов
public class DataInScene : MonoBehaviour
{
    [SerializeField] private Joystick joystick;
    public Joystick Joystick => joystick;

    public static DataInScene Instance;

    private void Awake()
    {
        Instance = this;
    }
}
